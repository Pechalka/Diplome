using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Infrastructure.EventSourcing;

namespace Infrastructure.CQRS
{
    public class QueryFor<TResult> where TResult : IAggregateRoot
    {
        private readonly ViewResult _view;

        public QueryFor(ViewResult view)
        {
            _view = view;
        }

        //public TResult GetBy(Guid id)
        //{
        //    var collection = MongoHelper.GetCollectionOf<TResult>();
        //    return collection.FindOneById(id);
        //}

        public TResult GetBy(Guid id) 
        {
            var rep = new EventSourcedRepository<TResult>(new MongoEventStore());
            return rep.ById(id);
        }

        public IList<TResult> GetPage(int page,int pageSize, out int totalPages)
        {
            var collection = MongoHelper.GetCollectionOf<TResult>();
            return collection.GetPage(null, page, pageSize, out totalPages);
        }

        public IList<TResult> All()
        {
            var collection = MongoHelper.GetCollectionOf<TResult>();
            return collection.FindAll().ToList();
        }

        public QueryExecuter<TQuery, TResult> Execute<TQuery>() where TQuery : IQuery<TResult>
        {
            return new QueryExecuter<TQuery, TResult>(_view);
        }
    }
}