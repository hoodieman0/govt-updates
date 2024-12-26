using GOV.Sender;

namespace GOV;

class Program
{
    static async Task Main(string[] args)
    {
        ApiFacade getter = new ApiFacade(CredentialManager.CongressAPIKey);
        BillRequest val = await getter.GetLatestBills();
        foreach (Bill x in val.Bills){
            if (x.Number == "10"){
                
            }
        }
        MessageSender sender = new MessageSender();
        await sender.Send();
    }
}
