using System.ComponentModel.DataAnnotations;

namespace DemoStore.WebUI.Models
{
    /// <summary>
    /// This class contains the information required to login to the application.
    /// </summary>
    public class LogOnViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}