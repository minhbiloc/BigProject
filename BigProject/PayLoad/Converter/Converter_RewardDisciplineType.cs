using BigProject.Entities;
using BigProject.PayLoad.DTO;

namespace BigProject.PayLoad.Converter
{
    public class Converter_RewardDisciplineType
    {
        public DTO_RewardDisciplineType EntityToDTO(RewardDisciplineType rewardDisciplineType)
        {
            return new DTO_RewardDisciplineType()
            {
                Id = rewardDisciplineType.Id,
                RewardDisciplineTypeName = rewardDisciplineType.RewardDisciplineTypeName
            };
        }
    }
}
