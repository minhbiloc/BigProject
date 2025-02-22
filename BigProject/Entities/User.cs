namespace BigProject.Entities
{
    public class User : EntityBase
    {
        public string MaTV { get; set; }
        public string? Class { get; set; }
        public DateTime Birthdate { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string PhoneNumber { get; set; }
        public string UrlAvatar { get; set; }

      

        public ICollection<RefreshToken> refreshTokens { get; set; }
        public ICollection<EmailConfirm> emailConfirms { get; set; }
        public ICollection<Report> reports {  get; set; }
    }
}
