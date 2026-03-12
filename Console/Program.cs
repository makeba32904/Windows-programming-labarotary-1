using Core.Repositories;
using Core.Services;

var userRepo = new UserRepository();
var postRepo = new PostRepository();
var commentRepo = new CommentRepository();

var authService = new AuthService(userRepo);
var postService = new PostService(postRepo, userRepo);
var commentService = new CommentService(commentRepo, postRepo);

var ui = new UI(authService, postService, commentService);

ui.Start();