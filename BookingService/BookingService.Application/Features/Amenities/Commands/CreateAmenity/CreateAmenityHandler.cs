using AutoMapper;
using MediatR;
using BookingService.Application.Dtos;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces;

namespace BookingService.Application.Features.Amenities.Commands.CreateAmenity
{
    public class CreateAmenityHandler : IRequestHandler<CreateAmenityCommand, AmenityDto>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public CreateAmenityHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<AmenityDto> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenity = _mapper.Map<Amenity>(request);
            amenity.Id = Guid.NewGuid();
            amenity.CreatedAt = DateTime.UtcNow;

            await _amenityRepository.AddAsync(amenity);

            return _mapper.Map<AmenityDto>(amenity);
        }
    }
}