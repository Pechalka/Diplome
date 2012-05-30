using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Diplom.Controllers;
using Diplom.Models;
using Diplom.ViewModels;
using Moq;
using Xunit;

namespace Tests
{
    public class UserCanLoginTests
    {
        private Mock<IAuthProvider> authMock;
        private Mock<IUserRepository> usersMock;
        private AccountController controller;

        public UserCanLoginTests()
        {
            authMock = new Mock<IAuthProvider>();
            usersMock = new Mock<IUserRepository>();

            controller = new AccountController(usersMock.Object, authMock.Object);
        }

        [Fact]
        public void user_can_login_if_user_have_in_database_and_active()
        {
            var form = new LogOnForm
                           {
                               Login = "Login",
                               Password = "Password",
                           };

            usersMock.Setup(r => r.Users).Returns(new List<User>
                                                      {
                                                          new User
                                                              {
                                                                  Login = form.Login,
                                                                  Password = form.Password,
                                                                  IsActive = true
                                                              }
                                                      }.AsQueryable());


            controller.LogOn(form);


            authMock.Verify(a => a.Authenticate(It.Is<User>(u => u.Login == form.Login)), Times.Once());
        }

        [Fact]
        public void user_can_not_login_if_user_not_exist_in_database()
        {
            var form = new LogOnForm
                           {
                               Login = "Login",
                               Password = "Password",
                           };


            controller.LogOn(form);


            authMock.Verify(a => a.Authenticate(It.IsAny<User>()), Times.Never());
        }


        [Fact]
        public void user_can_not_login_if_user_exist_in_database_but_not_active()
        {
            var form = new LogOnForm
                           {
                               Login = "Login",
                               Password = "Password",
                           };

            usersMock.Setup(r => r.Users).Returns(new List<User>
                                                      {
                                                          new User
                                                              {
                                                                  Login = form.Login,
                                                                  Password = form.Password,
                                                                  IsActive = false
                                                              }
                                                      }.AsQueryable());


            controller.LogOn(form);


            authMock.Verify(a => a.Authenticate(It.IsAny<User>()), Times.Never());
        }


        [Fact]
        public void when_user_login_success_should_redirect_to_Companies_List()
        {
            var form = new LogOnForm
            {
                Login = "Login",
                Password = "Password",
            };

            usersMock.Setup(r => r.Users).Returns(new List<User>
                                                      {
                                                          new User
                                                              {
                                                                  Login = form.Login,
                                                                  Password = form.Password,
                                                                  IsActive = true
                                                              }
                                                      }.AsQueryable());


            var result = controller.LogOn(form) as RedirectToRouteResult;


            Assert.Equal(result.RouteValues["action"], "List");
            Assert.Equal(result.RouteValues["controller"], "Companies");
        }


        [Fact]
        public void when_user_login_fail_should_return_current_page()
        {
            var form = new LogOnForm
            {
                Login = "Login",
                Password = "Password",
            };


            var result = controller.LogOn(form) as ViewResult;


            Assert.Empty(result.ViewName);
            Assert.Equal(form, result.Model);
        }


        [Fact]
        public void can_not_send_empty_form()
        {
            var form = new LogOnForm
            {
                Login = "Login",
                Password = "Password",
            };

            controller.ModelState.AddModelError("error", "error");


            var result = controller.LogOn(form) as ViewResult;


            Assert.Empty(result.ViewName);
            Assert.Equal(form, result.Model);
        }


        [Fact]
        public void can_show_logOn_form()
        {
            var result = controller.LogOn() as ViewResult;


            Assert.Empty(result.ViewName);
            Assert.NotNull(result.Model as LogOnForm);
        }
    }
}

