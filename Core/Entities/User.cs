using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string Bio { get; set; } = "";
    }
}
