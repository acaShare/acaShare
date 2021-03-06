﻿using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories.UserRelated
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByIdentityUserId(string identityUserId);
        bool IsMaterialFavorite(string identityUserId, int materialId);
    }
}
