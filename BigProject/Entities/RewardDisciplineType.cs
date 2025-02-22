namespace BigProject.Entities
{
    public class RewardDisciplineType : EntityBase
    {
        public string RewardDisciplineTypeName { get; set; }
        public bool RewardOrDiscipline { get; set; }
        ICollection<RewardDiscipline> RewardDisciplines { get; set;}
    }
}
