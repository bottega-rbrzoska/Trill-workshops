using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trill.Core.Entities;
using Trill.Core.Repositories;

namespace Trill.Tests.Integration.Common
{
    internal class TestStoryRepository : IStoryRepository
    {
        // This is not thread safe (in the real world scenario use e.g. ConcurrentDictionary)
        private static readonly ISet<Story> Stories = new HashSet<Story>();

        public Task<Story> GetAsync(Guid id) => Task.FromResult(Stories.SingleOrDefault(p => p.Id == id));

        public Task<IEnumerable<Story>> BrowseAsync(string author = null)
            => Task.FromResult(Stories.Where(x => author is null || x.Author == author));

        public async Task AddAsync(Story story)
        {
            if (await GetAsync(story.Id) is {})
            {
                throw new Exception($"Story with ID: '{story.Id}' already exists.");
            }

            Stories.Add(story);
        }
    }
}