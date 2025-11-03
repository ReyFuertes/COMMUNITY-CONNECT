using MediatR;
using UserAndUnitManagement.Domain.Entities;
using System;

namespace UserAndUnitManagement.Application.Features.Pets.Queries
{
    public class GetPetByIdQuery : IRequest<Pet?>
    {
        public Guid Id { get; set; }
    }
}