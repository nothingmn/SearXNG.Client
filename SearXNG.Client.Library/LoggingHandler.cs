using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;

public class LoggingHandler : DelegatingHandler {

    private JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web) {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camel case for properties
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, // Ignore null values
        WriteIndented = true // Format JSON with indents for readability
    };

    public LoggingHandler(HttpMessageHandler innerHandler) : base(innerHandler) {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
#if DEBUG_HTTP_CLIENT
        // Log the request details
        System.Console.WriteLine("Request:");
        System.Console.WriteLine($"{request.Method} {request.RequestUri}");
        System.Console.WriteLine($"Headers:\n{System.Text.Json.JsonSerializer.Serialize(request.Headers, serializerOptions)}");

        if (request.Content != null) {
            System.Console.WriteLine($"Content: {await request.Content.ReadAsStringAsync()}");
        }
#endif
        // Send the request and get the response
        var response = await base.SendAsync(request, cancellationToken);
#if DEBUG_HTTP_CLIENT

        // Log the response details
        System.Console.WriteLine("-----------------------------\nResponse:");
        System.Console.WriteLine($"Status: {response.StatusCode}");
        System.Console.WriteLine($"Headers:\n{System.Text.Json.JsonSerializer.Serialize(request.Headers, serializerOptions)}");

        System.Console.WriteLine("-----------------------------\nResponse Body:");
        if (response.Content != null) {
            System.Console.WriteLine($"{await response.Content.ReadAsStringAsync()}");
        }
#endif

        return response;
    }
}