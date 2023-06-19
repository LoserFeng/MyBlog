using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Personal;

namespace MyBlog.WebAPI.Controllers
{

    [Route("[controller]/[action]")]
    public class PersonalController : Controller
    {
        private readonly ILogger<PersonalController> _logger;
        private readonly IUserInfoService _userInfoService;


        public PersonalController(IUserInfoService userInfoService, ILogger<PersonalController> logger)
        {
            _userInfoService = userInfoService;
            _logger = logger;
        }




        [Route("~/Personal")]
        [Route("~/Personal/Index")]
        public async Task<IActionResult> Index(int Id)
        {
            var model =await  _userInfoService.GetPersonalInfo(Id);

            return View(model);
        }
    }
}
