using MongoDB.Driver;
using mongomvc.Models;
using System.Text.RegularExpressions;

public class TeacherService
{
    private readonly IMongoCollection<Teacher> _teachers;

    public TeacherService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("WebProject");
        _teachers = database.GetCollection<Teacher>("Teacher");
    }

    public async Task<List<Teacher>> GetAsync() =>
        await _teachers.Find(teacher => true).ToListAsync();

    public async Task<Teacher> GetAsync(string id) =>
        await _teachers.Find<Teacher>(teacher => teacher.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Teacher teacher) =>
        await _teachers.InsertOneAsync(teacher);

    public async Task UpdateAsync(string id, Teacher teacherIn) =>
        await _teachers.ReplaceOneAsync(teacher => teacher.Id == id, teacherIn);

    public async Task RemoveAsync(string id) =>
        await _teachers.DeleteOneAsync(teacher => teacher.Id == id);
}
