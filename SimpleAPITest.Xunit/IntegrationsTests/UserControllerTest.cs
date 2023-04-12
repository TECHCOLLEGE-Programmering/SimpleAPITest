using System.Net.Http;

namespace SimpleAPITest.Xunit.IntegrationsTests
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
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
            var response = await _httpClient.GetAsync("/api/User");
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
            var response = await _httpClient.GetAsync("/api/User/2");
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
            var response = await _httpClient.DeleteAsync("/api/User/1");
            //Assert.
            TestHelpers.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}