using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Infrastructure
{
    public static class MongoHelper
    {
        //public static List<T> GetPage<T>(this MongoCollection<T> collection, IMongoQuery query, int page, int pageSize, out int totalPages)
        //{
        //    var getCountCursor = new MongoCursor<T>(collection, query);
        //    var mainCountCursor = new MongoCursor<T>(collection, query);

        //    long countItems = getCountCursor.Count();
        //    totalPages = (int)Math.Ceiling((double)countItems / pageSize);

        //    return mainCountCursor
        //        .SetSkip((page - 1) * pageSize)
        //        .SetLimit(pageSize)
        //        .ToList();
        //}

        public static List<T> GetPage<T>(this MongoCollection<T> collection, IMongoQuery query, int page, int pageSize, out long totalItem)
        {
            var getCountCursor = new MongoCursor<T>(collection, query);
            var mainCountCursor = new MongoCursor<T>(collection, query);

            totalItem = getCountCursor.Count();

            return mainCountCursor
                .SetSkip((page - 1) * pageSize)
                .SetLimit(pageSize)
                .ToList();
        }

        public static MongoCollection<T> GetCollectionOf<T>()
        {
            string collectionName = typeof (T).Name;
            var server = MongoServer.Create();
            var database = server.GetDatabase("Diplome");

            return database.GetCollection<T>(collectionName);
        }
    }
}
