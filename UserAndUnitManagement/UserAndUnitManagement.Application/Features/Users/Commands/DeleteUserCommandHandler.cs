using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UserAndUnitManagement.Application.Features.Users.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public DeleteUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new ApplicationException($"User with id {request.Id} not found.");
            }

            await _userRepository.DeleteAsync(user);
        }
    }
}
