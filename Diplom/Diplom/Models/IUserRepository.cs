using System;
using System.Linq;
using Infrastructure;
using MongoDB.Driver.Linq;

namespace Diplom.Models
{
    public interface IUserRepository
    {
        void Save(User user);

        IQueryable<User> Users { get; }
    }

    public class UserRepository : IUserRepository
    {
        public void Save(User user)
        {
            MongoHelper.GetCollectionOf<User>().Save(user);
        }

        public IQueryable<User> Users
        {
            get { return MongoHelper.GetCollectionOf<User>().AsQueryable(); }
        }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }

}