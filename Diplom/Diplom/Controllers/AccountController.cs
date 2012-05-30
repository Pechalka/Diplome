using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Diplom.HtmlHalpers;
using Diplom.Models;
using Diplom.ViewModels;

namespace Diplom.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthProvider _authProvider;

        public AccountController(IUserRepository userRepository, IAuthProvider authProvider)
        {
            _userRepository = userRepository;
            _authProvider = authProvider;
        }

        public int PageSize = 10;

        [HttpPost]
        public ActionResult Registration(UserForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _userRepository.Save(new User
                                  {
                                      Login = form.Login,
                                      Password = form.Password,
                                      IsActive = false
                                  });

            return RedirectToAction("List", "Companies");
        }


        [HttpGet]
        public ActionResult  Registration()
        {
            return View(new UserForm());
        }


        public ActionResult List(int page = 1)
        {
            var users = _userRepository.Users.Skip((page - 1)*PageSize).Take(PageSize);
            var pageInfo = new PagingInfo
                               {
                                   CurrentPage = page,
                                   ItemsPerPage = PageSize,
                                   TotalItem = _userRepository.Users.Count()
                               };

            var form = new UsersListForm
                           {
                               Users = users.ToList(),
                               PagingInfo = pageInfo
                           };
            return View(form);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var user = _userRepository.Users.FirstOrDefault(u => 
                u.Login == form.Login && 
                u.Password == form.Password &&
                u.IsActive
                );
            var exist = user != null;

            if (exist)
            {
                _authProvider.Authenticate(user);
                return RedirectToAction("List", "Companies");
            }

            return View(form);
        }

        [HttpGet]
        public ViewResult LogOn()
        {
            return View(new LogOnForm());
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("List", "Companies");
        }

        public ActionResult SetUserState(Guid userId, bool newState, int page = 1)
        {
            var user = _userRepository.Users.FirstOrDefault(u => u.Id == userId);
            user.IsActive = newState;
            _userRepository.Save(user);


            return RedirectToAction("List", "Account", new { page });
        }
    }
}