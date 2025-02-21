namespace BigProject.Entities
{
    public class Report : EntityBase
    {
        public string ReportName { get; set; }
        public string Description { get; set; }
        public int ReportTypeId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ReportType ReportType { get; set;}
    }
}
