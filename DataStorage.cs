using Org.BouncyCastle.Asn1.X509;

namespace GOV;

class DataStorage {
    public static async Task<UserEntry[]> GetMailingList(){
        // TODO setup database
        return await Task.Run(() => { 
            return new UserEntry[] {
                new UserEntry {
                    Name = "James Mok",
                    Email = "hoodieman8@gmail.com"
                }
            };
        });
    }
}

class UserEntry {
    public string Name { get; init; } = "";
    public string Email { get; init; } = "";
}