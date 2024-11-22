// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using SearXNG.Client.Library;

public class Program {

    public static async Task Main() {
        Console.WriteLine("Hello, World!");

        var cts = new CancellationTokenSource();

        var client = new SearNXGClient("192.168.194.131", 8888);

        var results = await client.Search(new SearchParameters() {
            Query = "how tall is mount everest"
        }, cts.Token);

        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(results, new JsonSerializerOptions() { WriteIndented = true }));
    }
}