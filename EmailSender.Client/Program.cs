using EmailSender;
using System;
namespace EmailClient;

class Program
{
    static async Task Main()
    {
        var email = new Email(new EmailParams
        {
            HostSmtp = "smtp.gmail.com",
            Port = 587,
            EnableSsl= true,
            SenderName = "ja",
            SenderEmail = "szemintest@gmail.com",
            SenderEmailPassword = "ewccmfmueiqqxtbu"
        });
        Console.WriteLine("wysyłanie maila ...");

        for (int i = 1; i <= 3; i++)
        {
            await email.Send(
                "Email testowy z aplikacji",
                "Nie wiem no",
                "marzenakucharczyk0@gmail.com");

            Console.WriteLine("mail wysłano");
        }
    }
}