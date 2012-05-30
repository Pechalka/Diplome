using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Diplom.Controllers;
using Diplom.HtmlHalpers;
using Diplom.Models;
using Diplom.ViewModels;
using Moq;
using Xunit;

namespace Tests
{
    public class UserListTests
    {
        private Mock<IUserRepository> userServiceMock;
        private AccountController controller;

        public UserListTests()
        {
            userServiceMock = new Mock<IUserRepository>();

            controller = new AccountController(userServiceMock.Object, null);
        }

        [Fact]
        public void can_show_users_list()
        {
            userServiceMock.Setup(r => r.Users).Returns(new List<User>
                                                            {
                                                                new User(),
                                                                new User(),
                                                                new User(),
                                                            }.AsQueryable());


            var result = (controller.List() as ViewResult) ;
            var model = result.Model as UsersListForm;


            Assert.Equal(model.Users.Count, 3);
        }

        [Fact]
        public void can_paginate()
        {
            userServiceMock.Setup(r => r.Users).Returns(new List<User>
                                                            {
                                                                new User{ Login = "L1"},
                                                                new User{ Login = "L2"},
                                                                new User{ Login = "L3"},
                                                                new User{ Login = "L4"},
                                                                new User{ Login = "L5"},
                                                            }.AsQueryable());

            controller.PageSize = 3;


            var result = (controller.List(2) as ViewResult);
            var model = result.Model as UsersListForm;


            Assert.Equal(model.Users.Count, 2);
            Assert.Equal(model.Users[0].Login, "L4");
            Assert.Equal(model.Users[1].Login, "L5");

        }


        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            userServiceMock.Setup(r => r.Users).Returns(new List<User>
                                                            {
                                                                new User{ Login = "L1"},
                                                                new User{ Login = "L2"},
                                                                new User{ Login = "L3"},
                                                                new User{ Login = "L4"},
                                                                new User{ Login = "L5"},
                                                            }.AsQueryable());

            controller.PageSize = 3;


            var result = (controller.List(2) as ViewResult);
            var model = result.Model as UsersListForm;


            PagingInfo pagingInfo = model.PagingInfo;
            Assert.Equal(pagingInfo.CurrentPage, 2);
            Assert.Equal(pagingInfo.ItemsPerPage, 3);
            Assert.Equal(pagingInfo.TotalItem, 5);
            Assert.Equal(pagingInfo.TotalPages, 2);
        }
    }
}




