using MongoDB.Driver;
using mongomvc.Models;

namespace mongomvc.Service
{
    public class StudentService
    {

        private readonly IMongoCollection<UserTableModel> _user;

        public StudentService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("WebProject");
            _user = database.GetCollection<UserTableModel>("usersList");
        }

        public async Task<List<UserTableModel>> GetAsync() =>
    await _user.Find(teacher => true).ToListAsync();
        public async Task CreateAsync(UserTableModel model) =>
         await _user.InsertOneAsync(model);


    }
}
