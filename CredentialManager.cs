namespace GOV;

static class CredentialManager {
    static StreamReader reader = new StreamReader(@"env");
    public static string CongressAPIKey { get; }
    public static string GmailEmailName { get; }
    public static string GmailApiKey { get; }
    static CredentialManager(){
        CongressAPIKey = reader.ReadLine() ?? "N/A";
        GmailEmailName = reader.ReadLine() ?? "N/A";
        GmailApiKey = reader.ReadLine() ?? "N/A";
        reader.Close();
    }
}