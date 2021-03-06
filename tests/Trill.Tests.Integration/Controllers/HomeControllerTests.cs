using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Trill.Api;
using Xunit;

namespace Trill.Tests.Integration.Controllers
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public async Task get_home_endpoint_should_return_message()
        {
            var response = await _client.GetAsync("api");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldBe("Trill API [test]");
        }
        
        #region Arrange
        
        private readonly HttpClient _client;

        public HomeControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("test");
                })
                .CreateClient();
        }
        
        #endregion
    }
}