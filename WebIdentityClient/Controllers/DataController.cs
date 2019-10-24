using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebIdentityClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public DataController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public IHttpClientFactory ClientFactory { get; private set; }

        [HttpGet]
        public async Task<string> Get(string token)
        {
            var client = ClientFactory.CreateClient();
            client.SetBearerToken(token);

            var response = await client.GetAsync("http://localhost:5001/data");
            if (!response.IsSuccessStatusCode)
                return response.StatusCode.ToString();
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                return JArray.Parse(content).ToString();
            }
        }
    }
}