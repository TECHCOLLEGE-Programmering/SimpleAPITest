using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
            IEnumerable<User> users = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7041/");
                HttpResponseMessage response = await client.GetAsync("api/User");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();

                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
            return null;
        }
    }
    internal class UserTestDataProvider : IUserDataProvider
    {
        async Task<IEnumerable<User>?> IUserDataProvider.GetAllAsync()
        {
            return new List<User>() {
            new User {Id = 1,  Name = "Test1", Email = "Test1@TestCase.com"},
            new User {Id = 2,  Name = "Test2", Email = "Test2@TestCase.com"},
            new User {Id = 3,  Name = "Test3", Email = "Test3@TestCase.com"},
            new User {Id = 4,  Name = "Test4", Email = "Test4@TestCase.com"}
        };
    }
    }
}
