using BigProject.PayLoad.Request;
using BigProject.Service.Implement;
using BigProject.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_ReportType : ControllerBase
    {
        private readonly IService_ReportType service_ReportType;

        public Controller_ReportType(IService_ReportType service_ReportType)
        {
            this.service_ReportType = service_ReportType;
        }
        [HttpPost("Thêm loại báo cáo")]
        public IActionResult AddReportType(Request_AddReportType request)
        {
            return Ok(service_ReportType.AddReportType(request));

        }
        [HttpPut("Sửa loại báo cáo")]
        public IActionResult UpdateReportType(Request_UpdateReportType request)
        {
            return Ok(service_ReportType.UpdateReportType(request));

        }

        [HttpGet("Lấy danh sách loại báo cáo")]
        public IActionResult GetListReportType(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_ReportType.GetListReportType(pageSize, pageNumber));
        }
    }
}
