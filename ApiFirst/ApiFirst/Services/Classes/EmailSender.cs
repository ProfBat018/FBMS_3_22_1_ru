﻿
using Microsoft.AspNetCore.Identity.UI.Services;


namespace ApiFirst.Services.Classes;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;
    private readonly SmtpClient _smtpClient;
    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
      
        _smtpClient = new SmtpClient(_configuration["Email:Host"])
        {
            Port = int.Parse(_configuration["Email:Port"]),
            Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]),
            EnableSsl = true
        };
    }



    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Email:Username"]),
            Subject = subject,
            Body = message,
            IsBodyHtml = isHtml
        };

        mailMessage.To.Add(email);

        if (attachments != null)
        {
            foreach (var attachment in attachments)
            {
                mailMessage.Attachments.Add(new Attachment(attachment.FullName));
            }
        }

        await _smtpClient.SendMailAsync(mailMessage);

    }
}
