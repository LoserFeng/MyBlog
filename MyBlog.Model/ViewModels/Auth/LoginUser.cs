using System.ComponentModel.DataAnnotations;

namespace MyBlog.Model.ViewModels.Auth;

public class LoginUser {




    [Required]
    [Display(Name ="用户名")]
    public string Username { get; set; }
    [Required]
    [Display(Name = "密码")]
    [RegularExpression(@"^.{6,18}$")]
    public string Password { get; set; }



}