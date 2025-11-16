namespace UserAndUnitManagement.Application.Features.Users.Dtos
{
    public class UserDto
    {
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required int Role { get; set; } // Using int for enum for simplicity in DTO
        public required bool IsActive { get; set; }
        public required DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public required bool OptInToDirectory { get; set; }
        public required bool ShowEmailInDirectory { get; set; }
    }

    public class CreateUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required int Role { get; set; }
        public required bool IsActive { get; set; }
        public required bool OptInToDirectory { get; set; }
        public required bool ShowEmailInDirectory { get; set; }
    }

    public class UpdateUserDto
    {
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Role { get; set; }
        public required bool IsActive { get; set; }
        public required bool OptInToDirectory { get; set; }
        public required bool ShowEmailInDirectory { get; set; }
    }
}