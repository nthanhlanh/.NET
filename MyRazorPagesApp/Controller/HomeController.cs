using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyRazorPagesApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Login success";
            return View();
        }


        // Hiển thị trang đăng nhập
        public IActionResult Login()
        {
            return View();
        }

        // Xử lý yêu cầu đăng nhập bằng AJAX
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            // Logic xác thực người dùng
            if (login.Username == "admin" && login.Password == "password")
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invalid username or password" });
        }
    }
}
