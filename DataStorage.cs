using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace GOV;

class DataStorage {
    public static async Task<UserEntry[]> GetMailingList(){
        FirestoreClientBuilder builder = new FirestoreClientBuilder { ApiKey = CredentialManager.FirestoreApiKey };
        FirestoreDb db = await FirestoreDb.CreateAsync(CredentialManager.FirestoreId, builder.Build());
        CollectionReference reference = db.Collection("users");
        QuerySnapshot snap = await reference.GetSnapshotAsync();
        List<UserEntry> entries = new List<UserEntry>();
        foreach (DocumentSnapshot s in snap.Documents){
           entries.Add(s.ConvertTo<UserEntry>());
        }
        return entries.ToArray();
    }
}

[FirestoreData]
class UserEntry {
    [FirestoreProperty]
    public string FirstName { get; init; } = "";
    [FirestoreProperty]
    public string LastName { get; init; } = "";
    [FirestoreProperty]
    public string Email { get; init; } = "";
}