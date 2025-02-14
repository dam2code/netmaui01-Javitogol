using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Essentials;

namespace PartsClient.Data;

public static class PartsManager
{
    // Existing code...

    public static async Task<IEnumerable<Part>> GetAll()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            return new List<Part>();
        }

        var client = await GetClient();
        string result = await client.GetStringAsync($"{Url}parts");

        return JsonSerializer.Deserialize<List<Part>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }

    public static async Task<Part> Add(string partName, string supplier, string partType)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return new Part();

        var part = new Part()
        {
            PartName = partName,
            Suppliers = new List<string>(new[] { supplier }),
            PartID = string.Empty,
            PartType = partType,
            PartAvailableDate = DateTime.Now.Date
        };

        var client = await GetClient();
        var msg = new HttpRequestMessage(HttpMethod.Post, $"{Url}parts")
        {
            Content = JsonContent.Create(part)
        };

        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();

        var returnedJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Part>(returnedJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }

    public static async Task Update(Part part)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return;

        var client = await GetClient();
        var msg = new HttpRequestMessage(HttpMethod.Put, $"{Url}parts/{part.PartID}")
        {
            Content = JsonContent.Create(part)
        };

        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
    }

    public static async Task Delete(string partID)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return;

        var client = await GetClient();
        var msg = new HttpRequestMessage(HttpMethod.Delete, $"{Url}parts/{partID}");

        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
    }
}
