// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Arch.IS4Host
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("apiApp", "My API")
                {
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "apiApp" }
                },
                new Client
                {
                    ClientId = "eshop spa",
                    ClientName = "Angular Client",
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenType = AccessTokenType.Reference,
                    RedirectUris = { "http://localhost:5002/" },
                    PostLogoutRedirectUris = { "http://localhost:5002/" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes =
                    {
                        "openid",
                        "profile",
                        "apiApp"
                    }

                }
            };
        }
    }
}