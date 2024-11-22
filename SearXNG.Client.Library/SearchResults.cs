using System.Text.Json.Serialization;

namespace SearXNG.Client.Library;

public class SearchResults {

    [JsonPropertyName("query")]
    public string Query { get; set; }

    [JsonPropertyName("number_of_results")]
    public int NumberOfResults { get; set; }

    [JsonPropertyName("results")]
    public List<SearchResult> Results { get; set; }

    [JsonPropertyName("answers")]
    public List<object> Answers { get; set; }

    [JsonPropertyName("corrections")]
    public List<object> Corrections { get; set; }

    [JsonPropertyName("infoboxes")]
    public List<object> Infoboxes { get; set; }

    [JsonPropertyName("suggestions")]
    public List<string> Suggestions { get; set; }

    [JsonPropertyName("unresponsive_engines")]
    public List<object> UnresponsiveEngines { get; set; }
}

public class SearchResult {

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("publishedDate")]
    public DateTime? PublishedDate { get; set; }

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; }

    [JsonPropertyName("engine")]
    public string Engine { get; set; }

    [JsonPropertyName("parsed_url")]
    public List<string> ParsedUrl { get; set; }

    [JsonPropertyName("template")]
    public string Template { get; set; }

    [JsonPropertyName("engines")]
    public List<string> Engines { get; set; }

    [JsonPropertyName("positions")]
    public List<int> Positions { get; set; }

    [JsonPropertyName("score")]
    public float Score { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }
}