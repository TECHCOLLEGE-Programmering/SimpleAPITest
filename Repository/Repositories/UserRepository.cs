using DomainModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetUsers(string? name, string? email)
        {
            throw new NotImplementedException();
        }
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }
        public User InsertUser(User user)
        {
            throw new NotImplementedException();
        }
        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
