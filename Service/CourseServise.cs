using MongoDB.Driver;
using mongomvc.Models;

namespace mongomvc.Service
{
    public class CourseServise
    {
        private readonly IMongoCollection<Course> _groups;

        public CourseServise(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("WebProject");
            _groups = database.GetCollection<Course>("Course");
        }

        public async Task<List<Course>> GetAsync() =>
            await _groups.Find(group => true).ToListAsync();

        public async Task UpdateAsync(string id, Course groupIn) =>
    await _groups.ReplaceOneAsync(group => group.Id == id, groupIn);
        public async Task<Course> GetAsync(string id) =>
            await _groups.Find<Course>(group => group.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Course group) =>
            await _groups.InsertOneAsync(group);

    }
}
