using BigProject.DataContext;
using BigProject.PayLoad.DTO;
using BigProject.Payload.Response;
using BigProject.Service.Interface;
using BigProject.PayLoad.Request;
using BigProject.Entities;
using BigProject.PayLoad.Converter;

namespace BigProject.Service.Implement
{
    public class Service_ReportStatistics : IService_ReportStatistics
    {
        private readonly AppDbContext dbContext;

        private readonly ResponseObject<DTO_ReportStatistics> responseObject;
        private readonly Converter_ReportStatistics converter_ReportStatistics;
        private readonly ResponseBase responseBase;

        public Service_ReportStatistics(AppDbContext dbContext, ResponseObject<DTO_ReportStatistics> responseObject, Converter_ReportStatistics converter_ReportStatistics, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter_ReportStatistics = converter_ReportStatistics;
            this.responseBase = responseBase;
        }

        public ResponseObject<DTO_ReportStatistics> AddReport(Resquest_AddReport request)
        {
           var check_reportTypeId = dbContext.reportTypes.FirstOrDefault(x=>x.Id == request.ReportTypeId);
            if (check_reportTypeId == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Loại báo cáo không tồn tại",null);
            }
            var check_UserID = dbContext.users.FirstOrDefault( x=>x.Id ==  request.UserId);
            if (check_UserID == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Id  không tồn tại", null);
            }
            var report = new Report();
            report.ReportTypeId = request.ReportTypeId;
            report.UserId = request.UserId;
            report.ReportName = request.ReportName;
            report.Description = request.Description;
            dbContext.Add(report);
            dbContext.SaveChanges();
            return responseObject.ResponseObjectSuccess("Thêm báo cáo thành công ", converter_ReportStatistics.EntityToDTO(report));
        }

        public ResponseBase DeleteReport(int Id)
        {
          var Check_Id = dbContext.reports.FirstOrDefault( x=>x.Id == Id);
            if (Check_Id == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound, "Báo cáo  không tồn tại!");
            }
            dbContext.reports.Remove(Check_Id);
            dbContext.SaveChanges();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }
    }
}
