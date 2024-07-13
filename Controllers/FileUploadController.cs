using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using mongomvc.Models;

namespace mongomvc.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IMongoCollection<FileModel> _filesCollection;

        public FileUploadController(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("YourDatabaseName");
            _filesCollection = database.GetCollection<FileModel>("Files");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                var fileModel = new FileModel
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Data = stream.ToArray()
                };
                await _filesCollection.InsertOneAsync(fileModel);
            }

            return RedirectToAction("Index");
        }
    }
}
