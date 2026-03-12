using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;
namespace Core.Entities
{
    public class Comment : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; } = "";
    }
}
