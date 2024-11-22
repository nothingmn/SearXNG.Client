using System.Text.Json.Serialization;

namespace SearXNG.Client.Library;

public class SearchParameters {

    [JsonIgnore]
    public Method Method { get; set; } = Method.POST;

    [JsonPropertyName("q")]
    public string Query { get; set; }

    public List<string> Categories { get; set; }
    public List<string> Engines { get; set; }
    public string Language { get; set; }
    public int PageNumber { get; set; }
    public SearchTimeRange TimeRange { get; set; }
    public SearchFormat Format { get; set; } = SearchFormat.json;
    public bool ImageProxy { get; set; }
    public SafeSearch SafeSearch { get; set; } = SafeSearch.None;
    public SearchPlugins EnabledPlugins { get; set; }
    public SearchPlugins DisabledPlugins { get; set; }
    public List<string> EnabledEngines { get; set; }
    public List<string> DisabledEngines { get; set; }
}

public enum SearchPlugins {
    Hash_plugin, Self_Information, Tracker_URL_remover, Ahmia_blacklist,
    Hostnames_plugin, Open_Access_DOI_rewrite, Vimlike_hotkeys, Tor_check_plugin
}

/// <summary>
/// safe_search:
//Filter results.
//0: None
//1: Moderate
//2: Strict
/// </summary>
public enum SafeSearch {
    None = 0,
    Moderate = 1,
    Strict = 2
}

public enum SearchTimeRange {
    day, month, year
}

public enum SearchFormat {
    json, csv, rss
}

public enum Method {
    GET, POST
}