namespace NewsWebsiteAPI.Models.Authentication.Login
{
    public class AuthModel
    {
        public AuthModel()
        {
            List<string?> Roles = new List<string>();
        }
        public string Id { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string?> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
