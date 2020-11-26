using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Configuration
{
    public class ConfigurationProvider
    {
        IConfigurationRoot _configuration;
        public ConfigurationProvider()
        {
            _configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
        }
        public MailSettings GetMailSettings() => _configuration.GetSection("MailSettings").Get<MailSettings>();
    }
}