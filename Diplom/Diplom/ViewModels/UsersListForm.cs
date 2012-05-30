using System.Collections.Generic;
using Diplom.HtmlHalpers;
using Diplom.Models;

namespace Diplom.ViewModels
{
    public class UsersListForm
    {
        public List<User> Users { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}