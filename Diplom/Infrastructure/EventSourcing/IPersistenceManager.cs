using System;
using System.Data;

namespace Infrastructure.EventSourcing
{
    public interface IPersistenceManager : IDisposable
    {
        void Commit();
    }
}

