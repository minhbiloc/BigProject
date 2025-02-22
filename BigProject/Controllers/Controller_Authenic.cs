using BigProject.Entities;
using BigProject.PayLoad.Request;
using BigProject.Service.Implement;
using BigProject.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Authenic : ControllerBase
    {
        private readonly IService_Authentic service_Authentic;

        public Controller_Authenic(IService_Authentic service_Authentic)
        {
            this.service_Authentic = service_Authentic;
        }
        [HttpPost("Đăng kí")]
        public IActionResult Register(Request_Register request)
        {
            return Ok(service_Authentic.Register(request));

        }

        [HttpPut("Quên mật khẩu")]
        public IActionResult ForgotPassword(Request_forgot request)
        {
            return Ok(service_Authentic.ForgotPassword(request));
        }
        [HttpPost("Đăng nhập")]
        public IActionResult Login(Request_Login request)
        {
            return Ok(service_Authentic.Login(request));
        }
        [HttpPut("kích hoạt tài khoản")]
        public IActionResult Activate(string Opt)
        {
            return Ok(service_Authentic.Activate(Opt));
        }
        [HttpGet("phân quyền")]

        public IActionResult Authorization(string KeyRole)
        {
            return Ok(service_Authentic.Authorization(KeyRole));
        }
        [HttpPut("đổi mật khẩu")]
        public IActionResult ChangePassword(Request_ChangePassword request)
        {
            return Ok(service_Authentic.ChangePassword(request));
        }
        [HttpGet("Xem danh sách")]
        public IActionResult GetListMember(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_Authentic.GetListMember(pageSize, pageNumber));
        }
    }
    

}
