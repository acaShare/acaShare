using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public User GetUser(int userId)
        {
            return _uow.Users.FindById(userId);
        }

        public void RegisterNewUser(User newUser)
        {
            _uow.Users.Add(newUser);
            _uow.SaveChanges();
        }

        public User FindByIdentityUserId(string identityUserId)
        {
            return _uow.Users.FindByIdentityUserId(identityUserId);
        }
    }
}
