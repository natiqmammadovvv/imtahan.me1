using System.ComponentModel.DataAnnotations;

namespace imtahan.me.ViewModels
{
    public class SignInVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }
    
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
     
        public bool RememberMe { get; set; }
        
    }
}
