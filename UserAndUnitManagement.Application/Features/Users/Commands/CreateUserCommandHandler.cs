using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
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
