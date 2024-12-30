namespace GOV;

static class CredentialManager {
    static StreamReader reader = new StreamReader(@"env");
    public static string CongressAPIKey { get; }
    public static string GmailEmailName { get; }
    public static string GmailApiKey { get; }
    public static string FirestoreId { get; }
    public static string FirestoreApiKey { get; }
    static CredentialManager(){
        CongressAPIKey = reader.ReadLine() ?? "N/A";
        GmailEmailName = reader.ReadLine() ?? "N/A";
        GmailApiKey = reader.ReadLine() ?? "N/A";
        FirestoreId = reader.ReadLine() ?? "N/A";
        FirestoreApiKey = reader.ReadLine() ?? "N/A";
        reader.Close();
    }
}