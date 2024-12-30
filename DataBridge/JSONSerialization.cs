using System.Text.Json.Serialization;

namespace GOV;

#region Requests
class BillRequest {
    [JsonPropertyName("bills")]
    public Bill[]? Bills {get; set;}
    [JsonPropertyName("pagination")]
    public Pagination? Pagination {get; set;}
    [JsonPropertyName("request")]
    public RequestInfo? Request {get; set;}
}

class SummaryRequest
{
    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }
    [JsonPropertyName("request")]
    public RequestInfo? Request { get; set; }

    [JsonPropertyName("summaries")]
    public List<Summary>? Summaries { get; set; }
}

class CosponserRequest
{
    [JsonPropertyName("cosponsors")]
    public List<Cosponsor>? Cosponsors { get; set; }

    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }

    [JsonPropertyName("request")]
    public RequestInfo? Request { get; set; }
}

class CongressRequest {
    [JsonPropertyName("congress")]
    public Congress? Congress {get; set;}
    [JsonPropertyName("request")]
    public RequestInfo? Request {get; set;}
}
#endregion

#region Meta info
/// <summary>
/// The request object that gets returned from all API calls. 
/// </summary>
/// <remarks>
/// Not all properties are guaranteed to exist in an API call. 
/// </remarks>
class RequestInfo {
    [JsonPropertyName("contentType")]
    public string? ContentType {get; set;}
    [JsonPropertyName("format")]
    public string? Format {get; set;}

    #region Summary Request Info
    [JsonPropertyName("billNumber")]
    public string? BillNumber { get; set; }

    [JsonPropertyName("billType")]
    public string? BillType { get; set; }

    [JsonPropertyName("billUrl")]
    public string? BillUrl { get; set; }

    [JsonPropertyName("congress")]
    public string? Congress { get; set; }
    #endregion
}
class Pagination {
    [JsonPropertyName("count")]
    public int? Count {get; set;}
    [JsonPropertyName("next")]
    public string? Next {get; set;} = "";
    [JsonPropertyName("prev")]
    public string? Prev {get; set;} = "";
    
    #region Cosponser Pagination Info
    [JsonPropertyName("countIncludingWithdrawnCosponsors")]
    public int CountIncludingWithdrawnCosponsors { get; set; }
    #endregion
}
#endregion

class Summary
{
    [JsonPropertyName("actionDate")]
    public DateTime? ActionDate { get; set; }

    [JsonPropertyName("actionDesc")]
    public string? ActionDesc { get; set; }

    [JsonPropertyName("bill")]
    public Bill? BillDetails { get; set; }

    [JsonPropertyName("currentChamber")]
    public string? CurrentChamber { get; set; }

    [JsonPropertyName("currentChamberCode")]
    public string? CurrentChamberCode { get; set; }

    [JsonPropertyName("lastSummaryUpdateDate")]
    public DateTime? LastSummaryUpdateDate { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("updateDate")]
    public DateTime? UpdateDate { get; set; }

    [JsonPropertyName("versionCode")]
    public string? VersionCode { get; set; }
}

class Bill {
    [JsonPropertyName("congress")]
    public int Congress {get; set;}
    [JsonPropertyName("latestAction")]
    public LatestAction? LatestAction {get; set;}
    [JsonPropertyName("laws")]
    public Law[]? Laws {get; set;}
    [JsonPropertyName("number")]
    public string Number {get; set;} = "";
    [JsonPropertyName("originChamber")]
    public string OriginChamber {get; set;} = "";
    [JsonPropertyName("originChamberCode")]
    public string OriginChamberCode {get; set;} = "";
    [JsonPropertyName("title")]
    public string Title {get; set;} = "";
    [JsonPropertyName("type")]
    public string Type {get; set;} = "";
    [JsonPropertyName("updateDate")]
    public DateTime? UpdateDate {get; set;}
    [JsonPropertyName("updateDateIncludingText")]
    public string? UpdateDateIncludingText {get; set;} = "";
    [JsonPropertyName("url")]
    public string? URL {get; set;} = "";
}

class LatestAction {
    [JsonPropertyName("actionDate")]
    public string ActionDate {get; set;} = "";
    [JsonPropertyName("text")]
    public string Text {get; set;} = "";
}

class Law {
    [JsonPropertyName("number")]
    public string Number {get; set;} = "";
    [JsonPropertyName("type")]
    public string Type {get; set;} = "";
}

class Congress {
    [JsonPropertyName("startYear")]
    public string StartYear {get; set;} = "";
    [JsonPropertyName("endYear")]
    public string? EndYear {get; set;} = "";
    [JsonPropertyName("updateDate")]
    public DateTime UpdateDate {get; set;} = DateTime.MinValue;
    [JsonPropertyName("name")]
    public string Name {get; set;} = "";
    [JsonPropertyName("number")]
    public int Number {get; set;} = -1;
    [JsonPropertyName("url")]
    public string URL {get; set;} = "";
    [JsonPropertyName("sessions")]
    public Session[] Sessions {get; set;} = [];
}

class Session {
    [JsonPropertyName("number")]
    public int Number {get; set;} = -1;
    [JsonPropertyName("chamber")]
    public string Chamber {get; set;} = "";
    [JsonPropertyName("startDate")]
    public string StartDate {get; set;} = "";
    [JsonPropertyName("endDate")]
    public string? EndDate {get; set;} = "";
    [JsonPropertyName("type")]
    public string Type {get; set;} = "";
}

public class Cosponsor
{
    [JsonPropertyName("bioguideId")]
    public string? BioguideId { get; set; }

    [JsonPropertyName("district")]
    public int District { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

    [JsonPropertyName("isOriginalCosponsor")]
    public bool IsOriginalCosponsor { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("middleName")]
    public string? MiddleName { get; set; }

    [JsonPropertyName("party")]
    public string? Party { get; set; }

    [JsonPropertyName("sponsorshipDate")]
    public string? SponsorshipDate { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}