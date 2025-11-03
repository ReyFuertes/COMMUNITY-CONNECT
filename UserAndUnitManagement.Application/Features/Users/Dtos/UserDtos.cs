namespace UserAndUnitManagement.Application.Features.Users.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Role { get; set; } // Using int for enum for simplicity in DTO
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool OptInToDirectory { get; set; }
        public bool ShowEmailInDirectory { get; set; }
    }

    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool IsActive { get; set; }
        public bool OptInToDirectory { get; set; }
        public bool ShowEmailInDirectory { get; set; }
    }

    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Role { get; set; }
        public bool IsActive { get; set; }
        public bool OptInToDirectory { get; set; }
        public bool ShowEmailInDirectory { get; set; }
    }
}