using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Configuration
{
    public class MailSettings
    {

        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string Password { get; set; }

        public string SmtpServer { get; set; }

        public int Port { get; set; }
    }
}
