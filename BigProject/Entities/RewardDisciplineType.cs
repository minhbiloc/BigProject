namespace BigProject.Entities
{
    public class RewardDisciplineType : EntityBase
    {
        public string RewardDisciplineTypeName { get; set; }
        ICollection<RewardDiscipline> RewardDisciplines { get; set;}
    }
}
