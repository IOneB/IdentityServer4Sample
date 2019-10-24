using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityModel.OidcConstants;

namespace WebApplication3
{
    public static class Config
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("data", "Custom Data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ClientCredentials ,

                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },

                    AllowedScopes = { "data" }
                }
            };
    }
}
