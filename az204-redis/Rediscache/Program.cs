using StackExchange.Redis;
using System.Threading.Tasks;

string connectionString = "CONNECTION_STRING";

var cache = ConnectionMultiplexer.Connect(connectionString);
IDatabase db = cache.GetDatabase();

// PING server to test
var result = await db.ExecuteAsync("ping");
Console.WriteLine($"PING = {result.Type} : {result}");

// set key:value
bool setValue = await db.StringSetAsync("test:key", "100");
Console.WriteLine($"SET: {setValue}");

// return value from test key
string getValue = await db.StringGetAsync("test:key");
Console.WriteLine($"GET: {getValue}");