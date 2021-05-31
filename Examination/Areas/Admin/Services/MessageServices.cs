using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using System.Threading.Tasks;

namespace Examination.Areas.Admin.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {

        public AuthMessageSender(IOptions<SMSoptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public SMSoptions Options { get; }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            //var str = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"];
            //var client = new Client(creds: new Nexmo.Api.Request.Credentials
            //{
            //    ApiKey = "fcc547d2",
            //    ApiSecret = "v1I43BA7wvXq4W8H"
            //});
       
            //var start = client.NumberVerify.Verify(new NumberVerify.VerifyRequest
            //{   
            //    workflow_id="6",
            //    number = "998946180585",
            //    brand = "Verify",
            //    code_length = "4",
            //        });
            //var result = client.NumberVerify.Check(new NumberVerify.CheckRequest
            //{
            //    request_id = start.request_id,
            //    code = "5455454"
            //});
           
            //var m= start.status;        
         

            //var results = SMS.Send(new SMS.SMSRequest
            //{
            //    from = "998946113900",
            //    to = "998946180585",
            //    text = "12345"
            //});
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
