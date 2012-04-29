using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Diplom
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


        public static HtmlString PageLinks(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            if (totalPages <= 1)
                return new HtmlString("");

            var result = new StringBuilder();
            for (int page = 1; page <= totalPages; page++)
            {
                var tab = new TagBuilder("a");
                tab.MergeAttribute("href", pageUrl(page));
                tab.InnerHtml = page.ToString();

                var li = new TagBuilder("li") { InnerHtml = tab.ToString() };
                if (page == currentPage)
                    li.AddCssClass("active");

                result.AppendLine(li.ToString());
            }
            var ul = new TagBuilder("ul") { InnerHtml = result.ToString() };
            var div = new TagBuilder("div") {InnerHtml = ul.ToString()};

            div.AddCssClass("pagination pagination-centered");

            return new HtmlString(div.ToString());
        }

    }
}