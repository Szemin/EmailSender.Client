using System;
namespace EmailSender;

public class Email
{
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
}
