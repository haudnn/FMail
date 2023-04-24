using MongoDB.Driver;

namespace Workdo.Helpers;

public class ConnectDB
{
    private static readonly IMongoClient _client;
    static ConnectDB()
    {
        string DB_NAME = "mongodb+srv://dnhau191:L0ojM4eciv1rD9Km@mailbox.gzf7jht.mongodb.net/?retryWrites=true&w=majority";
        var settings = MongoClientSettings.FromConnectionString(DB_NAME);
        _client = new MongoClient(settings);
    }
    public static IMongoClient GetClient()
    {
        return _client;
    }
}