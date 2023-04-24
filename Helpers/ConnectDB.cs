using MongoDB.Driver;

namespace Workdo.Helpers;

public class ConnectDB<T>
{
    private static IMongoClient _client;

    public static IMongoCollection<T> GetClient(string databaseName, string collectionName)
    {
        string DB_NAME = "mongodb+srv://dnhau191:L0ojM4eciv1rD9Km@mailbox.gzf7jht.mongodb.net/?retryWrites=true&w=majority";
        var settings = MongoClientSettings.FromConnectionString(DB_NAME);

        _client = new MongoClient(settings);
        IMongoDatabase database = _client.GetDatabase(databaseName);
        return database.GetCollection<T>(collectionName);
    }
}