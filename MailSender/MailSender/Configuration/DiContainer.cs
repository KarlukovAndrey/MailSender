using MailSender.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Configuration
{
    public static class DiContainer
    {
        private static ServiceProvider _serviceProvider;
        static DiContainer()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<ConfigurationProvider>()
                .AddSingleton<SmtpClientService>()
                .AddTransient<MailSenderService>()
            .BuildServiceProvider();
        }

        public static T GetService<T>() => _serviceProvider.GetService<T>();
    }
}