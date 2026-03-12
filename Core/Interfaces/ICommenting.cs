using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Core.Entities;
namespace Core.Interfaces
{
    public interface ICommenting
    {
        void AddComment(Comment comment);
    }
}
