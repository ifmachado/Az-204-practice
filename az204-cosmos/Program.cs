using Microsoft.Azure.Cosmos;

public class Program
{

    private static readonly string EndpointUri = "URI";

    private static readonly string PrimaryKey = "PK";

    private CosmosClient cosmosClient;

    private Database database;

    private Container container;

    // db and container names
    private string databaseId = "az204Database";
    private string containerId = "az204Container";

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Beginning operations...\n");
            Program p = new Program();
            // run async method to create client, db and container
            await p.CosmosAsync();

        }
        catch (CosmosException de)
        {
            Exception baseException = de.GetBaseException();
            Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        finally
        {
            Console.WriteLine("End of program, press any key to exit.");
            Console.ReadKey();
        }
    }
  
    public async Task CosmosAsync()
    {
        // create cosmos client instance
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // run method to create DB
        await this.CreateDatabaseAsync();

        // run method to create container
        await this.CreateContainerAsync();
    }

    // create db database using client
    private async Task CreateDatabaseAsync()
    {
        this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        Console.WriteLine("Created Database: {0}\n", this.database.Id);
    }

    // create conrainer database using db created
        private async Task CreateContainerAsync()
    {
        // Create a new container
        this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
        Console.WriteLine("Created Container: {0}\n", this.container.Id);
    }
}