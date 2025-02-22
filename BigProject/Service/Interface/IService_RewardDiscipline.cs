using BigProject.Payload.Response;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;

namespace BigProject.Service.Interface
{
    public interface IService_RewardDiscipline
    {
        Task<ResponseObject<DTO_RewardDiscipline>> ProposeReward(Request_ProposeRewardDiscipline request,int proposerId);
        Task<ResponseObject<DTO_RewardDiscipline>> ProposeDiscipline(Request_ProposeRewardDiscipline request,int proposerId);
        Task<ResponseBase> DeleteRewardDiscipline(int id);
        IQueryable<DTO_RewardDiscipline> GetListReward(int pageSize, int pageNumber);
        IQueryable<DTO_RewardDiscipline> GetListDiscipline(int pageSize, int pageNumber);
        Task<ResponseObject<DTO_RewardDiscipline>> AcceptPropose(int proposeId);
        Task<ResponseObject<DTO_RewardDiscipline>> RejectPropose(int proposeId);
    }
}
