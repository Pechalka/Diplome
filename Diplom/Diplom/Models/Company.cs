using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Diplom.Models
{
    public class Company
    {
        public string Category { get; set; }

        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }

        public string Description { get; set; }

        public string WorkedTime { get; set; }

        public string Address { get; set; }

        public string LogoImg { get; set; }

        //public string LogoImg {
        //    get { return _LogoImg ?? "nologo.jpg"; }
        //    set { _LogoImg = value; }
        //}

        public string Name { get; set; }

        public static Company Create(string name, string description)
        {
            return new Company
                       {
                           Name = name, 
                           Description = description
                       };
        }
    }


}