namespace Trill.Core.Exceptions
{
    public class InvalidAuthorException : DomainException
    {
        public override string Code { get; } = "invalid_author";
        public string Author { get; }
        
        public InvalidAuthorException(string author) : base($"Invalid author: {author}")
        {
            Author = author;
        }
    }
}