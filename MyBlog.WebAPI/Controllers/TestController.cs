using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebAPI.Controllers
{

    [NonController]
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
     
    }
}
