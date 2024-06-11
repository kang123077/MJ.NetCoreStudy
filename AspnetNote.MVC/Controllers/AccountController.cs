using AspnetNote.MVC.DataContext;
using AspnetNote.MVC.Models;
using AspnetNote.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspnetNote.MVC.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인 뷰
        /// </summary>
        /// <returns></returns>
        [HttpGet] // 명시를 안해도 기본
        public IActionResult Login()
        {
            return View();
        }

        //뷰를 만들기 전에 일반적으로 C# 코드를 먼저 작업하는 편이다
        // 로그인 할 때 [HttpGet]을 통해서 하는 경우가 있다 초보경우
        // Login(string id, string password)
        // Route("http://www.example.com/login/honggil/123456")
        // 이 경우 쿠키 등 기록이 남아서 이렇게 쓰면 안된다
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            ClaimsIdentity? identity = null;
            bool isAuthenticated = false;
            // ID, 비밀번호 필수 입력
            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    // Linq 쿼리식 laguage itegrated quary?
                    // ex ) select, where, from ...
                    // Linq - 메서드 체이닝, 메서드가 연속으로 이어짐
                    // Print().Delete()...

                    // A => B : A go to B, 어떤 인수가 있을거야. 뭔진 모르겠고
                    // FirstOrDefault() 첫번째 혹은 기본 값을 입력하겠다는 의미

                    // Java도 C#도 == 를 사용하면 메모리에 누수가 일어난다.
                    // == 를 사용하게 되면, UserId를 새로운 객체를 생성해서 가져온것으로 되어버린다
                    // 따라서 == 대신에 이들이 같다는 것을 확인하기 위해 Equal()을 사용한다.
                    var user = db.Users.FirstOrDefault
                        (u => u.UserId.Equals(model.UserId) && u.UserPassword.Equals(model.UserPassword));
                    // 로그인에 성공 했을 때
                    if (user != null)
                    {
                        HttpContext.Session.SetString("username", model.UserId);
                        isAuthenticated = true;
                        identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, model.UserId)},
                        CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        // 로그인 정보를 전송
                        // HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNum);
                        return RedirectToAction("LoginSuccess", "Home"); // 로그인 성공 페이지로
                        // 만약 사용자 자체가 회원가입이 X일 경우
                        // 앞에서 아이디, 비밀번호를 찾을 때 분기 설정을 해주면 된다
                        // 근데 이렇게 하지 마! 아이디가 있나 없나 조차도 정보, 프라이버시가 될 수 있다.
                    }
                }
                // 로그인에 실패했을 때, Using문을 쓸 필요조차 없어서 Using이 빨리 닫히는 효과도 있음.
                ModelState.AddModelError(string.Empty, "사용자 id 혹은 비밀번호가 올바르지 않습니다.");
            }
            return View();
        }
        // 전역적인 경고 메세지를 주기 위해서는 asp-validation-summary="ModelOnly"를 HTML상에서 사용한다

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("username");
            // HttpContext.Session.Remove("USER_LOGIN_KEY");
            // 모든 세션이 삭제되므로, Clear는 필요에 의한 것이 아니며 관리자가 아니면 쓸 일 X
            // HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 회원 가입 뷰
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            // 데이터베이스 입출력 시 받을 때 열고 닫아야 함. 메모리 누수 방지
            // 그러나 그것을 직접 작성하기보다는 C#에서는 using문을 사용한다.
            // Java Ex ) try(SqlSession){} catch(){}
            // C#
            if (ModelState.IsValid) // 사용자에게 모든 내용을 입력 받았는지 확인 가능
            {
                using (var db = new AspnetNoteDbContext())
                {
                    db.Users.Add(model);
                    // 이것만 있어도 메모리 상으로는 올라간다
                    db.SaveChanges();
                    // 이걸 넣어야 db에 저장 or 삭제가 된다.
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }

    public class CheckSession : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var check = context.HttpContext;
            if (check.Session.GetString("username") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", Controller = "Account" }));
            }
        }
    }
}
