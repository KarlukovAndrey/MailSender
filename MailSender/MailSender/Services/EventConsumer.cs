using MassTransit;
using EmailExchanging;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class EventConsumer : IConsumer<EmailInputModel>
    {
        MailSenderService emaiSenderService;
        public async Task Consume(ConsumeContext<EmailInputModel> context)
        {
            emaiSenderService = new MailSenderService();
            emaiSenderService.SendEmail(context.Message);
        }
    }
}
