using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mongomvc.Models
{
    public class Group
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

    }
    public class GroupViewModel
    {
        public string Name { get; set; }
    }
    public class UserTableModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
    }
    public class AboutFaculty
    {
        public int countTeacher { get; set; }
        public int countStudent { get; set; }
        public List<Degree> degrees { get; set; }
    }
    public class Teacher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string GroupId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

    }
    public class TeacherViewModel
    {
      
        public string Name { get; set; }
        public string GroupId { get; set; }
       public IFormFile DataFile { get; set; }

    }

    public class Degree
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }  public class DegreeViewModel
    {
  
        public string Name { get; set; }
    }
    public class DegreeDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DegreeId { get; set; }
    }



    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Day { get; set; }
        public string TeacherId { get; set; }
        public int Capacity { get; set;}
        public int RegistrationNsumber { get; set;}

    }    public class CourseView
    {
     
        public string Id { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Day { get; set; }
        public string TeacherName { get; set; }
        public int Capacity { get; set;}
        public int RegistrationNsumber { get; set;}

    }   public class CourseViewModel
    {
     
        public string Name { get; set; }
        public string StartTimeH { get; set; }
        public string StartTimeM { get; set; }
        public string EndTimeH { get; set; }
        public string EndTimeM { get; set; }
        public string Day { get; set; }

        public string TeacherId { get; set; }
        public int Capacity { get; set; }
    }

    public class Enrollment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CourseId { get; set; }
    }

}
