using System.Text.Json;

namespace SearXNG.Client.Library;

public class ApiClient : IDisposable {
    private readonly HttpClient _httpClient;
    private bool _disposed;

    public ApiClient(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public ApiClient(string baseUrl) {
        // Initialize HttpClient with default settings
        var loggingHandler = new LoggingHandler(new HttpClientHandler());

        _httpClient = new HttpClient(loggingHandler) {
            BaseAddress = new Uri(baseUrl),
            Timeout = TimeSpan.FromSeconds(30) // Set an appropriate timeout
        };

        // Add default headers if necessary
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.UserAgent.Clear(); // Clear existing user-agent entries if needed
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36");
    }

    public async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken) {
        if (string.IsNullOrWhiteSpace(endpoint))
            throw new ArgumentException("Endpoint cannot be null or empty", nameof(endpoint));

        var response = await _httpClient.GetAsync(endpoint, cancellationToken);

        response.EnsureSuccessStatusCode();

        string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload, CancellationToken cancellationToken) {
        if (string.IsNullOrWhiteSpace(endpoint))
            throw new ArgumentException("Endpoint cannot be null or empty", nameof(endpoint));

        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        StringContent content;

        if (typeof(TRequest) != typeof(string)) {
            var jsonPayload = JsonSerializer.Serialize(payload);
            //single purpose ugliness, searnxg needed it encoded this way...
            content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
        } else {
            content = new StringContent(payload as string, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

        response.EnsureSuccessStatusCode();

        string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<TResponse>(jsonResponse, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task DeleteAsync(string endpoint, CancellationToken cancellationToken) {
        if (string.IsNullOrWhiteSpace(endpoint))
            throw new ArgumentException("Endpoint cannot be null or empty", nameof(endpoint));

        HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposed) {
            if (disposing) {
                _httpClient?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}