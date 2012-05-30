using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Diplom.Controllers;
using Diplom.Models;
using Diplom.ViewModels;
using Moq;
using Xunit;

namespace Tests
{
    public class UserCanActivateAndDeactivateTest
    {
        private readonly Mock<IUserRepository> usersMock;
        private readonly AccountController controller;

        public UserCanActivateAndDeactivateTest()
        {
            usersMock = new Mock<IUserRepository>();

            controller = new AccountController(usersMock.Object, null);
        }

        [Fact]
        public void when_user_registern_it_not_active()
        {
            var form = new UserForm
            {
                Login = "test",
                Password = "123"
            };


            controller.Registration(form);


            usersMock.Verify(s => s.Save(It.Is<User>(u => u.IsActive == false )));
        }

        [Fact]
        public void user_can_be_activated_or_deactivated()
        {
            Guid userId = Guid.NewGuid();
            const bool newState = true;

            usersMock.Setup(r => r.Users).Returns(new List<User>
                                                      {
                                                          new User
                                                              {
                                                                  Id = userId,
                                                                  IsActive = false
                                                              }
                                                      }.AsQueryable());


            controller.SetUserState(userId, newState);


            usersMock.Verify(r => r.Save(It.Is<User>(u =>
                    u.Id == userId &&
                    u.IsActive == newState
                )), Times.Once());
        }


        [Fact]
        public void when_user_activate_should_redirect_to_Account_List()
        {
            Guid userId = Guid.NewGuid();
            usersMock.Setup(r => r.Users).Returns(new List<User>
                                                      {
                                                          new User
                                                              {
                                                                  Id = userId,
                                                                  IsActive = false
                                                              }
                                                      }.AsQueryable());


            var result = controller.SetUserState(userId, true) as RedirectToRouteResult;



            Assert.Equal(result.RouteValues["action"], "List");
            Assert.Equal(result.RouteValues["controller"], "Account");
        }
    }
}
