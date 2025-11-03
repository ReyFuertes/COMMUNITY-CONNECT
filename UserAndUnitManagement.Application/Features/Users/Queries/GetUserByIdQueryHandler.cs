using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using UserAndUnitManagement.Application.Features.Users.Dtos;

namespace UserAndUnitManagement.Application.Features.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IRepository<User> _userRepository;

        public GetUserByIdQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = (int)user.Role,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                LastModifiedDate = user.LastModifiedDate,
                OptInToDirectory = user.OptInToDirectory,
                ShowEmailInDirectory = user.ShowEmailInDirectory
            };
        }
    }
}
