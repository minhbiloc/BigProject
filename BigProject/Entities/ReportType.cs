namespace BigProject.Entities
{
    public class ReportType : EntityBase
    {
        public string ReportTypeName { get; set; }
        ICollection<Report> Reports { get;}
    }
}
