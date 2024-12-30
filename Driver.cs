using System.Security;
using GOV.Email;
using GOV.Messages;

namespace GOV;

class Driver {
    ApiFacade api = new ApiFacade(CredentialManager.CongressAPIKey);
    EmailSender sender = new EmailSender(
        CredentialManager.GmailEmailName, 
        CredentialManager.GmailApiKey
        );
    public async Task Start(){
        DateTime date = DateTime.Today.AddDays(-1);
        SummaryRequest sum = await api.GetSummaryFromDay(date);
        string title = $"Know Your Congress {date:dd MMM yyyy}";
        string text = "<p>What happened on Captial Hill yesterday?</p>";
        int? count = sum.Pagination.Count;
        if (count == null || count == 0){
            text += "<p>There are no significant updates reported.</p>";
        }
        else if (count < 6) { // all detailed summaries
            List<Task<string>> billTasks = new List<Task<string>>();
            foreach (Summary s in sum.Summaries){
                billTasks.Add(DetailFormat(s));
            }
            foreach (Task<string> t in billTasks){
                text += await t;
            }
        }
        else { // 5 detailed summaries, rest titles only
            int[] indexes = new int[5];
            Random r = new Random();
            for (int i = 0; i < indexes.Length; i++)
            {
                int temp;
                do {
                    temp = Math.Abs((int) r.NextInt64() % (int) sum.Pagination.Count);
                }
                while (indexes.Contains(temp));
                indexes[i] = temp;
            }
            
            List<Task<string>> detailTasks = new List<Task<string>>();
            List<Task<string>> titleTasks = new List<Task<string>>();
            for (int tracker = 0; tracker < count; ){
                if (sum == null) break;
                Task<SummaryRequest> nextSumTask = api.GetNextSummaryList(sum);
                foreach(Summary s in sum.Summaries){
                    if (indexes.Contains(tracker)) detailTasks.Add(DetailFormat(s));
                    else titleTasks.Add(TitleFormat(s));
                    tracker++;
                }
                sum = await nextSumTask;
            }

            text += "<p>Here are some randomly selected bills:</p>";
            foreach(Task<string> task in detailTasks){
                text += await task;
            }
            text += "<p>Here are the rest of the bills from the day:<ul>";
            foreach(Task<string> task in titleTasks){
                text += await task;
            }
            text += "</ul></p>";
        }
        
        text += $"""
            <footer>
            Summaries provided by the Congressional Research Service.<br>
            Know Your Congress | 
            <a href="https://github.com/hoodieman0">GitHub</a> | 
            <a href="mailto:hoodz.api.test@gmail.com">Contact</a>
            </footer>
            """;
        
        EmailMessage msg = new EmailMessage(){
            Title = title,
            Text = text
        };
        await sender.Send(msg);
    }

    public async Task<string> DetailFormat(Summary s){
        Task<CosponserRequest> cosponsorTask = api.GetBillCosponsors(s.BillDetails.Congress, s.BillDetails.Type, s.BillDetails.Number);
        // The title of the bill is sometimes sent with the summary text.
        string result = $"""
        <p>
        Bill #{s.BillDetails.Number}<br>
        
        Summary:<br>
        {s.Text}<br>
        Cosponsors:<br>
        """;
        
        result += "<ul>";
        CosponserRequest req = await cosponsorTask;
        if (req == null || req.Cosponsors.Count == 0) result += "<li>Cosponsers not found.</li>";
        else
            foreach (Cosponsor c in req.Cosponsors){
                string prefix = c.FullName.Substring(0, 4);
                string district = '[' + c.FullName.Split('[')[1];
                result += $"<li>{prefix} {c.FirstName} {c.LastName} {district}</li>";
            }
        result += "</ul>";

        result += $"""
        Origin Chamber: {s.BillDetails.OriginChamber}<br>
        Current Chamber: {s.CurrentChamber}<br>
        Latest Update: {
        TimeZoneInfo.ConvertTime(s.UpdateDate ?? DateTime.MinValue, 
        TimeZoneInfo.FindSystemTimeZoneById("EST")):h:mm tt} EST<br>
        Update Details: {CongressAPI.ActionDictionary[s.VersionCode]}<br>
        </p>
        """;
        return result;
    }

    public Task<string> TitleFormat(Summary s){
        string result = $"""
        <li>
            <p>
                Bill #{s.BillDetails.Number}<br>
                Title: {s.BillDetails.Title}<br>
                Update Details: {CongressAPI.ActionDictionary[s.VersionCode]}<br>
            </p>
        </li>
        """;
        return Task.FromResult(result);
    }
}