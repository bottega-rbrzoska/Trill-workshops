using System;
using Microsoft.Extensions.Logging;

namespace Trill.Api.Services
{
    public class Messenger : IMessenger
    {
        private readonly ILogger<Messenger> _logger;
        private readonly Guid _id = Guid.NewGuid();

        public Messenger(ILogger<Messenger> logger)
        {
            _logger = logger;
            _logger.LogInformation($"Created a messenger with ID: {_id}");
        }

        public string GetMessage() => $"Hello [ID: {_id}]";
    }
}