namespace UserAndUnitManagement.Application.Features.Users.Dtos
{
    public class LoginDto
    {
        private string _email;
        private string _password;

        public required string Email
        {
            get => _email;
            set => _email = value.Trim();
        }

        public required string Password
        {
            get => _password;
            set => _password = value.Trim();
        }
    }
}