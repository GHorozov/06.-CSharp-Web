namespace Forum.Services.Interfaces
{
    using Forum.Services.EmailInfrastructure;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailSenderService
    {
        Task SendEmailAsync(
            string from,
            string fromName,
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment> attachments = null);
    }
}
