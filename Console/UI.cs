using Core.Services;
using Core.Entities;
using Core.Interfaces;
using Core.Repositories;
public class UI
{
    private readonly AuthService _authService;
    private readonly PostService _postService;
    private readonly CommentService _commentService;
    private User _currentUser;

    public UI(AuthService authService, PostService postService, CommentService commentService)
    {
        _authService = authService;
        _postService = postService;
        _commentService = commentService;
    }

    public void Start()
    {
        while (true)
        {
            if (_currentUser == null)
            {
                ShowAuthMenu();
            }
            else
            {
                ShowSocialMenu();
            }
        }
    }
    private void ShowAuthMenu()
    {
        string[] options = { "LOGIN", "REGISTER", "EXIT" };
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            DrawLargeBox("SOCIAL MEDIA PLATFORM", options, selectedIndex);

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;

            else if (key == ConsoleKey.DownArrow)
                selectedIndex = (selectedIndex + 1) % options.Length;

            else if (key == ConsoleKey.Enter)
            {
                switch (selectedIndex)
                {
                    case 0:
                        Login();
                        return;
                    case 1:
                        Register();
                        return;
                    case 2:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
    private void DrawLargeBox(string title, string[] options, int selectedIndex)
    {
        int width = 100;
        int heightPadding = 3;

        Console.WriteLine(" " + new string('_', width) + "");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("|" + CenterText(title.ToUpper(), width) + "|");
        Console.ResetColor();

        Console.WriteLine("|" + new string('-', width) + "|");

        for (int i = 0; i < heightPadding; i++)
            Console.WriteLine("|" + new string(' ', width) + "|");

        for (int i = 0; i < options.Length; i++)
        {
            string prefix = i == selectedIndex ? "►  " : "   ";
            string text = prefix + options[i];

            if (i == selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("|" + CenterText(text, width) + "|");

            Console.ResetColor();
        }

        for (int i = 0; i < heightPadding; i++)
            Console.WriteLine("|" + new string(' ', width) + "|");

        Console.WriteLine("|" + new string('_', width) + "|");
    }
    private string CenterText(string text, int width)
    {
        if (text.Length >= width)
            return text.Substring(0, width);
        int leftPadding = (width - text.Length) / 2;
        int rightPadding = width - text.Length - leftPadding;
        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }
    private void ShowSocialMenu()
    {
        string[] options = { "CREATE POST", "VIEW", "COMMENT", "LOGOUT" };
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            DrawLargeBox($"WELCOME {_currentUser.Username}", options, selectedIndex);

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;

            else if (key == ConsoleKey.DownArrow)
                selectedIndex = (selectedIndex + 1) % options.Length;

            else if (key == ConsoleKey.Enter)
            {
                switch (selectedIndex)
                {
                    case 0:
                        CreatePost();
                        break;
                    case 1:
                        ViewFeed();
                        break;
                    case 2:
                        CommentOnPost();
                        break;
                    case 3:
                        _currentUser = null;
                        return;
                }
            }
        }
    }
    private string ReadPassword()
    {
        string password = "";
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            else if (!char.IsControl(keyInfo.KeyChar) && keyInfo.KeyChar != ' ')
            {
                password += keyInfo.KeyChar;
                Console.Write("*");
            }
        } while (key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }
    private void Register()
    {
        Console.Write("Username: ");
        var username = Console.ReadLine();

        Console.Write("Password: ");
        var password = ReadPassword();

        var user = _authService.Register(username, password);

        if (user == null)
            Console.WriteLine("User already exists.");
        else
            Console.WriteLine("Registration successful.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void Login()
    {
        Console.Write("Username: ");
        var username = Console.ReadLine();

        Console.Write("Password: ");
        var password = ReadPassword();

        _currentUser = _authService.Login(username, password);

        if (_currentUser == null)
        {
            Console.WriteLine("Login failed.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        else
            Console.WriteLine("Login successful.");
    }

    private void CreatePost()
    {
        Console.Write("Write your post: ");
        var content = Console.ReadLine();

        _postService.CreatePost(_currentUser.Id, content);

        Console.WriteLine("Post created.");
    }
    private void DrawPostCard(Post post)
    {
        int width = 100;
        Console.WriteLine(" " + new string('_', width) + "");
        Console.ForegroundColor = ConsoleColor.Cyan;
        string authorLine = $"User: {GetUsername(post.AuthorId)}  |  Likes: {post.Likes.Count}";
        Console.WriteLine("|" + CenterText(authorLine, width) + "|");
        Console.ResetColor();

        Console.WriteLine("|" + new string('-', width) + "|");
        var contentLines = SplitText(post.Content, width - 4);
        foreach (var line in contentLines)
        {
            Console.WriteLine("|  " + line.PadRight(width - 4) + "  |");
        }
        // Comments
        Console.WriteLine("|" + new string('═', width) + "|");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("|  Comments:".PadRight(width) + "|");
        Console.ResetColor();
        foreach (var comment in post.Comments)
        {
            string user = GetUsername(comment.UserId);
            string commentText = $"  {user}: {comment.Text}";
            var commentLines = SplitText(commentText, width - 4);

            foreach (var line in commentLines)
                Console.WriteLine("|" + line.PadRight(width) + "|");
        }
        Console.WriteLine("|" + new string('_', width) + "|");
        Console.WriteLine();
    }
    private void CommentOnPost()
    {
        Console.Clear();
        var posts = _postService.GetFeed();

        if (posts.Count == 0)
        {
            Console.WriteLine("No posts available.");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < posts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {GetUsername(posts[i].AuthorId)}: {posts[i].Content}");
        }

        Console.Write("Select post number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) ||
            choice < 1 || choice > posts.Count)
        {
            Console.WriteLine("Invalid selection.");
            Console.ReadKey();
            return;
        }

        var selectedPost = posts[choice - 1];

        Console.Write("Write your comment: ");
        var content = Console.ReadLine();

        _commentService.AddComment(selectedPost, _currentUser, content);

        Console.WriteLine("Comment added!");
        Console.ReadKey();
    }
    private void ViewFeed()
    {
        Console.Clear();
        var posts = _postService.GetFeed();
        if (posts.Count == 0)
        {
            Console.WriteLine("No posts yet.");
        }
        else
        {
            foreach (var post in posts.OrderByDescending(p => p.CreatedAt))
            {
                DrawPostCard(post);
            }
        }
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
    private string GetUsername(Guid userId)
    {
        var user = _postService.GetUserById(userId);
        return user != null ? user.Username : "Unknown";
    }
    private List<string> SplitText(string text, int maxLineLength)
    {
        var lines = new List<string>();
        var words = text.Split(' ');
        string currentLine = "";

        foreach (var word in words)
        {
            if ((currentLine + " " + word).Trim().Length > maxLineLength)
            {
                lines.Add(currentLine);
                currentLine = word;
            }
            else
            {
                currentLine = (currentLine + " " + word).Trim();
            }
        }

        if (!string.IsNullOrEmpty(currentLine))
            lines.Add(currentLine);

        return lines;
    }
}