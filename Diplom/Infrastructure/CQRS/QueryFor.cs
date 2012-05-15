using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Infrastructure.CQRS
{
    public class QueryFor<TResult> 
    {
        private readonly ViewResult _view;

        public QueryFor(ViewResult view)
        {
            _view = view;
        }

        public TResult GetBy(Guid id)
        {
            var collection = MongoHelper.GetCollectionOf<TResult>();
            return collection.FindOneById(id);
        }



        public IList<TResult> GetPage(int page,int pageSize, out int totalPages)
        {
            var collection = MongoHelper.GetCollectionOf<TResult>();
            long _totalPages;
            var r = collection.GetPage(null, page, pageSize, out _totalPages);

            totalPages = (int)_totalPages;
            return r;
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