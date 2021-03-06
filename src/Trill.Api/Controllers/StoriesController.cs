using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trill.Application.Commands;
using Trill.Application.DTO;
using Trill.Application.Services;

namespace Trill.Api.Controllers
{
    public class StoriesController : BaseController
    {
        private readonly IStoryService _storyService;

        public StoriesController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [HttpGet]
        public async Task<ActionResult<StoryDto>> Get(string author)
            => Ok(await _storyService.BrowseAsync(author));
    
        [HttpGet("{storyId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<StoryDetailsDto>> Get(Guid storyId)
        {
            var story = await _storyService.GetAsync(storyId);
            if (story is null)
            {
                return NotFound();
            }

            return Ok(story);
        }

        [HttpPost]
        public async Task<ActionResult> Post(SendStory command)
        {
            await _storyService.AddAsync(command);

            return CreatedAtAction(nameof(Get), new {storyId = command.Id}, null);
        }
    }
}