namespace BigProject.PayLoad.Request
{
    public class Request_ChangePassword
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string newpassword { get; set; }

        public string renewpassword { get; set; }
    }
}
