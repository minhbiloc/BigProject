using BigProject.DataContext;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.Payload.Response;
using BigProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Azure.Core;
using BigProject.Entities;

namespace BigProject.Service.Implement
{
    public class Service_RewardDisciplineType : IService_RewardDisciplineType
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_RewardDisciplineType> responseObject;
        private readonly Converter_RewardDisciplineType converter_RewardDisciplineType;
        private readonly ResponseBase responseBase;

        public Service_RewardDisciplineType(AppDbContext dbContext, ResponseObject<DTO_RewardDisciplineType> responseObject, Converter_RewardDisciplineType converter_RewardDisciplineType, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter_RewardDisciplineType = converter_RewardDisciplineType;
            this.responseBase = responseBase;
        }

        public async Task<ResponseObject<DTO_RewardDisciplineType>> AddDisciplineType(string disciplineTypeName)
        {
            var disciplineTypeNameCheck = await dbContext.rewardDisciplineTypes.FirstOrDefaultAsync(x => x.RewardDisciplineTypeName == disciplineTypeName && x.RewardOrDiscipline == false);
            if(disciplineTypeNameCheck != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Loại kỷ luật đã tồn tại!", null);
            }
            var disciplineType = new RewardDisciplineType();
            disciplineType.RewardDisciplineTypeName = disciplineTypeName;
            disciplineType.RewardOrDiscipline = false;
            dbContext.rewardDisciplineTypes.Add(disciplineType);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_RewardDisciplineType.EntityToDTO(disciplineType));

        }

        public async Task<ResponseObject<DTO_RewardDisciplineType>> AddRewardType(string rewardTypeName)
        {
            var rewardTypeNameCheck = await dbContext.rewardDisciplineTypes.FirstOrDefaultAsync(x => x.RewardDisciplineTypeName == rewardTypeName && x.RewardOrDiscipline == true);
            if (rewardTypeNameCheck != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Loại kỷ luật đã tồn tại!", null);
            }
            var rewardType = new RewardDisciplineType();
            rewardType.RewardDisciplineTypeName = rewardTypeName;
            rewardType.RewardOrDiscipline = true;
            dbContext.rewardDisciplineTypes.Add(rewardType);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_RewardDisciplineType.EntityToDTO(rewardType));
        }

        public async Task<ResponseBase> DeleteRewardDisciplineType(int id)
        {
            var rewardDisciplineType = await dbContext.rewardDisciplineTypes.FirstOrDefaultAsync(x=>x.Id==id);
            if (rewardDisciplineType == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound, "Không tồn tại!");
            }
            dbContext.rewardDisciplineTypes.Remove(rewardDisciplineType);
            await dbContext.SaveChangesAsync();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }

        public IQueryable<DTO_RewardDisciplineType> GetListDisciplineType(int pageSize, int pageNumber)
        {
            return dbContext.rewardDisciplineTypes.Where(x => x.RewardOrDiscipline == false).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_RewardDisciplineType.EntityToDTO(x));
        }

        public IQueryable<DTO_RewardDisciplineType> GetListRewardType(int pageSize, int pageNumber)
        {
            return dbContext.rewardDisciplineTypes.Where(x => x.RewardOrDiscipline == true).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_RewardDisciplineType.EntityToDTO(x));
        }
    }
}
