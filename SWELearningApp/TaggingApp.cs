using System.Net.Http.Json;
using System.Text.Json;

namespace SWELearningApp
{
    public interface ITaggingApp
    {
        void Run();
    }

    public class TaggingApp : ITaggingApp
    {
        public void Run()
        {
            Console.WriteLine("Tagging App is running...");
            string tenantId = "86b1dded-d548-4e1b-9584-900494145773";
            string appId = "9d021cc0-9ba1-4ab8-8eb0-68c902b89faf";
            string appSecret = "5";
            string resourceAppIdUri = "https://api-gov.securitycenter.microsoft.us";
            Uri oAuthUri = new("https://login.microsoftonline.us/$TenantId/oauth2/token");

            AuthBody authBody = new(resourceAppIdUri, appId, appSecret, "client_credentials");
            var jsonContent = JsonContent.Create(authBody);

            HttpRequestMessage httpRequest = new();
            httpRequest.Method = HttpMethod.Post;
            httpRequest.RequestUri = oAuthUri;
            httpRequest.Content = jsonContent;

            HttpClient client = new();
            var response = client.Send(httpRequest);
            Console.WriteLine("hey");
        }
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