using Lab_4a.Models;
using MongoDB.Driver;

namespace Lab_4a.Data
{
  public class MongoDbContext
{
    private readonly IAtlasSettings _settings;
    private readonly IMongoDatabase _db;

    public MongoDbContext(IAtlasSettings settings)
    {
        _settings = settings;
        var client = new MongoClient(_settings.ConnectionString);
        _db = client.GetDatabase(_settings.Database);
    }

    public IMongoCollection<_Task> Tasks
    {
        get
        {
            return _db.GetCollection<_Task>(_settings.Collection);
        }
    }
}
}