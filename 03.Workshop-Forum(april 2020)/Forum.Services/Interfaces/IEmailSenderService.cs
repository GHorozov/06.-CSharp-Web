namespace Forum.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Forum.Services.EmailInfrastructure;

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
