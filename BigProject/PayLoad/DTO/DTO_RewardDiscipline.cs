using BigProject.Entities;

namespace BigProject.PayLoad.DTO
{
    public class DTO_RewardDiscipline
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string RewardDisciplineType { get; set; }
        public string RecipientName { get; set; }
        public string ProposerName { get; set; }
    }
}
