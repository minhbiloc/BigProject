namespace BigProject.PayLoad.Request
{
    public class Request_Register
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string MaTV { get; set; }
        public string? Class { get; set; }
        public DateTime Birthdate { get; set; }
       
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public IFormFile? UrlAvatar { get; set; }


    }
}
