using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager;
using Azure.Identity;

namespace SWELearningApp
{
    public interface ITaggingApp
    {
        Task Run();
    }

    public class TaggingApp : ITaggingApp
    {
        public async Task Run()
        {
            Console.WriteLine("Tagging App is running...");
            string tenantId = "86b1dded-d548-4e1b-9584-900494145773";
            string appId = "9d021cc0-9ba1-4ab8-8eb0-68c902b89faf";
            string appSecret = "19lxQk~rnjV~SPTzVb-E7w7Y0__DrpyMOl";
            string resourceAppIdUri = "https://api-gov.securitycenter.microsoft.us";
            Uri oAuthUri = new($"https://login.microsoftonline.us/{tenantId}/oauth2/token");

            AuthBody authBody = new(resourceAppIdUri, appId, appSecret, "client_credentials");

            var formDataContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("resource", resourceAppIdUri),
                new KeyValuePair<string, string>("client_id", appId),
                new KeyValuePair<string, string>("client_secret", appSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            HttpRequestMessage httpRequest = new();
            httpRequest.Method = HttpMethod.Post;
            httpRequest.RequestUri = oAuthUri;
            httpRequest.Content = formDataContent;

            HttpClient client = new();
            var response = client.Send(httpRequest);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("hey");

            var authResponse = JsonSerializer.Deserialize<AuthResponse>(content);

            var armClient = new ArmClient(new DefaultAzureCredential());

            var defaultSub = armClient.GetDefaultSubscription();

            var vms = defaultSub.GetVirtualMachines().ToList();
        }
    }

    public class AuthResponse 
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonPropertyName("ext_expires_in")]
        public string ExtExpiresIn { get; set; }

        [JsonPropertyName("expires_on")]
        public string ExpiresOn { get; set; }

        [JsonPropertyName("not_before")]
        public string NotBefore { get; set; }

        [JsonPropertyName("resource")]
        public string Resource { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }

    public class AuthBody
    {
        public string resource { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }

        public AuthBody(string resource, string client_id, string client_secret, string grant_type)
        {
            this.resource = resource;
            this.client_id = client_id;
            this.client_secret = client_secret;
            this.grant_type = grant_type;
        }
    }
}