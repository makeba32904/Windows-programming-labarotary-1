using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Interfaces
{
    public interface IFollowable
    {
        void Follow(Guid userId);
    }
}
