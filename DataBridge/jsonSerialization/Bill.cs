using System.Text.Json.Serialization;

namespace GOV;

class BillRequest {
    [JsonPropertyName("bills")]
    public Bill[]? Bills {get; set;}
    [JsonPropertyName("pagination")]
    public Pagination? Pagination {get; set;}
    [JsonPropertyName("request")]
    public RequestInfo? Request {get; set;}
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
    public string UpdateDate {get; set;} = "";
    [JsonPropertyName("updateDateIncludingText")]
    public string UpdateDateIncludingText {get; set;} = "";
    [JsonPropertyName("url")]
    public string URL {get; set;} = "";
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

class Pagination {
    [JsonPropertyName("count")]
    public int? Count {get; set;}
    [JsonPropertyName("next")]
    public string? Next {get; set;} = "";
    [JsonPropertyName("prev")]
    public string? Prev {get; set;} = "";
}
