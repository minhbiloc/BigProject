namespace BigProject.Entities
{
    public class RewardDiscipline : EntityBase
    {
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool RewardOrDiscipline { get; set; }
        public string Status { get; set; }
        public int RewardOrDisciplineTypeId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public RewardDisciplineType RewardDisciplineType { get; set; }
    }
}
