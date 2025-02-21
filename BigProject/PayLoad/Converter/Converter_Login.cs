using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;

namespace BigProject.PayLoad.Converter
{
    public class Converter_Login
    {
        private readonly AppDbContext _context;

        public Converter_Login(AppDbContext context)
        {
            _context = context;
        }
        public DTO_Login EntityToDTO(User login)
        {
            return new DTO_Login
            {
                Id = login.Id,
                UserName = login.Username,
                Password = login.Password,
                MaTV = login.MaTV,
                Email = login.Email,
            };
        }
    }
}
