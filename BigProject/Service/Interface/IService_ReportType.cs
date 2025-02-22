using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Payload.Response;

namespace BigProject.Service.Interface
{
    public interface IService_ReportType
    {
        ResponseObject<DTO_ReportType> AddReportType(Request_AddReportType request);
       ResponseObject<DTO_ReportType> UpdateReportType(Request_UpdateReportType request);
        ResponseBase DeleteReportType(int Id);
        IQueryable<DTO_ReportType> GetListReportType(int pageSize, int pageNumber);
    }
}
