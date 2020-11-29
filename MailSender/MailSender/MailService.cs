using System;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Configuration;
using MailSender.Services;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MailSender
{
    public class MailService : BackgroundService
    {
        private readonly ILogger<MailService> _logger;
        private SmtpClientService _client;
        private IBusControl _busControl;
        private MailSenderService _mailSenderService;

        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;          
            _client = DiContainer.GetService<SmtpClientService>();
            _mailSenderService = DiContainer.GetService<MailSenderService>();
            _busControl = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.ReceiveEndpoint("mail-messages", e =>
                {
                    e.Consumer<EventConsumer>();
                });
            });

        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
        
            _busControl.Start();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _busControl.Stop();
            _client.CloseSmtpConnection();
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
