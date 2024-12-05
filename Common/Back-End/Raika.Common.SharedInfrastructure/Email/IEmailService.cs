namespace Raika.Common.SharedInfrastructure.Email
{
    public interface IEmailService
    {
        Task SendMailAsync(string from, string to, string subject, string body);
        Task SendMailAsync(string fromAddress, string fromName, string toAddress, string subject, string body);
    }
}
