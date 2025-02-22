using BigProject.DataContext;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.Payload.Response;
using BigProject.Service.Interface;
using BigProject.PayLoad.Request;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using BigProject.Entities;

namespace BigProject.Service.Implement
{
    public class Service_RewardDiscipline : IService_RewardDiscipline
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_RewardDiscipline> responseObject;
        private readonly Converter_RewardDiscipline converter_RewardDiscipline;
        private readonly ResponseBase responseBase;

        public Service_RewardDiscipline(AppDbContext dbContext, ResponseObject<DTO_RewardDiscipline> responseObject, Converter_RewardDiscipline converter_RewardDiscipline, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter_RewardDiscipline = converter_RewardDiscipline;
            this.responseBase = responseBase;
        }

        public async Task<ResponseBase> DeleteRewardDiscipline(int id)
        {
            var Reward = await dbContext.rewardDisciplines.FirstOrDefaultAsync(x => x.Id == id);
            if(Reward == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound, "Đề xuất này không tồn tại!"); 
            };
            dbContext.rewardDisciplines.Remove(Reward);
            await dbContext.SaveChangesAsync();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }

        public IQueryable<DTO_RewardDiscipline> GetListDiscipline(int pageSize, int pageNumber)
        {
            return dbContext.rewardDisciplines.Where(x => x.RewardOrDiscipline == false).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_RewardDiscipline.EntityToDTO(x));
        }

        public IQueryable<DTO_RewardDiscipline> GetListReward(int pageSize, int pageNumber)
        {
            return dbContext.rewardDisciplines.Where(x => x.RewardOrDiscipline == true).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_RewardDiscipline.EntityToDTO(x));

        }

        public async Task<ResponseObject<DTO_RewardDiscipline>> ProposeReward(Request_ProposeRewardDiscipline request, int proposerId)
        {
            var proposerCheck = await dbContext.users.FirstOrDefaultAsync(x => x.Id == proposerId);
            if(proposerCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người đề xuất không tồn tại!", null);
            }
            var recipientCheck = await dbContext.users.FirstOrDefaultAsync(x => x.Id == request.RecipientId);
            if(recipientCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người được đề xuất không tồn tại!", null);
            }
            var rewardTypeCheck = await dbContext.rewardDisciplineTypes.FirstOrDefaultAsync(x => x.Id == request.RewardDisciplineTypeId && x.RewardOrDiscipline == true);
            if(rewardTypeCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Loại giải thưởng không tồn tại!", null);
            }
            var proposer = new RewardDiscipline();
            proposer.RewardOrDiscipline = true;
            proposer.Description = request.Description;
            proposer.RewardDisciplineTypeId = request.RewardDisciplineTypeId;
            proposer.Status = "Waiting";
            proposer.RecipientId = request.RecipientId;
            proposer.ProposerId = proposerId;
            dbContext.rewardDisciplines.Add(proposer);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_RewardDiscipline.EntityToDTO(proposer));
        }

        public async Task<ResponseObject<DTO_RewardDiscipline>> ProposeDiscipline(Request_ProposeRewardDiscipline request, int proposerId)
        {
            var proposerCheck = await dbContext.users.FirstOrDefaultAsync(x => x.Id == proposerId);
            if (proposerCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người đề xuất không tồn tại!", null);
            }
            var recipientCheck = await dbContext.users.FirstOrDefaultAsync(x => x.Id == request.RecipientId);
            if (recipientCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người được đề xuất không tồn tại!", null);
            }
            var disciplineTypeCheck = await dbContext.rewardDisciplineTypes.FirstOrDefaultAsync(x => x.Id == request.RewardDisciplineTypeId && x.RewardOrDiscipline == false);
            if (disciplineTypeCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Loại kỷ luật không tồn tại!", null);
            }
            var proposer = new RewardDiscipline();
            proposer.RewardOrDiscipline = false;
            proposer.Description = request.Description;
            proposer.RewardDisciplineTypeId = request.RewardDisciplineTypeId;
            proposer.Status = "Waiting";
            proposer.RecipientId = request.RecipientId;
            proposer.ProposerId = proposerId;
            dbContext.rewardDisciplines.Add(proposer);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_RewardDiscipline.EntityToDTO(proposer));
        }

        public async Task<ResponseObject<DTO_RewardDiscipline>> AcceptPropose(int proposeId)
        {
            var propose = await dbContext.rewardDisciplines.FirstOrDefaultAsync(x => x.Id == proposeId);
            if (propose == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đề xuất không tồn tại!", null);
            }
            propose.Status = "Accept";
            dbContext.rewardDisciplines.Update(propose);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Chấp nhận!", converter_RewardDiscipline.EntityToDTO(propose));
        }

        public async Task<ResponseObject<DTO_RewardDiscipline>> RejectPropose(int proposeId)
        {
            var propose = await dbContext.rewardDisciplines.FirstOrDefaultAsync(x => x.Id == proposeId);
            if (propose == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đề xuất không tồn tại!", null);
            }
            propose.Status = "Reject";
            dbContext.rewardDisciplines.Update(propose);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Từ chối!", converter_RewardDiscipline.EntityToDTO(propose));
        }
    }
}
