using System;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Azure.Identity;

namespace az204graph{
    class Program {
            static async Task Main(string[] args){

                var scopes = new[] { "https://graph.microsoft.com/.default" };

                var tenantId = "TENANT_ID";
                var clientId = "CLIENT_ID";
                var clienSecret = "CLIENT_SECRET";

                // using Azure.Identity;
                var options = new TokenCredentialOptions
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                };

                var clientSecretCredential = new ClientSecretCredential (tenantId, clientId, clienSecret, options);

                var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

                // async call to Graph API to read all users
                var usersResponse = await graphClient.Users.GetAsync();
                var users = usersResponse.Value;

                foreach (var usr in users){
                    Console.WriteLine(usr.DisplayName);
                }

            }
    }
}
