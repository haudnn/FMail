using MongoDB.Driver;

namespace Workdo.Helpers;

public class ConnectDB
{
    private static readonly IMongoClient _client;
    static ConnectDB()
    {
        string DB_NAME = "mongodb://localhost:27017/mailbox";
        var settings = MongoClientSettings.FromConnectionString(DB_NAME);
        _client = new MongoClient(settings);
    }
    public static IMongoClient GetClient()
    {
        return _client;
    }
}