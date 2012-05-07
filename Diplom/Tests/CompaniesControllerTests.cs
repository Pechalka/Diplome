using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Diplom.Controllers;
using Domain;
using Domain.Commands;
using Moq;
using Xunit;

namespace Tests
{
    //public class CompaniesControllerTests
    //{
    //    private Mock<ICompanyRepository> repositoryMock;
    //    private CompaniesController controler;
    //    public CompaniesControllerTests()
    //    {
    //       repositoryMock = new Mock<ICompanyRepository>();
    //       controler = new CompaniesController(repositoryMock.Object);
    //    }



    //    [Fact]
    //    public void List_return_comanies()
    //    {
    //        var companies = new List<Company>();

    //        repositoryMock.Setup(r => r.GetAll()).Returns(companies);
    //        var model = (controler.List() as ViewResult).Model as List<Company>;


    //        Assert.Equal(companies, model);
    //    }

    //    [Fact]
    //    public void CreatePost_setNameAndDescriptionAndCategory_saveComanyInRepositoryAnd()
    //    {
    //        controler.Create(new CreateComanyCommand { Name = "Name", Description = "Description", Category = "Category" });


    //        repositoryMock.Verify(r => r.Save(It.Is<Company>(c => c.Name == "Name" && c.Description == "Description" && c.Category == "Category")));
    //    }

    //    //[Fact]
    //    //public void CreatePost_succesCreate_redirectToNewCompanyDetail()
    //    //{
    //    //    var id = Guid.NewGuid();
    //    //    repositoryMock.Setup(r => r.Save(It.IsAny<Company>())).Returns(id);


    //    //    var view = controler.Create(new CreateComanyCommand()) as RedirectToRouteResult;


    //    //    Assert.Equal("Details", view.RouteValues["action"]);
    //    //    Assert.Equal(id, view.RouteValues["id"]);
    //    //}


    //    [Fact]
    //    public void CreateGet_displayNotNullCreateComanyViewModel()
    //    {
    //        var view = controler.Create() as ViewResult;


    //        Assert.NotNull(view.Model as CreateComanyCommand);
    //        Assert.Empty(view.ViewName);
    //    }

    //    //[Fact]
    //    //public void ChangeComapny_post_Company_gets_from_repository_and_save_In_it()
    //    //{
    //    //    var companyFromDb = new Company();
    //    //    repositoryMock.Setup(r => r.GetBy("7")).Returns(companyFromDb);


    //    //    controler.Change(new ChangeCompanyCommand{ Id = "7" });


    //    //    repositoryMock.Verify(r => r.Save(companyFromDb));
    //    //}

    //    [Fact]
    //    public void ChangeComapny_post_saveCompanyInRepositpory()
    //    {
    //        repositoryMock.Setup(r => r.GetBy(It.IsAny<string>())).Returns(new Company());

    //        var form = new ChangeCompanyCommand
    //                       {
    //                           Name = "Name",
    //                           Description = "Description",
    //                           Address = "Address"
    //                       };


    //        controler.Change(form);


    //        repositoryMock.Verify(r => r.Save(It.Is<Company>(
    //                c => c.Name == form.Name &&
    //                     c.Description == form.Description &&
    //                     c.Address == form.Address 
    //            )));

    //    }

    //    [Fact]
    //    public void ChangeComapny_post_redirectToDetailsPage()
    //    {
    //        repositoryMock.Setup(r => r.GetBy(It.IsAny<string>())).Returns(new Company());
    //        const string companyId = "88";

    //        var view = controler.Change(new ChangeCompanyCommand { Id = companyId }) as RedirectToRouteResult;


    //        Assert.Equal("Details", view.RouteValues["action"]);
    //        Assert.Equal(companyId, view.RouteValues["id"]);
    //    }

    //    [Fact]
    //    public void ChangeComapny_get_return_viewModel_from_repository()
    //    {
    //        var company = new Company
    //                          {
    //                              Id = "7",
    //                              Address = "Address",
    //                              Description = "Description",
    //                              Name = "Name"
    //                          };
    //        repositoryMock.Setup(r => r.GetBy("7")).Returns(company);


            
    //        var view = controler.Change("7") as ViewResult;
    //        var viewModel = view.Model as ChangeCompanyCommand;



    //        Assert.Equal(viewModel.Name, company.Name);
    //        Assert.Equal(viewModel.Description, company.Description);
    //        Assert.Equal(viewModel.Address, company.Address);
    //        Assert.Equal(viewModel.Id, company.Id);

    //    }

    //    [Fact]
    //    public void ChangeComapny_get_display_in_valid_view()
    //    {
    //        repositoryMock.Setup(r => r.GetBy("7")).Returns(new Company());


    //        var view = controler.Change("7") as ViewResult;


    //        Assert.Empty(view.ViewName);
    //    }
    //}



}
