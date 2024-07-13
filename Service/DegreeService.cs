using MongoDB.Driver;
using mongomvc.Models;

namespace mongomvc.Service
{
    public class DegreeService
    {
        private readonly IMongoCollection<Degree> _groups;

        public DegreeService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("WebProject");
            _groups = database.GetCollection<Degree>("Degree");
        }

        public async Task<List<Degree>> GetAsync() =>
            await _groups.Find(group => true).ToListAsync();

        public async Task<Degree> GetAsync(string id) =>
            await _groups.Find<Degree>(group => group.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Degree group) =>
            await _groups.InsertOneAsync(group);

    }
}
