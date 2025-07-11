namespace CustomerManagement.Application.Dtos.Auth
{
    public class SignInResponse
    {
        public string Token { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public bool IsSuccess { get; set; }

        public string Message
        {
            get; set;
        }
    }
}
