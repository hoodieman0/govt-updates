using System.Text.Json.Serialization;

/*
 {
    "congress": {
        "endYear": "2024",
        "name": "118th Congress",
        "number": 118,
        "sessions": [
            {
                "chamber": "House of Representatives",
                "endDate": "2024-01-03",
                "number": 1,
                "startDate": "2023-01-03",
                "type": "R"
            },
            {
                "chamber": "Senate",
                "endDate": "2024-01-03",
                "number": 1,
                "startDate": "2023-01-03",
                "type": "R"
            },
            {
                "chamber": "Senate",
                "number": 2,
                "startDate": "2024-01-03",
                "type": "R"
            },
            {
                "chamber": "House of Representatives",
                "number": 2,
                "startDate": "2024-01-03",
                "type": "R"
            }
        ],
        "startYear": "2023",
        "updateDate": "2023-01-03T17:43:32Z",
        "url": "https://api.congress.gov/v3/congress/118?format=json"
    },
    "request": {
        "contentType": "application/json",
        "format": "json"
    }
}
*/

class CongressRequest {
    [JsonPropertyName("congress")]
    public Congress? Congress {get; set;}
    [JsonPropertyName("request")]
    public RequestInfo? Request {get; set;}
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

class RequestInfo {
    [JsonPropertyName("contentType")]
    public string ContentType {get; set;} = "";
    [JsonPropertyName("format")]
    public string Format {get; set;} = "";
}