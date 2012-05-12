using System;
using System.Web.Mvc;
using MongoDB.Bson;
using SimpleCqrs.Commanding;

namespace Infrastructure.CQRS
{
    public class CQRSController : Controller
    {
        public QueryFor<TResult> For<TResult>() 
        {
            return new QueryFor<TResult>(View());
        }



        public ActionResult Handle<TCommand>(TCommand command, 
            ActionResult success, ActionResult fail = null) where TCommand : ICommand
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DependencyResolver.Current.GetService<ICommandBus>().Send(command);
                    return success;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e);
                }
            }
            TempData[modelStateKey] = ModelState;
            return fail;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (TempData[modelStateKey] != null && 
                ModelState.Equals(TempData[modelStateKey]) == false)
                    ModelState.Merge((ModelStateDictionary)TempData[modelStateKey]);

            base.OnActionExecuted(filterContext);
        }

        private const string modelStateKey = "modelStateKey";
    }
}


