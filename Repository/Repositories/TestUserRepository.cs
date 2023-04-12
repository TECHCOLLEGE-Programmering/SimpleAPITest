using DomainModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TestUserRepository : IUserRepository
    {
        private static List<User> users = new List<User>() { 
            new User {Id = 1,  Name = "Test1", Email = "Test1@TestCase.com"},
            new User {Id = 2,  Name = "Test2", Email = "Test2@TestCase.com"},
            new User {Id = 3,  Name = "Test3", Email = "Test3@TestCase.com"},
            new User {Id = 4,  Name = "Test4", Email = "Test4@TestCase.com"}
        };
        public IEnumerable<User> GetUsers()
        {
            return users;
        }
        public User GetUser(int id)
        {
            return users.Single(x => x.Id == id);
        }
        public User InsertUser(User user)
        {
            Debug.WriteLine(user.Id);
            if (users.Any(item => item.Id == user.Id))
            {
                return null;
            }
            users.Add(user);
            return users.Last();
        }
        public User UpdateUser(User user)
        {
            if (!users.Any(item => item.Id == user.Id))
            {
                return null;
            }
            User ToBeUpdated = users.Single(x => x.Id == user.Id);
            users.Remove(ToBeUpdated);
            users.Add(user);
            return users.Last();
        }
        public void DeleteUser(int id)
        {
            User ToBeDeleted = users.Single(x => x.Id == id);
            users.Remove(ToBeDeleted);
        }
    }
}
