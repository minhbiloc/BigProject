using BigProject.PayLoad.Request;
using BigProject.Service.Implement;
using BigProject.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Event : ControllerBase
    {
        private readonly IService_Event service_Event;

        public Controller_Event(IService_Event service_Event)
        {
            this.service_Event = service_Event;
        }
        [HttpPost("Thêm hoạt động")]
        public async Task<IActionResult> AddEvent(Request_AddEvent request)
        {
            return Ok(await service_Event.AddEvent(request));
        }
        [HttpPut("Sửa đổi hoạt động")]
        public async Task<IActionResult> UpdateEvent(Request_UpdateEvent request)
        {
            return Ok(await service_Event.UpdateEvent(request));
        }
        [HttpDelete("Xóa hoạt động")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            return Ok(await service_Event.DeleteEvent(eventId));
        }
        [HttpGet("Lấy danh sách hoạt động")]
        public IActionResult GetListProductFull(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_Event.GetListEvent(pageSize, pageNumber));
        }
    }
}
