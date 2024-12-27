using System.Text.Json.Serialization;
namespace GOV;

class SummaryRequest
{
    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }
    [JsonPropertyName("request")]
    public RequestInfo? Request { get; set; }

    [JsonPropertyName("summaries")]
    public List<Summary>? Summaries { get; set; }
}

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