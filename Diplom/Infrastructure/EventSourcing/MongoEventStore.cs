using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using MongoDB.Driver.Builders;

namespace Infrastructure.EventSourcing
{

    public class MongoEventStore : IEventStore
    {
        private BinaryFormatter serializer = new BinaryFormatter();


        private const int UniqueKeyViolation = 2627;

        public void PersistUncommitedEvents(IAggregateRoot aggregate)
        {
            try
            {
                var eventRecord = new EventRecord
                                      {
                                          Id = Guid.NewGuid(),
                                          Version = aggregate.Version + 1,
                                          AggregateId = aggregate.Id,
                                          Data = Serialize(aggregate.UncommitedEvents)
                                      };
                MongoHelper.GetCollectionOf<EventRecord>().Insert(eventRecord);

            }
            catch (SqlException se)
            {
                // Thanks Jonathan Oliver's CQRS Event Store
                if (se.Number == UniqueKeyViolation) throw new ConcurrencyException();
                throw;
            }
        }

        public IEventInputStream LoadEventHistory(Guid aggregateId)
        {
            var eventsRecords = MongoHelper.GetCollectionOf<EventRecord>().Find(Query.EQ("AggregateId", aggregateId)).SetSortOrder(SortBy.Ascending("Version")).ToList();

            var events = new List<IEvent>();
            var version = 0;

            foreach (var eventsRecord in eventsRecords)
            {
                version = eventsRecord.Version;
                events.AddRange(Deserialize(eventsRecord.Data));
            }


            return new SimpleEventInputStream(events, version, aggregateId);
            //using (var reader = persistenceManager.ExecuteQuery("SELECT [Version], [Data] FROM [Events] WHERE aggregate_id = @AggregateId ORDER BY [Version] ASC", new { AggregateId = aggregateId }))
            //{
            //    var events = new List<IEvent>();
            //    var version = 0;

            //    while (reader.Read())
            //    {
            //        version = reader.GetInt32(0);
            //        var data = ((SqlDataReader)reader).GetSqlBinary(1).Value;
            //        events.AddRange(Deserialize(data));
            //    }

            //    return new SimpleEventInputStream(events, version, aggregateId);
            //}

           // return null;
        }

        private class SimpleEventInputStream : IEventInputStream
        {
            public SimpleEventInputStream(IEnumerable<IEvent> events, int version, Guid aggregateId)
            {
                Events = events;
                Version = version;
                AggregateId = aggregateId;
            }

            public IEnumerable<IEvent> Events { get; private set; }
            public int Version { get; private set; }
            public Guid AggregateId { get; private set; }
        }

        private byte[] Serialize(IEnumerable<IEvent> events)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, events.ToArray());
                return stream.GetBuffer();
            }
        }

        private IEnumerable<IEvent> Deserialize(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return (IEvent[])serializer.Deserialize(stream);
            }
        }
    }


    public class EventRecord 
    {
        public Guid Id { get; set; }

        public Guid AggregateId { get; set; }

        public byte[] Data { get; set; }

        public int Version { get; set; }
    }
}
