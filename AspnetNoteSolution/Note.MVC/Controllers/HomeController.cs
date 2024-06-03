using Microsoft.AspNetCore.Mvc;
using Note.BLL;
using Note.MVC.Models;
using System.Diagnostics;

namespace Note.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserBll _userBll;

        // 생성자를 통해 파라미터를 받아서 객체를 전역 변수에 넣어준다.
        // 이때 userBll을 new 하는 것은 Program.cs에서 대신 해준다.
        // 의존성 (객체) 주입을 진행한 상황이다.
        public HomeController(UserBll userBll)
        {
            _userBll = userBll;
        }

        public IActionResult Index()
        {
            var userList = _userBll.GetUserList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
