using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
namespace EmailSender2;
public class Email
{
    private MailMessage _mail;  //instancja klasy MailMessage dzięki której można używać From/To/Subject/Body/Encoding
    private SmtpClient _smtp;  //instancja klasy SmtpClient dzięki której można przypisać wartości smtp

    private string _hostSmtp;
    private bool _enableSsl;
    private int _port;
    private string _senderName;
    private string _senderEmail;
    private string _senderEemailPassword;

    public Email(EmailParams emailParams)  //konstruktor przyjmujący jako parametr metode EmailParams 
    {
        _hostSmtp = emailParams.HostSmtp;
        _enableSsl = emailParams.Ssl;
        _port = emailParams.Port;
        _senderName = emailParams.SenderName;
        _senderEmail = emailParams.SenderEmail;
        _senderEemailPassword = emailParams.SenderEmailPassword;
    }
    public async Task Send(string subject, string body ,string to)
    {
        _mail = new MailMessage();  //instancja klasy MailMessage
        _mail.From = new MailAddress(_senderEmail, _senderName);   //pobiera lub ustawia adres mailowy nadawcy
        _mail.To.Add(new MailAddress(to)); // pobiera kolekcje adresów zawierających odbiorcę wiadomości mailowej
        _mail.Subject = subject;   //pobiera lub ustawia temat wiadomości
        _mail.BodyEncoding = System.Text.Encoding.UTF8; //enkodowanie wiadomości
        _mail.SubjectEncoding = System.Text.Encoding.UTF8; //enkodowanie tematu
        _mail.Body = body;  // treść wiadomości mailowej

        _smtp = new SmtpClient 
        {
            Host= _hostSmtp,  //pobiera lub ustawia adres IP hosta używając SMTP
            EnableSsl = _enableSsl,  //podaj gdzie SMTPClient używa SSL do szyfrowania połączenia
            Port = _port,  // ustawia port dla SMTP
            DeliveryMethod = SmtpDeliveryMethod.Network, // określa sposób wychodzących wiadomości, precyzuje jak email zostanie dostarczony, czyli przez sieć do SMTP serwera
            UseDefaultCredentials = false, //ustawia referenfcje, zwraca true gdy domyślna referencja,otherwise false
            Credentials = new NetworkCredential(_senderEmail, _senderEemailPassword) //ustawia lub pobiera referencje służące do uwierzytelniania nadawcy, mail i hasło do maila
        };

        _smtp.SendCompleted += OnSendCompleted;  //pointer do zwolnienia pamięci po wykonanej operacji

        await _smtp.SendMailAsync(_mail);  // wysyła wiadomość na serwer SMTP dla dostarczenia asynchronicznej operacji i zwraca taska (trzeba zmienic metodę na await Task)

    }
    private void OnSendCompleted(object sender, AsyncCompletedEventArgs e) //metoda utworzona z pointera wyżej
    {
        _smtp.Dispose(); //zwalnianie zasobów z _smtp
        _mail.Dispose(); //zwalnianie zasobow z _mail
    }
}
