using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Trill.Api.Services
{
    public class NotificationsService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationsService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var messenger = scope.ServiceProvider.GetRequiredService<IMessenger>();
                    var message = messenger.GetMessage();
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}