using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpareManagement.Models
{
    public class LoginViewModel
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "*必填")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "*必填")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
