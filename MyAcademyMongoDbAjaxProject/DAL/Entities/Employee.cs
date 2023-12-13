using MongoDB.Bson.Serialization.Attributes;

namespace MyAcademyMongoDbAjaxProject.DAL.Entities
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Salary { get; set; }
    }
}
