using System.Text.Json;

namespace SearXNG.Client.Library;

public class SearchParametersSerializer {

    /// <summary>
    /// Serializes SearchParameters to a query string for GET requests.
    /// </summary>
    /// <param name="parameters">The SearchParameters object.</param>
    /// <returns>A query string representation.</returns>
    public string ToQueryString(SearchParameters parameters) {
        if (parameters == null)
            throw new ArgumentNullException(nameof(parameters));

        var queryParams = new List<string>();

        if (!string.IsNullOrWhiteSpace(parameters.Query))
            queryParams.Add($"q={Uri.EscapeDataString(parameters.Query)}");

        if (parameters.Categories != null && parameters.Categories.Any())
            queryParams.Add($"categories={string.Join(",", parameters.Categories.Select(Uri.EscapeDataString))}");

        if (parameters.Engines != null && parameters.Engines.Any())
            queryParams.Add($"engines={string.Join(",", parameters.Engines.Select(Uri.EscapeDataString))}");

        if (!string.IsNullOrWhiteSpace(parameters.Language))
            queryParams.Add($"language={Uri.EscapeDataString(parameters.Language)}");

        if (parameters.PageNumber > 0)
            queryParams.Add($"pageno={parameters.PageNumber}");

        if (parameters.TimeRange != default)
            queryParams.Add($"time_range={parameters.TimeRange}");

        queryParams.Add($"format={parameters.Format}");

        if (parameters.ImageProxy) {
            queryParams.Add($"image_proxy=True");
        } else {
            queryParams.Add($"image_proxy=False");
        }

        queryParams.Add($"safesearch={(int)parameters.SafeSearch}");

        if (parameters.EnabledPlugins != default)
            queryParams.Add($"enabled_plugins={parameters.EnabledPlugins}");

        if (parameters.DisabledPlugins != default)
            queryParams.Add($"disabled_plugins={parameters.DisabledPlugins}");

        if (parameters.EnabledEngines != null && parameters.EnabledEngines.Any())
            queryParams.Add($"enabled_engines={string.Join(",", parameters.EnabledEngines.Select(Uri.EscapeDataString))}");

        if (parameters.DisabledEngines != null && parameters.DisabledEngines.Any())
            queryParams.Add($"disabled_engines={string.Join(",", parameters.DisabledEngines.Select(Uri.EscapeDataString))}");

        return string.Join("&", queryParams);
    }

    /// <summary>
    /// Serializes SearchParameters to a JSON body for POST requests.
    /// </summary>
    /// <param name="parameters">The SearchParameters object.</param>
    /// <returns>A JSON representation.</returns>
    public string ToJson(SearchParameters parameters) {
        if (parameters == null)
            throw new ArgumentNullException(nameof(parameters));

        var options = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        return JsonSerializer.Serialize(parameters, options);
    }
}