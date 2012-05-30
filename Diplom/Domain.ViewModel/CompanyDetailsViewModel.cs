using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.ViewModel
{
    public class CompanyDetailsBase
    {
        public Guid Id { get; set; }

        [BsonIgnore]
        public Guid CompanyId { get { return Id; }
            set { Id = value; }
        }
        public string CompanyName { get; set; }

        public List<string> Services { get { return new List<string>
                                                        {
                                                            "Автоэкспертиза",
                                                             "Аренда автомобилей",
                                                              "Изготовление автоключей",
                                                               "Тюнинг кузова",
                                                                "Шиномонтаж и балансировка",
                                                                 "Мойка двигателя",
                                                                  "Развал-схождение",
                                                        }; }
        }

        //new SelectListItem{ Text = "Автоэкспертиза ", Value = "Автоэкспертиза "},
        //                        new SelectListItem{ Text = "Аренда автомобилей", Value = "Аренда автомобилей"},
        //                        new SelectListItem{ Text = "Изготовление автоключей", Value = "Изготовление автоключей"},
        //                        new SelectListItem{ Text = "Тюнинг кузова", Value = "Тюнинг кузова"},
        //                        new SelectListItem{ Text = "Шиномонтаж и балансировка", Value = "Шиномонтаж и балансировка"},
        //                        new SelectListItem{ Text = "Мойка двигателя", Value = "Мойка двигателя"},
        //                        new SelectListItem{ Text = "Развал-схождение", Value = "Развал-схождение"},

        public List<SelectListItem> ServicesAll { 
            get
            {

                return Services.Select(s => new SelectListItem{ Text = s, Value = s, Selected = true}).ToList();
            }
        }

        public Guid CreatedBy { get; set; }

        [BsonIgnore]
        public bool CurrentUserAreOwner { get; private set; }
        public void SetPermission(Guid userId)
        {
            CurrentUserAreOwner = CreatedBy == userId;
        }
    }

    public class PhotosCompanyDetailsViewModel : CompanyDetailsBase
    {
        public List<string> Photos { get; set; }
    }

    public class ReviewCompanyDetailsViewModel : CompanyDetailsBase
    {
        public List<CompanyReviewViewModel> Reviews { get; set; }
    }


    public class DetailsViewModel : CompanyDetailsBase
    {
        public string CompanyDescription { get; set; }
        public string CompanyAddress { get; set; }

        public List<string> News { get; set; }
    }

    public class CompanyWorksPage : CompanyDetailsBase
    {
        public List<CompanyWork> Works { get; set; }
    }

    public class CompanyWork
    {
        public Guid WorkId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}