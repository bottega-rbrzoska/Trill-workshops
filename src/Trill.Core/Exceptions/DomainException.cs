using System;

namespace Trill.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public abstract string Code { get; }

        protected DomainException(string author) : base(author)
        {
        }
    }
}