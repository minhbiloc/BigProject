using BigProject.Payload.Response;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;

namespace BigProject.Service.Interface
{
    public interface IService_Authentic
    {
        Task <ResponseObject<DTO_Register>> Register(Request_Register request);

        ResponseObject<DTO_Register> ForgotPassword(Request_forgot request);

        ResponseObject<DTO_Token> Login(Request_Login request);

        ResponseBase Activate(string Opt);

        ResponseBase ChangePassword(Request_ChangePassword requset);

        ResponseObject<List<DTO_Register>> Authorization(string KeyRole);

        IQueryable<DTO_Register> GetListMember(int pageSize, int pageNumber);
    }
}
