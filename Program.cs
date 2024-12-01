namespace GOV;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = CredentialManager.CongressAPIKey();
        ApiFacade getter = new ApiFacade(apiKey);
        BillRequest val = await getter.GetLatestBills();
        string title = "";
        string chamber = "";
        string number = "";
        
    }
}
