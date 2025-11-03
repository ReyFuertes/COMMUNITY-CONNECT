using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Text;

namespace UserAndUnitManagement.Application.Features.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Generate a salt
            var salt = Guid.NewGuid().ToString();

            using (var sha256 = SHA256.Create())
            {
                // Hash the password with the salt
                var passwordWithSalt = request.Password + salt;
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));
                var passwordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    Salt = salt,
                    Role = request.Role,
                    IsActive = request.IsActive,
                    OptInToDirectory = request.OptInToDirectory,
                    ShowEmailInDirectory = request.ShowEmailInDirectory,
                    CreatedDate = DateTime.UtcNow
                };

                await _userRepository.AddAsync(user);
                return user;
            }
        }
    }
}
