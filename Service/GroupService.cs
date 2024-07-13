using MongoDB.Driver;
using mongomvc.Models;

public class GroupService
{
    private readonly IMongoCollection<Group> _groups;

    public GroupService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("WebProject");
        _groups = database.GetCollection<Group>("groups");
    }

    public async Task<List<Group>> GetAsync() =>
        await _groups.Find(group => true).ToListAsync();

    public async Task<Group> GetAsync(string id) =>
        await _groups.Find<Group>(group => group.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Group group) =>
        await _groups.InsertOneAsync(group);

    public async Task UpdateAsync(string id, Group groupIn) =>
        await _groups.ReplaceOneAsync(group => group.Id == id, groupIn);

    public async Task RemoveAsync(string id) =>
        await _groups.DeleteOneAsync(group => group.Id == id);
}
