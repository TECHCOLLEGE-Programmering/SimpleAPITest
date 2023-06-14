using DomainModel;
using System.Net.Http;

namespace SimpleAPITest.Xunit.IntegrationsTests
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        private readonly string endpoint = "/api/User";
        public UserControllerTest()
        {
            _factory = new WebApplicationFactory<Program>();
            _httpClient = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IUserRepository, TestUserRepository>();
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetAllTest()
        {
            //Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = TestUserRepository.users;
            var stopwatch = Stopwatch.StartNew();
            //Act.
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            //Assert.
            await TestHelpers.AssertResponseWithContentAsync(
                stopwatch, response, expectedStatusCode, expectedContent);
        }
        [Fact]
        public async Task GetTest()
        {
            //Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            int indexOfExpectedContent = TestUserRepository.users.BinarySearch(new User { Id = 2 });
            var expectedContent = TestUserRepository.users[indexOfExpectedContent];

            var stopwatch = Stopwatch.StartNew();
            //Act.
            HttpResponseMessage response = await _httpClient.GetAsync($"{endpoint}/2");
            //Assert.
            await TestHelpers.AssertResponseWithContentAsync(
                stopwatch, response, expectedStatusCode, expectedContent);
        }
        [Fact]
        public async Task CreateTest()
        {
            //Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new User() { Id = 5, Name = "CreateTest", Email = "createTest@email.com" };

            var stopwatch = Stopwatch.StartNew();
            //Act.
            HttpContent httpContent = TestHelpers.GetJsonStringContent(expectedContent);
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, httpContent);
            //Assert.
            await TestHelpers.AssertResponseWithContentAsync(
                stopwatch, response, expectedStatusCode, expectedContent);
        }
        [Fact]
        public async Task UpdateTest() // TODO: Returns BadRequest
        {
            //Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            int indexOfExpectedContent = TestUserRepository.users.BinarySearch(new User { Id = 3 });
            var expectedContent = TestUserRepository.users[indexOfExpectedContent];
            expectedContent.Name = "UpdatedName";
            expectedContent.Email = "UpdatedEmail@email.com";

            var stopwatch = Stopwatch.StartNew();
            //Act.
            HttpContent httpContent = TestHelpers.GetJsonStringContent(expectedContent);
            HttpResponseMessage response = await _httpClient.PutAsync(endpoint, httpContent);
            //Assert.
            await TestHelpers.AssertResponseWithContentAsync(
                stopwatch, response, expectedStatusCode, expectedContent);
        }
        [Fact]
        public async Task DeleteTest()
        {
            //Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var id = TestUserRepository.users;
            var stopwatch = Stopwatch.StartNew();
            //Act.
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{endpoint}/1");
            //Assert.
            TestHelpers.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}