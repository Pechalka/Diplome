using System.ComponentModel.DataAnnotations;
namespace Diplom.ViewModels
{
    public class UserForm
    {
	[Required]
        public string Login { get; set; }

	[Required]
        public string Password { get; set; }
    }
}