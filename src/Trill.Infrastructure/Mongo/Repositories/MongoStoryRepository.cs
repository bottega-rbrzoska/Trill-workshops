using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Trill.Core.Entities;
using Trill.Core.Repositories;

namespace Trill.Infrastructure.Mongo.Repositories
{
    internal class MongoStoryRepository : IStoryRepository
    {
        private readonly IMongoDatabase _database;

        public MongoStoryRepository(IMongoDatabase database)
        {
            _database = database;
        }
        
        public Task<Story> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Story>> BrowseAsync(string author = null)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Story story)
        {
            throw new NotImplementedException();
        }
    }
}