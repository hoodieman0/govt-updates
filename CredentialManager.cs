namespace GOV;

static class CredentialManager {
    public static string CongressAPIKey(){
        return File.ReadAllText(@"C:\Users\hoodi\Desktop\Coding\govt-updates\env");
    }
}