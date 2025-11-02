using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UserAndUnitManagement.Application.Features.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public UpdateUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new ApplicationException($"User with id {request.Id} not found.");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PasswordHash = request.PasswordHash;
            user.Role = request.Role;
            user.IsActive = request.IsActive;
            user.OptInToDirectory = request.OptInToDirectory;
            user.ShowEmailInDirectory = request.ShowEmailInDirectory;
            user.LastModifiedDate = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }
    }
}
