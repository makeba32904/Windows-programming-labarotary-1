using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Interfaces
{
    public interface ILiking
    {
        void Like(Guid userId);
    }
}
