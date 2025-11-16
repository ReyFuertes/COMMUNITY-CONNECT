using MediatR;
using UserAndUnitManagement.Domain.Entities;
using System.Collections.Generic;

namespace UserAndUnitManagement.Application.Features.Pets.Queries
{
    public class GetAllPetsQuery : IRequest<IEnumerable<Pet>>
    {
    }
}