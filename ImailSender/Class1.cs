using System;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace EmailSender;

public class Email
{
    private SmtpClient _smtp;   //instance of smtp client 
    private MailMessage _mail;  //instance of mail message client

    private string _hostSmtp;
    private bool _enableSsl;
    private int _port;
    private string _senderEmail;
    private string _senderEmailPassword;
    private string _senderName;

    public Email(EmailParams emailParams)
    {
        _hostSmtp= emailParams.HostSmtp;
        _enableSsl = emailParams.EnableSsl;
        _port = emailParams.Port;
        _senderEmail= emailParams.SenderEmail;
        _senderEmailPassword= emailParams.SenderEmailPassword;
        _senderName= emailParams.SenderName;
    }
    public void Send (string subject,string body,string to)
    {
        _mail = new MailMessage();
        _mail.From = new MailAddress(_senderEmail, _senderName);
        _mail.To.Add(new MailAddress(to));
        _mail.Subject = subject;
        _mail.BodyEncoding = System.Text.Encoding.UTF8;
        _mail.SubjectEncoding = System.Text.Encoding.UTF8;
        _mail.Body = body;

        _smtp = new SmtpClient
        {
            Host = _hostSmtp,
            EnableSsl = _enableSsl,
            Port = _port,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_senderEmail, _senderEmailPassword)
        };
    }
}
