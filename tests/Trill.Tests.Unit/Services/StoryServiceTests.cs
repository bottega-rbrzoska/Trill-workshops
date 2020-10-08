using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using Trill.Application.Services;
using Trill.Core.Entities;
using Trill.Core.Repositories;
using Xunit;

namespace Trill.Tests.Unit.Services
{
    public class StoryServiceTests
    {
        [Fact]
        public async Task get_story_details_should_succeed_given_valid_id()
        {
            var story = new Story(Guid.NewGuid(), "test", "lorem ipsum",
                "test_user", new[] {"tag1"}, DateTime.UtcNow);
            
            _storyRepository.GetAsync(story.Id).Returns(story);

            var dto = await _storyService.GetAsync(story.Id);
            
            dto.ShouldNotBeNull();
            await _storyRepository.Received().GetAsync(story.Id);
        }

        #region Arrange

        private readonly IStoryService _storyService;
        private readonly IStoryRepository _storyRepository;
        private readonly ILogger<StoryService> _logger;

        public StoryServiceTests()
        {
            _storyRepository = Substitute.For<IStoryRepository>();
            _logger = Substitute.For<ILogger<StoryService>>();
            _storyService = new StoryService(_storyRepository, _logger);
        }

        #endregion
    }
}