using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Diplom.HtmlHalpers;
using Xunit;

namespace Tests
{
    public class PagegTests
    {
        [Fact]
        public void helper_should_generage_valid_markup()
        {
            HtmlHelper helper = null;
            var pagingInfo = new PagingInfo
                                        {
                                            CurrentPage = 2,
                                            TotalItem = 28, 
                                            ItemsPerPage = 10
                                        };

            Func<int, string> pageUrlDelegate = n => "Page" + n;



            MvcHtmlString result = helper.PageLinks(pagingInfo, pageUrlDelegate);



            Assert.Equal(result.ToString(), 
                    "<div class=\"pagination pagination-centered\">" +
                        "<ul>" +
                        "<li><a href=\"Page1\">1</a></li>" +
                        "<li class=\"active\"><a href=\"Page2\">2</a></li>" + 
                        "<li><a href=\"Page3\">3</a></li>" +
                        "</ul>" + 
                    "</div>"
                    );
        }

        [Fact]
        public void helper_should_not_generate_markup_if_TotalPages_1_or_les()
        {
            HtmlHelper helper = null;
            var pagingInfo = new PagingInfo
            {
                CurrentPage = 1,
                TotalItem = 9,
                ItemsPerPage = 10
            };

            MvcHtmlString result = helper.PageLinks(pagingInfo, n => n.ToString());


            Assert.Empty(result.ToString());
        }
    }



}
