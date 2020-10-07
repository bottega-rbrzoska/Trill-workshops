using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trill.Application.Services
{
    public interface IStoryService
    {
        Task<StoryDetailsDto> GetAsync(Guid storyId);
        Task<IEnumerable<StoryDto>> BrowseAsync(string author = null);
        Task AddAsync(SendStory command);
    }
}