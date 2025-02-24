using BigProject.DataContext;
using BigProject.Entities;
using BigProject.Helper;
using BigProject.Payload.Response;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BigProject.Service.Implement
{
    public class Service_Authentic : IService_Authentic
    {
        private readonly AppDbContext dbContext;

        private readonly ResponseObject<DTO_Register> responseObject;
        private readonly Converter_Register converter_Register;
        private readonly ResponseBase responseBase;
        private readonly ResponseObject<DTO_Token> responseObjectToken;
        private readonly IConfiguration configuration;
        private readonly ResponseObject<List<DTO_Register>> responseObjectList;
        private readonly ResponseObject<DTO_Login> responseObjectLogin;
        private readonly Converter_Login  converter_Login;

        public Service_Authentic(AppDbContext dbContext, ResponseObject<DTO_Register> responseObject, Converter_Register converter_Register, ResponseBase responseBase, ResponseObject<DTO_Token> responseObjectToken, IConfiguration configuration, ResponseObject<List<DTO_Register>> responseObjectList, ResponseObject<DTO_Login> responseObjectLogin, Converter_Login converter_Login)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter_Register = converter_Register;
            this.responseBase = responseBase;
            this.responseObjectToken = responseObjectToken;
            this.configuration = configuration;
            this.responseObjectList = responseObjectList;
            this.responseObjectLogin = responseObjectLogin;
            this.converter_Login = converter_Login;
        }

        public ResponseBase Activate(string Opt)
        {
            var comfirmEmail = dbContext.emailConfirms.FirstOrDefault(x => x.Code == Opt);
            if (comfirmEmail == null)
            {
                return responseBase.ResponseBaseError(400, "Mã xác nhận không đúng !");
            }

            comfirmEmail.IsConfirmed = true;
            dbContext.emailConfirms.Update(comfirmEmail);
            dbContext.SaveChanges();

            return responseBase.ResponseBaseSuccess("Kích hoạt tài khoản thành công !");
        }

        public  ResponseObject<DTO_Register> ForgotPassword(Request_forgot request)
        {
            var user =  dbContext.users.FirstOrDefault(x => x.Email == request.Email);
            if (user == null)
            {
                return  responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Email  không tồn tại !", null);
            }

            Random random = new Random(); 
            int code = random.Next(100000, 999999);

            EmailTo emailTo = new EmailTo();
            emailTo.Mail = request.Email;
            emailTo.Subject = "MÃ XÁC NHẬN !";
            emailTo.Content = $"Mật khẩu là {code} mã sẽ hết hạn sau 5 phút!";
            emailTo.SendEmailAsync(emailTo);
            user.Password = BCrypt.Net.BCrypt.HashPassword(code.ToString());
            dbContext.users.Update(user);
            dbContext.SaveChanges();
            return responseObject.ResponseObjectSuccess("Đã gửi mật khẩu qua Email ", null);
        }

        public ResponseObject<DTO_Token> Login(Request_Login request)
        {
            var user = dbContext.users.FirstOrDefault(x => x.Username == request.UserName || x.Email == request.UserName ||  x.MaTV == request.UserName);
            if (user == null)
            {
                return responseObjectToken.ResponseObjectError(404, "Tài khoản không tồn tại !", null);
            }
           
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return responseObjectToken.ResponseObjectError(404, "Sai mk !", null);
            }


            var comfimEmail = dbContext.emailConfirms.FirstOrDefault(x => x.UserId == user.Id);
            if (comfimEmail.IsConfirmed == false)
            {
                return responseObjectToken.ResponseObjectError(404, "Tài khoản chưa được kích hoạt !", null);
            }



            return responseObjectToken.ResponseObjectSuccess("đăng nhập email thành công ", GenerateAccessToken(user));

        }

        public async Task<ResponseObject<DTO_Register>>  Register(Request_Register request)
        {
            var msv_check  = dbContext.users.FirstOrDefault(x => x.MaTV == request.MaTV);
            if (msv_check != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, " Mã Sinh viên  đã tồn tại ", null);
            }
            var name_check = dbContext.users.FirstOrDefault(x => x.Username == request.Username);
            if (name_check != null)
            {
                return  responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Tài khoản đã tồn tại ", null);
            }
            var email_check = dbContext.users.FirstOrDefault(x => x.Email == request.Email);
            if (email_check != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Email đã tồn tại", null);
            }
            if (CheckInput.IsPassWord(request.Password) != request.Password)
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, CheckInput.IsPassWord(request.Password), null);
            var phone_check = dbContext.users.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);
            if (phone_check != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "số điện thoại đã tồn tại", null);
            }
            var checkEmail = CheckInput.IsValiEmail(request.Email);
            if (!checkEmail)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Email không hợp lệ (thiếu ký tự đặc biệt hoặc sai định dạng)", null);
            }

            string UrlAvt = null;
            var cloudinary = new CloudinaryService();
            if (request.UrlAvatar == null)
            {
                UrlAvt = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
            }
            else
            {
                if (!CheckInput.IsImage(request.UrlAvatar))
                {
                    return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Định dạng ảnh không hợp lệ !", null);
                }

                UrlAvt = await cloudinary.UploadImage(request.UrlAvatar);
            }

            var register = new User();
            register.MaTV = request.MaTV;
            request.Class = request.Class;
            register.Birthdate = request.Birthdate;
            register.Username = request.Username;
            register.Password = request.Password;
            register.Email = request.Email;
            register.PhoneNumber = request.PhoneNumber;         
            register.UrlAvatar  = UrlAvt;
            register.FullName = request.FullName;
           

            register.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            Random random = new Random();
            int code = random.Next(100000, 999999);

            EmailTo emailTo = new EmailTo();
            emailTo.Mail = request.Email;
            emailTo.Subject = "MÃ XÁC NHẬN !";
            emailTo.Content = $"Mã xác nhận của bạn là: {code} mã sẽ hết hạn sau 5 phút!";
            emailTo.SendEmailAsync(emailTo);

            register.Email = request.Email;
            register.RoleId = 1;
            dbContext.users.Add(register);
            dbContext.SaveChanges();

            EmailConfirm comfirmEmail = new EmailConfirm();
            comfirmEmail.UserId = register.Id;
            comfirmEmail.Code = $"{code}";
            comfirmEmail.Exprired = DateTime.Now.AddMinutes(5);
            dbContext.emailConfirms.Add(comfirmEmail);
            dbContext.SaveChanges();

            return   responseObject.ResponseObjectSuccess("đăng kí thành công",  converter_Register.EntityToDTO(register));
        }
        public async Task<ResponseObject<DTO_Token>> RenewAccessToken(DTO_Token request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);

            var tokenValidation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value))
            };

            try
            {
                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.AccessToken, tokenValidation, out var validatedToken);
                if (validatedToken is not JwtSecurityToken jwtSecurityToken || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return responseObjectToken.ResponseObjectError(StatusCodes.Status400BadRequest, "Token không hợp lệ", null);
                }
                RefreshToken refreshToken = await dbContext.refreshTokens.FirstOrDefaultAsync(x => x.Token == request.RefreshToken);
                if (refreshToken == null)
                {
                    return responseObjectToken.ResponseObjectError(StatusCodes.Status404NotFound, "RefreshToken không tồn tại trong database", null);
                }
                if (refreshToken.Exprited < DateTime.Now)
                {
                    return responseObjectToken.ResponseObjectError(StatusCodes.Status401Unauthorized, "Token chưa hết hạn", null);
                }
                var user = dbContext.users.FirstOrDefault(x => x.Id == refreshToken.UserId);
                if (user == null)
                {
                    return responseObjectToken.ResponseObjectError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
                }
                var newToken = GenerateAccessToken(user);

                return responseObjectToken.ResponseObjectSuccess("Làm mới token thành công", newToken);
            }
            catch (Exception ex)
            {
                return responseObjectToken.ResponseObjectError(StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        private DTO_Token GenerateAccessToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);

            var decentralization = dbContext.roles.FirstOrDefault(x => x.Id == user.RoleId);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
             new Claim("Id", user.Id.ToString()),
             new Claim(ClaimTypes.Email, user.Email),
             new Claim("Username", user.Username),
             new Claim("RoleId", user.RoleId.ToString()),   
             
             //new Claim("UrlAvatar", user.UrlAvatar.ToString()),
            
             new Claim(ClaimTypes.Role, decentralization?.Name ?? "")
         }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                Exprited = DateTime.Now.AddHours(4),
                UserId = user.Id
            };

            dbContext.refreshTokens.Add(rf);
            dbContext.SaveChanges();

            DTO_Token tokenDTO = new DTO_Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return tokenDTO;
        }

        public ResponseBase ChangePassword(Request_ChangePassword requset)
        {
            var change = dbContext.users.FirstOrDefault(x => x.Username.ToLower() == requset.UserName.ToLower() || x.MaTV.ToLower() == requset.UserName.ToLower() || x.Email.ToLower() == requset.UserName.ToLower());
            if (change == null)
            {
                return responseBase.ResponseBaseError(404, "tên tài khoản không chính xác");
            }
            if (!BCrypt.Net.BCrypt.Verify(requset.Password, change.Password))
            {
                return responseBase.ResponseBaseError(404, "Mật khẩu không chính xác");
            }
            if (requset.newpassword != requset.renewpassword)
            {
                return responseBase.ResponseBaseError(404, "mật khẩu không trùng nhau");
            }
            if (requset.newpassword == requset.Password)
            {
                return responseBase.ResponseBaseError(404, "mật khẩu mới trùng mật khẩu cũ");
            }
            if (CheckInput.IsPassWord(requset.newpassword) != requset.newpassword)
            {
                return responseBase.ResponseBaseError(404, CheckInput.IsPassWord(requset.newpassword));
            }

            change.Password = BCrypt.Net.BCrypt.HashPassword(requset.Password);
            dbContext.Update(change);
            dbContext.SaveChanges();

            return responseBase.ResponseBaseSuccess("Đổi mật khẩu thành công");
        }

        public ResponseObject<List<DTO_Register>> Authorization(string KeyRole)
        {
            var listUserForRoleInput = dbContext.users.Include(user => user.Role).AsNoTracking().Where(user => user.Role.Name.ToLower() == KeyRole.ToLower());



            if (!listUserForRoleInput.Any())
            {
                return responseObjectList.ResponseObjectError(StatusCodes.Status404NotFound, "Bảng không tồn tại", null);
            }
            return responseObjectList.ResponseObjectSuccess("hiện thành công", listUserForRoleInput.Select(x => converter_Register.EntityToDTO(x)).ToList());
        }

        public IQueryable<DTO_Register> GetListMember(int pageSize, int pageNumber)
        {
            return dbContext.users.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_Register.EntityToDTO(x));
        }
    }
}
