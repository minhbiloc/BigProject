using BigProject.DataContext;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.Payload.Response;
using BigProject.Service.Interface;
using BigProject.PayLoad.Request;
using BigProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigProject.Service.Implement
{
    public class Service_Event : IService_Event
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_Event> responseObject;
        private readonly Converter_Event converter_Event;
        private readonly ResponseBase responseBase;

        public Service_Event(AppDbContext dbContext, ResponseObject<DTO_Event> responseObject, Converter_Event converter_Event, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter_Event = converter_Event;
            this.responseBase = responseBase;
        }

        public async Task<ResponseObject<DTO_Event>> AddEvent(Request_AddEvent request)
        {
            var eventType_check = await dbContext.eventTypes.FirstOrDefaultAsync(x => x.Id == request.EventTypeId);
            if (eventType_check == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, " Loại hoạt động không tồn tại! ", null);
            }
            var eventName_check = await dbContext.events.FirstOrDefaultAsync(x => x.EventName == request.EventName);
            if (eventName_check != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, " Tên hoạt động không được trùng! ", null);
            }
            var event1 = new Event();
            event1.EventName = request.EventName;
            event1.EventLocation = request.EventLocation;
            event1.EventStartDate = request.EventStartDate;
            event1.EventEndDate = request.EventEndDate;
            event1.Description = request.Description;
            event1.EventTypeId = request.EventTypeId;
            dbContext.events.Add(event1);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_Event.EntityToDTO(event1));
        }

        public async Task<ResponseBase> DeleteEvent(int eventId)
        {
            var event1 = await dbContext.events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (event1 == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound,"Hoạt động không tồn tại!");
            }
            dbContext.events.Remove(event1);
            await dbContext.SaveChangesAsync();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }

        public IQueryable<DTO_Event> GetListEvent(int pageSize, int pageNumber)
        {
            return dbContext.events.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_Event.EntityToDTO(x));
        }

        public async Task<ResponseObject<DTO_Event>> UpdateEvent(Request_UpdateEvent request)
        {
            var event1 = await dbContext.events.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (event1 == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Hoạt động không tồn tại!", null);
            }
            var eventType_check = await dbContext.eventTypes.FirstOrDefaultAsync(x => x.Id == request.EventTypeId);
            if (eventType_check == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Loại hoạt động không tồn tại!", null);
            }
            var eventName_check = await dbContext.events.FirstOrDefaultAsync(x => x.EventName == request.EventName);
            if (eventName_check != null && event1.EventName != request.EventName)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Tên hoạt động không được trùng! ", null);
            }
            event1.EventName = request.EventName;
            event1.EventLocation = request.EventLocation;
            event1.EventStartDate = request.EventStartDate;
            event1.EventEndDate = request.EventEndDate;
            event1.Description = request.Description;
            event1.EventTypeId = request.EventTypeId;
            dbContext.events.Update(event1);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_Event.EntityToDTO(event1));
        }

        
    }
}
