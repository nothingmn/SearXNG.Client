namespace SearXNG.Client.Library;

public class UrlHelper {

    public string GetBaseUrl(string host, int port = default) {
        if (string.IsNullOrWhiteSpace(host))
            throw new ArgumentException("Host cannot be null or empty", nameof(host));

        UriBuilder uriBuilder;

        try {
            // Check if the host already contains a scheme (http:// or https://)
            if (Uri.TryCreate(host, UriKind.Absolute, out var existingUri)) {
                // Build URI from existing one, updating the port only if explicitly set
                uriBuilder = new UriBuilder(existingUri);
                if (port != default) {
                    uriBuilder.Port = port; // Update the port if provided
                }
            } else {
                // Assume "http" if no scheme is provided
                uriBuilder = new UriBuilder("http", host);

                // Only set the port if explicitly provided
                if (port != default) {
                    uriBuilder.Port = port;
                }
            }
        } catch (UriFormatException ex) {
            throw new ArgumentException("Invalid host format", nameof(host), ex);
        }

        // Return the URL without trailing slash
        return uriBuilder.Uri.ToString().TrimEnd('/');
    }
}