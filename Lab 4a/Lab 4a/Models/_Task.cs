using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Lab_4a.Models
{
    public class _Task
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string date { get; set; }

        public string text { get; set; }

        public bool done { get; set; }
    }
}