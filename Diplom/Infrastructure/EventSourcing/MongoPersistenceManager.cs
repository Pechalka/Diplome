using System.Collections.Generic;

namespace Infrastructure.EventSourcing
{
    public class MongoPersistenceManager : IPersistenceManager
    {
        internal const string AGGREGATE_KEY = "Infrastructure.Impl.PersistenceManager.Aggregate";
        private readonly IEventBus _eventBus;
        private readonly IEventStore lazyEventStore;
        private readonly IContext _context;

        public MongoPersistenceManager(IEventBus eventBus,IEventStore eventStore, IContext context)
        {
            _eventBus = eventBus;
            lazyEventStore = eventStore;
            _context = context;
        }

        public void Dispose()
        {
            
        }

        public void Commit()
        {
            var aggregates = _context[AGGREGATE_KEY] as HashSet<IAggregateRoot>;

            if (aggregates != null && aggregates.Count > 0)
            {

                foreach (var ar in aggregates)
                {
                    lazyEventStore.PersistUncommitedEvents(ar);
                }


                // At this stage, no concurrency issues, so pass on to the event handlers
                foreach (var ar in aggregates)
                {
                    foreach (var evt in ar.UncommitedEvents)
                    {
                        _eventBus.Publish(evt);
                    }
                }


                _context[AGGREGATE_KEY] = null;
            }
        }
    }
}