using System.Collections.Generic;
using System.Web.Mvc;
using Diplom.Controllers;
using Diplom.Models;
using Diplom.ViewModels;
using Moq;
using Xunit;

namespace Tests
{
    public class CompaniesControllerTests
    {
        private Mock<ICompanyRepository> repositoryMock;
        private CompaniesController controler;
        public CompaniesControllerTests()
        {
           repositoryMock = new Mock<ICompanyRepository>();
           controler = new CompaniesController(repositoryMock.Object);
        }



        [Fact]
        public void List_return_comanies()
        {
            var companies = new List<Company>();

            repositoryMock.Setup(r => r.GetAll()).Returns(companies);
            var model = (controler.List() as ViewResult).Model as List<Company>;


            Assert.Equal(companies, model);
        }

        [Fact]
        public void CreatePost_setNameAndDescription_saveComanyInRepositoryAnd()
        {
            controler.Create(new CreateComanyViewModel { Name = "Name", Description = "Description" });


            repositoryMock.Verify(r => r.Save(It.Is<Company>(c => c.Name == "Name" && c.Description == "Description")));
        }

        [Fact]
        public void CreatePost_succesCreate_redirectToNewCompanyDetail()
        {
            repositoryMock.Setup(r => r.Save(It.IsAny<Company>())).Returns(27);


            var view = controler.Create(new CreateComanyViewModel()) as RedirectToRouteResult;


            Assert.Equal("Details", view.RouteValues["action"]);
            Assert.Equal(27, view.RouteValues["id"]);
        }


        [Fact]
        public void CreateGet_displayNotNullCreateComanyViewModel()
        {
            var view = controler.Create() as ViewResult;


            Assert.NotNull(view.Model as CreateComanyViewModel);
            Assert.Empty(view.ViewName);
        }

    }



}
