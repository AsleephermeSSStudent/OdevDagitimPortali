using System.ComponentModel.DataAnnotations;

namespace OdevDagitim.ViewModel
{
    public class RegisterModel
    {

       [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
    [Display(Name = "Kullanıcı Adı")]
    public string Username { get; set; }

    [Required(ErrorMessage = "E-posta zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    [Display(Name = "E-posta")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Parola zorunludur")]
    [StringLength(100, ErrorMessage = "Parola en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Parola")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Parola Tekrar")]
    [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor")]
    public string ConfirmPassword { get; set; }
    }
}
