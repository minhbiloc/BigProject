using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;

namespace BigProject.PayLoad.Converter
{
    public class Converter_ReportType
    {
        private readonly AppDbContext appDbContext;

        public Converter_ReportType(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public DTO_ReportType EntityToDTO(ReportType reportType)
        {
            return new DTO_ReportType
            {
                EventTypeName = reportType.ReportTypeName,
                Id = reportType.Id,
            };
                
        }
    }
}
