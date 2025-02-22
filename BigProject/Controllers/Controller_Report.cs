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
    public class Controller_Report : ControllerBase
    {
        private readonly IService_ReportStatistics service_ReportStatistics;

        public Controller_Report(IService_ReportStatistics service_ReportStatistics)
        {
            this.service_ReportStatistics = service_ReportStatistics;
        }

        [HttpPost("Thêm báo cáo")]
        public IActionResult AddReport(Resquest_AddReport request)
        {
            return Ok(service_ReportStatistics.AddReport(request));

        }
        [HttpDelete("Xóa báo cáo")]
        public  IActionResult DeleteReport(int Id)
        {
            return Ok(service_ReportStatistics.DeleteReport(Id));
        }
    }
}
