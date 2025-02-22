using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.Payload.Response;
using BigProject.Service.Interface;
using BigProject.PayLoad.Request;
using BigProject.DataContext;
using BigProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigProject.Service.Implement
{
    public class Service_ReportType : IService_ReportType
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_ReportType> responseObject;
        private readonly Converter_ReportType converter_ReportType;
        private readonly ResponseBase responseBase;

        public Service_ReportType(AppDbContext dbContext, ResponseObject<DTO_ReportType> responseObject, Converter_ReportType converter_ReportType, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter_ReportType = converter_ReportType;
            this.responseBase = responseBase;
        }

        public ResponseObject<DTO_ReportType> AddReportType(Request_AddReportType request)
        {
            var Check_name = dbContext.reportTypes.FirstOrDefault(x => x.ReportTypeName == request.ReportTypeName);
            if (Check_name != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "báo cáo đã tồn tại", null);
            }
            var reportType = new ReportType();
            reportType.ReportTypeName = request.ReportTypeName;
            dbContext.reportTypes.Add(reportType);
            dbContext.SaveChanges();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_ReportType.EntityToDTO(reportType));
        }

        public  ResponseBase DeleteReportType(int Id)
        {

            var ReportType =  dbContext.reportTypes.FirstOrDefault(x => x.Id == Id);
            if (ReportType == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound, "báo cáo không tồn tại!");
            }
            dbContext.reportTypes.Remove(ReportType);
             dbContext.SaveChanges();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }

        public IQueryable<DTO_ReportType> GetListReportType(int pageSize, int pageNumber)
        {
            return dbContext.reportTypes.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_ReportType.EntityToDTO(x));
        }

        public ResponseObject<DTO_ReportType> UpdateReportType(Request_UpdateReportType request)
        {
            var Check_Id = dbContext.reportTypes.FirstOrDefault(x => x.Id == request.Id);
            if (Check_Id == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "báo cáo đã tồn tại", null);
            }
            var reportType = new ReportType();
            reportType.ReportTypeName = request.ReportTypeName;
            dbContext.reportTypes.Update(reportType);
            dbContext.SaveChanges();
            return responseObject.ResponseObjectSuccess("sửa thành công!", converter_ReportType.EntityToDTO(reportType));
        }
    }
}
