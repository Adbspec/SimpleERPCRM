namespace ERP.Services.Interfaces
{
    public interface IEmailSenderService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="receiverEmail">The email address of the recipient.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body content of the email.</param>
        /// <param name="mimeType">The MIME type of the email body. Default is "text/html".</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SendEmailAsync(string email, string subject, string message, string mimeType="text/html");
    }
}
