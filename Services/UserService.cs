using DomainModel;
using Service.Interfaces;
using Repository.Repositories;
using Repository.Interfaces;
using System.Diagnostics;

namespace Services
{
    public class UserService : ICustomService<User>
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<User> GetAll(string? name, string? email)
        {
            try
            {
                return _userRepository.GetUsers(name, email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        public User Get(int id)
        {
            try
            {
                return _userRepository.GetUser(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        public User Insert(User entity)
        {
            try
            {
                return _userRepository.InsertUser(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public User Update(User entity)
        {
            try
            {
                return _userRepository.UpdateUser(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        public void Delete(int id)
        {
            try
            {
                _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}