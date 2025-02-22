using BigProject.Payload.Response;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;

namespace BigProject.Service.Interface
{
    public interface IService_Event
    {
        Task<ResponseObject<DTO_Event>> AddEvent(Request_AddEvent request);
        Task<ResponseObject<DTO_Event>> UpdateEvent(Request_UpdateEvent request);
        Task<ResponseBase> DeleteEvent(int Id);
        IQueryable<DTO_Event> GetListEvent(int pageSize, int pageNumber);
    }
}
