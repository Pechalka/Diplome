using System;
using System.Web.Mvc;

namespace Infrastructure.CQRS
{
    public class QueryExecuter<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly ViewResult _view;

        public QueryExecuter(ViewResult view)
        {
            _view = view;
        }

        public ActionResult WithParams(Action<TQuery> initQuery)
        {
            var query = DependencyResolver.Current.GetService<TQuery>();
            initQuery(query);
            var viewModel = query.Execute();
            _view.ViewData.Model = viewModel;

            return _view;
        }


        public ActionResult WithNoParams()
        {
            var query = DependencyResolver.Current.GetService<TQuery>();
            var viewModel = query.Execute();
            _view.ViewData.Model = viewModel;

            return _view;
        }
    }
}