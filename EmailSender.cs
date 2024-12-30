using GOV.Messages;
using MailKit.Net.Smtp;
using MimeKit;

namespace GOV.Email;

class EmailSender {
    string senderEmail;
    string senderKey;
    public EmailSender(string senderEmail, string senderKey){
        this.senderEmail = senderEmail;
        this.senderKey = senderKey;
    }
    // display strings must be formatted as html
    // TODO null checking of msg
    public async Task Send(EmailMessage msg){
        Task<UserEntry[]> queryList = DataStorage.GetMailingList();        
        using (SmtpClient client = new SmtpClient()){
            client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            client.Authenticate(senderEmail, senderKey);

            UserEntry[] mailList = await queryList;
            foreach(UserEntry user in mailList){
                MimeMessage mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("KYC", senderEmail));
                mailMessage.Subject = msg.Title;
                mailMessage.Body = new TextPart("html"){
                    Text = msg.Text
                };

                mailMessage.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
                await client.SendAsync(mailMessage); // must be awaited, multithread for bulk emails
            }
            await client.DisconnectAsync(true);
        }
    }
}