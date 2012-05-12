using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SimpleCqrs.Commanding;

namespace Domain.Commands
{
    public class CreateComanyCommand : ICommand
    {
        public Guid CompanyId { get; set; }

        [Required]
        // [Display("Название компании")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public List<SelectListItem> CategoriesDropdown
        {
            get
            {
                return new List<SelectListItem>
                           {
                               new SelectListItem{ Text = "АВТОЗАПЧАСТИ", Value = "АВТОЗАПЧАСТИ"},
                               new SelectListItem{ Text = "БЫТОВЫЕ УСЛУГИ", Value = "БЫТОВЫЕ УСЛУГИ"},
                               new SelectListItem{ Text = "ФИТНЕС И КРАСОТА", Value = "ФИТНЕС И КРАСОТА"},
                               new SelectListItem{ Text = "СТРОИТЕЛЬСТВО И РЕМОНТ", Value = "СТРОИТЕЛЬСТВО И РЕМОНТ"},
                               new SelectListItem{ Text = "ОБРАЗОВАНИЕ, КУРСЫ", Value = "ОБРАЗОВАНИЕ, КУРСЫ"},
                               new SelectListItem{ Text = "МЕБЕЛЬ И ИНТЕРЬЕР", Value = "МЕБЕЛЬ И ИНТЕРЬЕР"},
                           };
            }
        }
    }
}
