using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mongomvc.Models
{
    public class FileModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int Location { get ; set; }
        public string ParentId{ get; set; }
      
        public byte[] Data { get; set; }
    }
    
}
