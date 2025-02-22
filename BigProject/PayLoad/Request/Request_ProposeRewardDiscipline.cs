using BigProject.Entities;

namespace BigProject.PayLoad.Request
{
    public class Request_ProposeRewardDiscipline
    {
        public string Description { get; set; }
        public int RewardDisciplineTypeId { get; set; }
        public int RecipientId { get; set; }
    }
}
