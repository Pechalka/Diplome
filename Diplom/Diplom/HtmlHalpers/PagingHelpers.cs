using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Diplom.HtmlHalpers
{
    public static class PagingHelpers
    {
      //              <div class="pagination pagination-centered">
      //  <ul>
      //    <li class="active"><a href="#">1</a></li>
      //    <li><a href="#">2</a></li>
      //    <li><a href="#">3</a></li>
      //    <li><a href="#">4</a></li>
      //    <li><a href="#">5</a></li>
      //  </ul>
      //</div>




        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pageInfo, Func<int, string> pageUrlDelegate)
        {
            if (pageInfo.TotalPages <= 1)
                return new MvcHtmlString(string.Empty);

            var result = new StringBuilder();
            for (int page = 1; page <= pageInfo.TotalPages; page++)
            {
                var a = new TagBuilder("a") { InnerHtml = page.ToString()};
                a.MergeAttribute("href", pageUrlDelegate(page));

                var li = new TagBuilder("li") {InnerHtml = a.ToString()};
                
                if (page == pageInfo.CurrentPage)
                    li.AddCssClass("active");

                result.Append(li.ToString());
            }
            var ul = new TagBuilder("ul") {InnerHtml = result.ToString()};
            var div = new TagBuilder("div") {InnerHtml = ul.ToString()};

            div.AddCssClass("pagination pagination-centered");

            return new MvcHtmlString(div.ToString());
        }
    }
}