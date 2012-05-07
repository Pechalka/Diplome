﻿using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Xunit;

namespace Tests
{
    public class Test
    {
        [BsonRepresentation(BsonType.ObjectId)] 
     //   [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
    }


    public abstract class Entity
    {
        public ObjectId Id { get; set; }
    }

    public class User : Entity
    {
        public string Name { get; private set; }
        public User(string name)
        {
            Name = name;
            roles = new List<string>();
        }


        private  List<string> roles;

        public IEnumerable<string> Roles
        {
            get { return roles.AsReadOnly(); }

        }

        public void AddRole(string role)
        {
            roles.Add(role);
        }

    }


    public class MOngoTests
    {
        private MongoServer server;
        private MongoDatabase database;
        private MongoCollection<Test> collection;

        public MOngoTests()
        {
            server = MongoServer.Create();
            database = server.GetDatabase("Diplome");
            collection = database.GetCollection<Test>("companies");
           collection.RemoveAll();
        }

        [Fact]
        public void CanSaveCollection()
        {
            MongoHelper.SetPrivateFieldsFindOn();

            var user = MongoHelper.GetCollection<User>();
            user.Drop();

            var u = new User("Vasa");
            u.AddRole("Admin");
            user.Save(u);

            var id = u.Id;

            var u2 = user.FindOneById(id);

            Assert.Equal(u2.Roles.Count(), 1);
        }


        [Fact]
        public void CanSave()
        {
            var u = new User("Vasa");

            var user = MongoHelper.GetCollection<User>();
            user.Drop();

            user.Save(u);

            Assert.NotNull(u.Id);

            var id = u.Id;
;

            var u2 = user.FindOneById(id);

            Assert.Equal(u2.Name, "Vasa");
        }

        [Fact]
        public void can_insert()
        {
            collection.Save(new Test { Name = "111" });
            collection.Save(new Test { Name = "222" });


            var count = collection.Count();
            Assert.Equal(2, count);
        }

        [Fact]
        public void can_returnId()
        {
            var obj = new Test {Name = "11"};


            collection.Save(obj);


            Assert.NotEmpty(obj.Id);
        }

        [Fact]
        public void can_save()
        {
            var obj = new Test {  Name = "11" };


            collection.Save(obj);
            obj.Name = "222";
            collection.Save(obj);


            Assert.Equal(1, collection.Count());
        }

        [Fact]
        public void can_get_by_id()
        {
            var obj = new Test { Name = "11" };


            collection.Save(obj);
            var item = collection.FindOneById(ObjectId.Parse( obj.Id));


            Assert.NotNull(item);
        }
    }
}
