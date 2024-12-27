using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace GOV;

// "facade" to handle API logic
class ApiFacade {
    CongressAPI congressAPI;
    public ApiFacade(string congressKey){
        congressAPI = new CongressAPI(congressKey);
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
}

class CongressAPI {
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
}