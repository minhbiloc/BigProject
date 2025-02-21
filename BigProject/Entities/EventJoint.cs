namespace BigProject.Entities
{
    public class EventJoint : EntityBase
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
