using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shouldly;
using Trill.Api;
using Trill.Application.Commands;
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

        [Fact]
        public async Task post_send_story_should_return_created_and_location_header()
        {
            var command = new SendStory(Guid.NewGuid(), "test", "Lorem ipsum",
                "test_user", new[] {"tag1", "tag2"});
            var payload = GetPayload(command);
            
            var response = await _client.PostAsync("api/stories", payload);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            var location = response.Headers.Location;
            location.ShouldNotBeNull();
            location.ToString().ShouldBe($"http://localhost/api/Stories/{command.Id}");
        }

        private static StringContent GetPayload<T>(T value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        
        #region Arrange
        
        private readonly HttpClient _client;

        public StoriesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        #endregion
    }
}