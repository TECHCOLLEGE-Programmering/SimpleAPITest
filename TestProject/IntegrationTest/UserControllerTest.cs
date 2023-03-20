namespace TestProject.IntergrationTests
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        public UserControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IUserRepository, TestUserRepository>();
                });
            }).CreateClient();
        }

        //[Theory]
        //[InlineData("/")]
        //[InlineData("/Index")]
        //[InlineData("/About")]
        //[InlineData("/Privacy")]
        //[InlineData("/Contact")]
        //public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    // Act
        //    var response = await client.GetAsync(url);

        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299
        //    Assert.Equal("text/html; charset=utf-8",
        //        response.Content.Headers.ContentType.ToString());
        //}
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
            Assert.Equal(expectedStatusCode, response.StatusCode);
            await TestHelpers.AssertResponseWithContentAsync(
                stopwatch, response, expectedStatusCode, expectedContent);
        }
    }
    public static class TestHelpers
    {
        private const string _jsonMediaType = "application/json";
        private const int _expectedMaxElapsedMilliseconds = 1000;
        private static readonly JsonSerializerOptions _jsonSerializerOptions =
            new() { PropertyNameCaseInsensitive = true };

        public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
            HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
            T expectedContent)
        {
            AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
            Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
            Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
                await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions));
        }
        private static void AssertCommonResponseParts(Stopwatch stopwatch,
            HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds < _expectedMaxElapsedMilliseconds);
        }
        public static StringContent GetJsonStringContent<T>(T model)
            => new(JsonSerializer.Serialize(model), Encoding.UTF8, _jsonMediaType);
    }
}