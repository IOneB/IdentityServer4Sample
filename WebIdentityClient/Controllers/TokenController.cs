using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace WebIdentityClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        public TokenController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public IHttpClientFactory ClientFactory { get; }

        public async Task<string> Get(string clientId, string secret)
        {
            var client = ClientFactory.CreateClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                return disco.Error;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = clientId,
                ClientSecret = secret,
                Scope = "data"
            });

            if (tokenResponse.IsError)
            {
                return tokenResponse.Error;
            }
            return tokenResponse.AccessToken;
        }
    }
}
