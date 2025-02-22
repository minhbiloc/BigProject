using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Payload.Response;

namespace BigProject.Service.Interface
{
    public interface IService_RewardDisciplineType
    {
        Task<ResponseObject<DTO_RewardDisciplineType>> AddRewardType(string rewardTypeName);
        Task<ResponseObject<DTO_RewardDisciplineType>> AddDisciplineType(string disciplineTypeName);
        IQueryable<DTO_RewardDisciplineType> GetListRewardType(int pageSize, int pageNumber);
        IQueryable<DTO_RewardDisciplineType> GetListDisciplineType(int pageSize, int pageNumber);
        Task<ResponseBase> DeleteRewardDisciplineType(int id);
    }
}
