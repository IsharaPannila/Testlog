using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestAspnetcorewebapplication1.Configurations
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client{
                    ClientId ="testClient",
                    ClientName ="Example test ishara",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>{
                        new Secret("suppassword".Sha256()) },
                    AllowedScopes = new List<string>{ "customAPI.read"}

                },

                new Client{
                    ClientId ="openidconnectClient",
                    ClientName ="example OpenID client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes .OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        "customAPI.write"
                    },
                    RedirectUris =new List<string>{ "http://localhost:56890/signin-oidc" },
                    PostLogoutRedirectUris=new List<string>{ "http://localhost:56890/" }

                }

            };

        }


    }

    internal class Resources {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources .Email(),
                new IdentityResources.Profile(),
                new IdentityResource {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }

            };

        }

        public static IEnumerable<ApiResource> GetApiResources()
        {

            return new List<ApiResource>
            {
                new ApiResource{
                    Name ="customAPI",
                    DisplayName = "custom API",
                    Description =" custom API desc",
                    UserClaims = new List<string>{"role" },
                    ApiSecrets = new List<Secret>{  new Secret("scopesecret".Sha256())},
                    Scopes = new List<Scope>
                    {
                        new Scope("customAPI.read"),
                        new Scope("customAPI.write")
                    }

                }

            };

        }

    }

    internal class Users
    {
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser{
                    SubjectId ="5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username ="ishara",
                    Password ="testpwd",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email,"isharasp@gmail.com"),
                        new Claim(JwtClaimTypes.Role, "admin" )
                    }

                }
            };

        }

    }


}
