using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Введите токен: ");
        string token = Console.ReadLine();

        Console.Write("Введите ID организации: ");
        string orgId = Console.ReadLine();

        string fields = "email,name,id";
        int perPage = 50;

        string url = $"https://api360.yandex.net/directory/v1/org/{orgId}/users?fields={fields}&perPage={perPage}";

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                using (JsonDocument document = JsonDocument.Parse(responseBody))
                {
                    JsonElement root = document.RootElement;
                    JsonElement usersElement = root.GetProperty("users");

foreach (JsonElement userElement in usersElement.EnumerateArray())
{
    string email = "";
    string firstName = "";
    string lastName = "";
    string id = "";

    if (userElement.TryGetProperty("email", out JsonElement emailElement) && emailElement.ValueKind == JsonValueKind.String)
    {
        email = emailElement.GetString();
    }

    if (userElement.TryGetProperty("name", out JsonElement nameElement) && nameElement.ValueKind == JsonValueKind.Object)
    {
        if (nameElement.TryGetProperty("first", out JsonElement firstNameElement) && firstNameElement.ValueKind == JsonValueKind.String)
        {
            firstName = firstNameElement.GetString();
        }

        if (nameElement.TryGetProperty("last", out JsonElement lastNameElement) && lastNameElement.ValueKind == JsonValueKind.String)
        {
            lastName = lastNameElement.GetString();
        }
    }

    if (userElement.TryGetProperty("id", out JsonElement idElement) && idElement.ValueKind == JsonValueKind.String)
    {
        id = idElement.GetString();
    }

    Console.WriteLine($"Email: {email} FirstName: {firstName} LastName: {lastName} Id: {id}");
}
                }
                }
        }
    }
}
