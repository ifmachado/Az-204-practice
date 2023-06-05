
using System.Threading.Tasks;
using Microsoft.Identity.Client;

 class Program{
    private const string _clientId = "APPLICATION_CLIENT_ID";
    private const string _tenantId = "DIRECTORY_TENANT_ID";

    public static async Task Main(string[] args){

    //build authorization context
        var app = PublicClientApplicationBuilder
    .Create(_clientId)
    // add known Authority corresponding to an ADFS server (in this case Azure Public Cloud and tenant where app is registered)
    .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
    .WithRedirectUri("http://localhost")
    .Build();

    //permission scope for the token request   
    string[] scopes = { "user.read" };

    //request token
    AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

    //write token to console
    Console.WriteLine($"Token:\t{result.AccessToken}");

    }


}
