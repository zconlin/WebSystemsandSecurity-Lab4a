using Lab_4a.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab_4a.Data.Dao

{
    public class TaskDao : ITaskDao
{
    private readonly MongoDbContext _db;
    public TaskDao(IAtlasSettings settings)
    {
        _db = new MongoDbContext(settings);
    }
    public async Task Create(_Task task)
    {
        try { await _db.Tasks.InsertOneAsync(task); }
        catch { throw; }
    }
    public async Task Delete(string id)
    {
        try
        {
            FilterDefinition<_Task> data = Builders<_Task>.Filter.Eq("Id", id);
            await _db.Tasks.DeleteOneAsync(data);
        }
        catch { throw; }
    }
    public async Task<_Task> Get(string id)
    {
        try
        {
            FilterDefinition<_Task> filter = Builders<_Task>.Filter.Eq("Id", id);
            return await _db.Tasks.Find(filter).FirstOrDefaultAsync();
        }
        catch { throw; }
    }
    public async Task<IEnumerable<_Task>> Read(string UserId)
    {
        try
        {
            FilterDefinition<_Task> filter = Builders<_Task>.Filter.Eq("UserId", UserId);
            return await _db.Tasks.Find(filter).ToListAsync();
        }
        catch { throw; }
    }
    public async Task Update(_Task task)
    {
        try { await _db.Tasks.ReplaceOneAsync(filter: g => g.Id == task.Id, replacement: task); }
        catch { throw; }
    }
}

public interface ITaskDao
{
    Task Create(_Task task);
    Task Delete(string id);
    Task<_Task> Get(string id);
    Task<IEnumerable<_Task>> Read(string UserId);
    Task Update(_Task task);
}
}