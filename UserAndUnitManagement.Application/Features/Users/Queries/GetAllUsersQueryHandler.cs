using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using UserAndUnitManagement.Application.Features.Users.Dtos;

namespace UserAndUnitManagement.Application.Features.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IRepository<User> _userRepository;

        public GetAllUsersQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = (int)u.Role,
                IsActive = u.IsActive,
                CreatedDate = u.CreatedDate,
                LastModifiedDate = u.LastModifiedDate,
                OptInToDirectory = u.OptInToDirectory,
                ShowEmailInDirectory = u.ShowEmailInDirectory
            });
        }
    }
}
