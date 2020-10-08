using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Trill.Api;
using Xunit;

namespace Trill.Tests.Integration.Controllers
{
    public class StoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public async Task get_story_should_return_not_found_given_invalid_id()
        {
            var storyId = Guid.NewGuid();
            var response = await _client.GetAsync($"api/stories/{storyId}");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        
        #region Arrange
        
        private readonly HttpClient _client;

        public StoriesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        #endregion
    }
}