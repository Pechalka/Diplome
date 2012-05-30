using System.Web.Mvc;
using Diplom.Controllers;
using Diplom.Models;
using Diplom.ViewModels;
using Moq;
using Xunit;

namespace Tests
{
    public class CreateUserTests
    {
        private readonly Mock<IUserRepository> userServiceMock;
        private readonly AccountController controller;

        public CreateUserTests()
        {
            userServiceMock = new Mock<IUserRepository>();

            controller = new AccountController(userServiceMock.Object, null);
        }

        [Fact]
        public void user_can_be_created()
        {
            var form = new UserForm
                           {
                               Login = "test",
                               Password = "123"
                           };


            controller.Registration(form);


            userServiceMock.Verify(s => s.Save(It.Is<User>(u => 
                u.Login == form.Login && 
                u.Password == form.Password && 
                u.IsActive == false
                )));
        }

        [Fact]
        public void when_user_are_created_should_redirect_to_Companies_List()
        {
            var result = controller.Registration(new UserForm
            {
                Login = "test",
                Password = "123"
            }) as RedirectToRouteResult;



            Assert.Equal(result.RouteValues["action"], "List");
            Assert.Equal(result.RouteValues["controller"], "Companies");
        }

        [Fact]
        public void cannot_save_invalid_user_data()
        {
            controller.ModelState.AddModelError("error", "error");


            var result = controller.Registration(new UserForm
            {
                Login = "test",
                Password = "123"
            });


            userServiceMock.Verify(s => s.Save(It.IsAny<User>()), Times.Never());

            Assert.IsType(typeof(ViewResult), result);
        }

        [Fact]
        public void can_show_Registration_form()
        {
            var result = controller.Registration() as ViewResult;


            Assert.NotNull(result.Model as UserForm);
            Assert.Empty(result.ViewName);
        }
    }
}


