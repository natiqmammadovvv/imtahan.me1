using System.ComponentModel.DataAnnotations;

namespace imtahan.me.ViewModels
{
    public class RegisterVM
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }
     
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
       
        
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
       
    }
}
