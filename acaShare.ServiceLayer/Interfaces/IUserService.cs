using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IUserService
    {
        User GetUser(int userId);
        void RegisterNewUser(User newUser);
        User FindByIdentityUserId(string identityUserId);
        bool IsMaterialFavorite(Material material, string identityUserId);
    }
}
