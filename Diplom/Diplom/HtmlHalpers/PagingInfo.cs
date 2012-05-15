using System;

namespace Diplom.HtmlHalpers
{
    public class PagingInfo
    {
        public int TotalItem { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPage
        {
            get { return (int)Math.Ceiling((decimal)TotalItem / ItemsPerPage); }
        }
    }
}