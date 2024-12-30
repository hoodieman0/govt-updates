using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace GOV;

// "facade" to handle API logic
class ApiFacade {
    CongressAPI congressAPI;
    public ApiFacade(string congressKey){
        congressAPI = new CongressAPI(congressKey);
    }

    #region Congress API
    public async Task<int> GetCongressNumber(){
        return await congressAPI.GetCurrentCongressNumber();
    }
    public async Task<BillRequest> GetLatestBills() {
        return await congressAPI.GetLatestBills();
    }

    public async Task<SummaryRequest> GetSummaryFromYesterday(){
        return await congressAPI.GetDaySummary(DateTime.Today.AddDays(-1));
    }
    public async Task<SummaryRequest> GetSummaryFromDay(DateTime day){
        return await congressAPI.GetDaySummary(day);
    }
    public async Task<SummaryRequest> GetNextSummaryList(SummaryRequest s){
        return await congressAPI.GetNextSummaryList(s);
    }
    public async Task<CosponserRequest> GetBillCosponsors(int congress, string billType, string billNumber){
        return await congressAPI.GetBillCosponsors(congress, billType, billNumber);
    }

    
    #endregion
}

class CongressAPI {
    public static readonly Dictionary<string, string> ActionDictionary = new Dictionary<string, string>
    {
        { "00", "Introduced to Chamber" },
        { "01", "Reported to Senate with amendment(s)" },
        { "02", "Reported to Senate amended, 1st committee reporting" },
        { "03", "Reported to Senate amended, 2nd committee reporting" },
        { "04", "Reported to Senate amended, 3rd committee reporting" },
        { "07", "Reported to House" },
        { "08", "Reported to House, Part I" },
        { "09", "Reported to House, Part II" },
        { "12", "Reported to Senate without amendment, 1st committee reporting" },
        { "13", "Reported to Senate without amendment, 2nd committee reporting" },
        { "17", "Reported to House with amendment(s)" },
        { "18", "Reported to House amended, Part I" },
        { "19", "Reported to House amended Part II" },
        { "20", "Reported to House amended, Part III" },
        { "21", "Reported to House amended, Part IV" },
        { "22", "Reported to House amended, Part V" },
        { "25", "Reported to Senate" },
        { "28", "Reported to House without amendment, Part I" },
        { "29", "Reported to House without amendment, Part II" },
        { "31", "Reported to House without amendment, Part IV" },
        { "33", "Laid on table in House" },
        { "34", "Indefinitely postponed in Senate" },
        { "35", "Passed Senate amended" },
        { "36", "Passed House amended" },
        { "37", "Failed of passage in Senate" },
        { "38", "Failed of passage in House" },
        { "39", "Senate agreed to House amendment with amendment" },
        { "40", "House agreed to Senate amendment with amendment" },
        { "43", "Senate disagreed to House amendment" },
        { "44", "House disagreed to Senate amendment" },
        { "45", "Senate receded and concurred with amendment" },
        { "46", "House receded and concurred with amendment" },
        { "47", "Conference report filed in Senate" },
        { "48", "Conference report filed in House" },
        { "49", "Became Public Law" },
        { "51", "Line item veto by President" },
        { "52", "Passed Senate amended, 2nd occurrence" },
        { "53", "Passed House" },
        { "54", "Passed House, 2nd occurrence" },
        { "55", "Passed Senate" },
        { "56", "Senate vitiated passage of bill after amendment" },
        { "58", "Motion to recommit bill as amended by Senate" },
        { "59", "House agreed to Senate amendment" },
        { "60", "Senate agreed to House amendment with amendment, 2nd occurrence" },
        { "62", "House agreed to Senate amendment with amendment, 2nd occurrence" },
        { "66", "House receded and concurred with amendment, 2nd occurrence" },
        { "70", "House agreed to Senate amendment without amendment" },
        { "71", "Senate agreed to House amendment without amendment" },
        { "74", "Senate agreed to House amendment" },
        { "77", "Discharged from House committee" },
        { "78", "Discharged from Senate committee" },
        { "79", "Reported to House without amendment" },
        { "80", "Reported to Senate without amendment" },
        { "81", "Passed House without amendment" },
        { "82", "Passed Senate without amendment" },
        { "83", "Conference report filed in Senate, 2nd conference report" },
        { "86", "Conference report filed in House, 2nd conference report" },
        { "87", "Conference report filed in House, 3rd conference report" }
    };
    string apiKey = "";
    public CongressAPI(string key){
        apiKey = key;
    }

    public async Task<int> GetCurrentCongressNumber(){
        const string CONGRESS_URI = "https://api.congress.gov/v3/congress/current?api_key=";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(CONGRESS_URI + apiKey);
        string? json = await response.Content.ReadAsStringAsync();
        CongressRequest? currentCongress = JsonSerializer.Deserialize<CongressRequest>(json);
        return currentCongress?.Congress?.Number ?? throw new Exception("Couldn't get congressional number");
    }

    public async Task<BillRequest> GetLatestBills(){
        const string BILL_URI = "https://api.congress.gov/v3/law/";
        HttpClient client = new HttpClient();
        int congressNum = await GetCurrentCongressNumber();
        string requestUri = BILL_URI + congressNum + "?sort=updateDate+desc&api_key=" + apiKey;
        HttpResponseMessage response = await client.GetAsync(requestUri);
        string? json = await response.Content.ReadAsStringAsync();
        BillRequest? billData = JsonSerializer.Deserialize<BillRequest>(json);
        return billData ?? throw new Exception();
    }

    public async Task<SummaryRequest> GetDaySummary(DateTime day){
        string summaryUri = $"https://api.congress.gov/v3/summaries?"
        + $"fromDateTime={day.Date:yyyy-MM-ddThh:mm:ssZ}&toDateTime="
        + $"{day.Date.AddDays(1):yyy-MM-ddThh:mm:ssZ}&sort=updateDate+asc"
        + $"&api_key={apiKey}"; 
        
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(summaryUri);
        string? json = await response.Content.ReadAsStringAsync();
        SummaryRequest? daySummary = JsonSerializer.Deserialize<SummaryRequest>(json);
        return daySummary ?? throw new Exception("Couldn't deserialize summary");
    }

    public async Task<SummaryRequest> GetNextSummaryList(SummaryRequest s){
        if (s.Pagination.Next == null || s.Pagination.Next == "") return new SummaryRequest();
        string summaryUri = s.Pagination.Next.Replace(' ', '+') + $"&api_key={apiKey}";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(summaryUri);
        string? json = await response.Content.ReadAsStringAsync();
        SummaryRequest? daySummary = JsonSerializer.Deserialize<SummaryRequest>(json);
        return daySummary ?? throw new Exception("Couldn't deserialize summary");
    }
    public async Task<CosponserRequest> GetBillCosponsors(int congress, string billType, string billNumber){
        string summaryUri = $"https://api.congress.gov/v3/bill/{congress}"
        + $"/{billType.ToLower()}/{billNumber}/cosponsors?api_key={apiKey}"; 
        // bill type must be lowercase, but other parts of the API capitalize it

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(summaryUri);
        string? json = await response.Content.ReadAsStringAsync();
        CosponserRequest? cosponsors = JsonSerializer.Deserialize<CosponserRequest>(json);
        return cosponsors ?? throw new Exception("Couldn't deserialize cosponsors");
    }

}