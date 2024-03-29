﻿using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace FilmStore.WEB.Areas.Identity
{
  public class EmailSender : IEmailSender
  {
    public EmailSender()
    {
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      MimeMessage emailMessage = new MimeMessage();

      emailMessage.From.Add(new MailboxAddress("Администрация сайта", "info.FilmStore@gmail.com"));
      emailMessage.To.Add(new MailboxAddress("", email));
      emailMessage.Subject = subject;
      emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html){ Text = htmlMessage };

      using (var client = new SmtpClient())
      {
        await client.ConnectAsync("smtp.gmail.com", 25, false);
        await client.AuthenticateAsync("info.FilmStore@gmail.com", "FilmStorePassword11");
        await client.SendAsync(emailMessage);

        await client.DisconnectAsync(true);
      }
    }
  }
}
