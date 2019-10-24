using System;
using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static async Task Main()
        {
            Console.ReadLine();
            var token = await GetToken();
            
            var client = new HttpClient();
            client.SetBearerToken(token);

            var response = await client.GetAsync("http://localhost:5001/data");
            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.StatusCode);
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadLine();
        }

        private static async Task<string> GetToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return default;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "data"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return default;
            }
            Console.WriteLine(tokenResponse.Json);
            return tokenResponse.AccessToken;
        }
    }
}
