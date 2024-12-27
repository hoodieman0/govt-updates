using GOV.Messages;
using GOV.Sender;

namespace GOV;

class Program
{
    static async Task Main(string[] args)
    {
        ApiFacade getter = new ApiFacade(CredentialManager.CongressAPIKey);
        BillRequest val = await getter.GetLatestBills();
        SummaryRequest v1 = await getter.GetSummaryFromYesterday();
        SummaryRequest v2 = await getter.GetSummaryFromDay(DateTime.Today.AddDays(-30));
        MessageSender sender = new MessageSender();
        await sender.Send(new Message(){
            BillInfo = val
        });
    }
}
