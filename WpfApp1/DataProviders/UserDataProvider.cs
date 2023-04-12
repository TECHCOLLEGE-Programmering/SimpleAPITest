using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DataProviders
{
    internal interface IUserDataProvider
    {
        Task<IEnumerable<User>?> GetAllAsync();
    }
    internal class APIUserDataProvider : IUserDataProvider
    {
        async Task<IEnumerable<User>?> IUserDataProvider.GetAllAsync()
        {
            //TODO get District from API and Add API as multi startup project
            throw new NotImplementedException();
        }
    }
    internal class UserTestDataProvider : IUserDataProvider
    {
        async Task<IEnumerable<User>?> IUserDataProvider.GetAllAsync()
        {
            await Task.Delay(10);

            return new List<User>() {
            new User {Id = 1,  Name = "Test1", Email = "Test1@TestCase.com"},
            new User {Id = 2,  Name = "Test2", Email = "Test2@TestCase.com"},
            new User {Id = 3,  Name = "Test3", Email = "Test3@TestCase.com"},
            new User {Id = 4,  Name = "Test4", Email = "Test4@TestCase.com"}
        };
    }
    }
}
