using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfApp1.DataProviders
{
    internal interface IUserDataProvider
    {
        Task<IEnumerable<User>?> GetAllAsync();
        Task<bool> Delete(User user);
        Task<bool> Add(User user);
    }
    internal class ApiUserDataProvider : IUserDataProvider
    {
        Uri _baseAdress = new Uri("https://localhost:7041/");
        string _endpointAdress = "api/User";
        private static StringContent GetJsonStringContent<T>(T model)
            => new(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        

        async Task<IEnumerable<User>?> IUserDataProvider.GetAllAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.GetAsync(_endpointAdress);
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

        public async Task<bool> Add(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpContent httpContent = GetJsonStringContent(user);
                HttpResponseMessage response = await client.PostAsync(_endpointAdress, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
                return false;
            }
        }
        public async Task<bool> Delete(User user)
        {
            string url = $"{_endpointAdress}/{user.Id}";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                Console.WriteLine(url);
                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
                return false;
            }
        }
    }
    internal class UserTestDataProvider : IUserDataProvider
    {
        List<User> _users = new List<User>();
        public async Task<bool> Delete(User user)
        {
            _users.Remove(user);
            return true;
        }
        public async Task<bool> Add(User user)
        {
            _users.Add(user);
            return true;
        }
        async Task<IEnumerable<User>?> IUserDataProvider.GetAllAsync()
        {
            _users = new List<User>() {
                new User {Id = 1,  Name = "Test1", Email = "Test1@TestCase.com"},
                new User {Id = 2,  Name = "Test2", Email = "Test2@TestCase.com"},
                new User {Id = 3,  Name = "Test3", Email = "Test3@TestCase.com"},
                new User {Id = 4,  Name = "Test4", Email = "Test4@TestCase.com"}
            };
            return _users;
        }
    }
}
