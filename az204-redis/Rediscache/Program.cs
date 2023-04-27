using StackExchange.Redis;
using System.Threading.Tasks;

string connectionString = "az204redis2365.redis.cache.windows.net:6380,password=U7UhNb9QQsoAglfpUS566rYZ9MSTRTuurAzCaAfSEJQ=,ssl=True,abortConnect=False";

var cache = ConnectionMultiplexer.Connect(connectionString);
IDatabase db = cache.GetDatabase();

// Snippet below executes a PING to test the server connection
var result = await db.ExecuteAsync("ping");
Console.WriteLine($"PING = {result.Type} : {result}");

// Call StringSetAsync on the IDatabase object to set the key "test:key" to the value "100"
bool setValue = await db.StringSetAsync("test:key", "100");
Console.WriteLine($"SET: {setValue}");

// StringGetAsync takes the key to retrieve and return the value
string getValue = await db.StringGetAsync("test:key");
Console.WriteLine($"GET: {getValue}");