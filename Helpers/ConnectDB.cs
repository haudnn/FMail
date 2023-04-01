using MongoDB.Driver;

namespace Workdo.Helpers;

public class ConnectDB
{
    private readonly IMongoClient _client;
    private readonly string DB_NAME = "mongodb://localhost:27017/mailbox";
    public ConnectDB()
    {
        var settings = MongoClientSettings.FromConnectionString(DB_NAME);
        _client = new MongoClient(settings);
    }
    public IMongoClient GetClient()
    {
        return _client;
    }

}