using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;

namespace BigProject.PayLoad.Converter
{
    public class Converter_ReportStatistics
    {
        private readonly AppDbContext appDbContext;

        public Converter_ReportStatistics(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public DTO_ReportStatistics EntityToDTO(Report report)
            
        {
            return new DTO_ReportStatistics
            {
                Description = report.Description,
                Id = report.Id,
                ReportTypeId = report.ReportTypeId,
                UserId = report.UserId,
            };
        }
    }
}
