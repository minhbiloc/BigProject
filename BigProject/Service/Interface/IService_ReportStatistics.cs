using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Payload.Response;

namespace BigProject.Service.Interface
{
    public interface IService_ReportStatistics
    {
        ResponseObject<DTO_ReportStatistics> AddReport(Resquest_AddReport request);
        ResponseBase DeleteReport(int Id);
    }
}
