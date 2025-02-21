using BigProject.Entities;

namespace BigProject.PayLoad.DTO
{
    public class DTO_RewardDiscipline
    {
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool RewardOrDiscipline { get; set; }
        public string Status { get; set; }
        public int RewardOrDisciplineTypeId { get; set; }
        public int UserId { get; set; }
    }
}
