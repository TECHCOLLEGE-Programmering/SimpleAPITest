using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
        public IEnumerable<User> GetUsers(string? name, string? email);
        public User GetUser(int id);
        public User InsertUser(User user);
        public User UpdateUser(User user);
        public void DeleteUser(int id);
    }
}
