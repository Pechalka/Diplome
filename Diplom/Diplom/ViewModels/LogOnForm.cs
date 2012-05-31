using System.ComponentModel.DataAnnotations;
namespace Diplom.ViewModels
{
    public class LogOnForm
    {
	[Required]
        public string Login { get; set; }

	[Required]
        public string Password { get; set; }
    }
}