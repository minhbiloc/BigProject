using BigProject.PayLoad.Request;
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
    public class Controller_RewardDisciplineType : ControllerBase
    {
        private readonly IService_RewardDisciplineType service_RewardDisciplineType;

        public Controller_RewardDisciplineType(IService_RewardDisciplineType service_RewardDisciplineType)
        {
            this.service_RewardDisciplineType = service_RewardDisciplineType;
        }

        [HttpGet("Lấy danh sách loại khen thưởng")]
        public IActionResult GetListRewardTypeFull(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_RewardDisciplineType.GetListRewardType(pageSize, pageNumber));
        }
        [HttpGet("Lấy danh sách loại kỷ luật")]
        public IActionResult GetListDisciplineTypeFull(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_RewardDisciplineType.GetListDisciplineType(pageSize, pageNumber));
        }
        [HttpPost("Thêm loại khen thưởng")]
        public async Task<IActionResult> AddRewardType(string rewardTypeName)
        {
            return Ok(await service_RewardDisciplineType.AddRewardType(rewardTypeName));
        }

        [HttpPost("Thêm loại kỷ luật")]
        public async Task<IActionResult> AddDisciplineType(string disciplineTypeName)
        {
            return Ok(await service_RewardDisciplineType.AddDisciplineType(disciplineTypeName));
        }

        [HttpDelete("Xóa loại khen thưởng/kỷ luật")]
        public async Task<IActionResult> DeleteRewardDisciplineType(int proposeId)
        {
            return Ok(await service_RewardDisciplineType.DeleteRewardDisciplineType(proposeId));
        }
    }
}
