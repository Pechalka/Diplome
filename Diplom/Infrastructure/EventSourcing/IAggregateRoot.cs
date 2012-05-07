using System;
using System.Collections.Generic;

namespace Infrastructure.EventSourcing
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        IEnumerable<IEvent> UncommitedEvents { get; }
        int Version { get; }
    }
}
