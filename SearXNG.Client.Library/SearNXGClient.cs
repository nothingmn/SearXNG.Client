using System.Net.Http;

namespace SearXNG.Client.Library;

/// <summary>
/// https://docs.searxng.org/dev/search_api.html
/// </summary>
public class SearNXGClient : IDisposable {
    private string _host;
    private string _baseUrl;
    private int _port;
    private bool _disposed;
    private ApiClient _apiClient;
    private UrlHelper _urlHelper = new UrlHelper();
    private SearchParametersSerializer _serializer = new SearchParametersSerializer();

    public SearNXGClient() {
    }

    public void Init(string host, int port = default, HttpClient httpClient = default) {
        _host = host;
        _port = port;
        _baseUrl = _urlHelper.GetBaseUrl(host, port);
        _apiClient = new ApiClient(_baseUrl);
    }

    public SearNXGClient(string host, int port = default, HttpClient httpClient = default) {
        this.Init(host, port, httpClient);
    }

    public async Task<SearchResults> Search(SearchParameters search, CancellationToken cancellationToken) {
        if (search.Method == Method.POST) {
            return await _apiClient.PostAsync<string, SearchResults>("/search", _serializer.ToQueryString(search), cancellationToken);
        } else {
            var queryString = $"/search?{_serializer.ToQueryString(search)}";
            return await _apiClient.GetAsync<SearchResults>(queryString, cancellationToken);
        }
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposed) {
            if (disposing) {
                _apiClient?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}