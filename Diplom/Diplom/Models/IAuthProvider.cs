using System.Web.Security;
using System;
using System.Web;

namespace Diplom.Models
{
    public interface IAuthProvider
    {
       // void Authenticate(string login);
        void Authenticate(User user);
        Guid CurrentUserId { get;  }
    }

    public class FormAuthProvider : IAuthProvider
    {
        public void Authenticate(string userName)
        {
            FormsAuthentication.SetAuthCookie(userName, false);
        }

        public void Authenticate(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Login, false);
            HttpContext.Current.Session["UserId"] = user.Id;
        }

        public Guid CurrentUserId
        {
            get
            {
                if (HttpContext.Current.Session["UserId"] == null)
                    return Guid.Empty;

                return (Guid) HttpContext.Current.Session["UserId"];
            }
        }
    }
}