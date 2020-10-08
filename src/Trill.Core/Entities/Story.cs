using System;
using System.Collections.Generic;
using System.Linq;
using Trill.Core.ValueObjects;

namespace Trill.Core.Entities
{
    public class Story
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public Author Author { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        public Story(Guid id, string title, string text, Author author, IEnumerable<string> tags, DateTime createdAt)
        {
            Id = id;
            Title = title.Trim();
            Text = text.Trim();
            Author = author;
            Tags = tags ?? Enumerable.Empty<string>();
            CreatedAt = createdAt;
        }
    }
}