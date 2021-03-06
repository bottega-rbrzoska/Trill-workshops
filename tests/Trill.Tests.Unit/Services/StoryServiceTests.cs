using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using Trill.Application.Commands;
using Trill.Application.Services;
using Trill.Core.Entities;
using Trill.Core.Exceptions;
using Trill.Core.Repositories;
using Xunit;

namespace Trill.Tests.Unit.Services
{
    public class StoryServiceTests
    {
        [Fact]
        public async Task get_story_details_should_succeed_given_valid_id()
        {
            // Arrange
            var story = new Story(Guid.NewGuid(), "test", "lorem ipsum",
                "test_user", new[] {"tag1"}, DateTime.UtcNow);
            _storyRepository.GetAsync(story.Id).Returns(story);

            // Act
            var dto = await _storyService.GetAsync(story.Id);
            
            // Assert
            dto.ShouldNotBeNull();
            await _storyRepository.Received(1).GetAsync(story.Id);
        }

        [Fact]
        public async Task add_should_succeed_given_valid_data()
        {
            // Arrange
            var command = new SendStory(Guid.NewGuid(), "test", "Lorem ipsum", "user1", new[] {"tag1", "tag2"});

            // Act
            await _storyService.AddAsync(command);

            // Assert
            await _storyRepository.Received(1).AddAsync(Arg.Is<Story>(x =>
                x.Id == command.Id &&
                x.Title == command.Title &&
                x.Text == command.Text &&
                x.Author == command.Author));
        }
        
        [Fact]
        public async Task add_should_fail_given_missing_author()
        {
            var command = new SendStory(Guid.NewGuid(), "test", "Lorem ipsum", default, new[] {"tag1", "tag2"});
            var exception = await Record.ExceptionAsync(async () => await _storyService.AddAsync(command));
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidAuthorException>();
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