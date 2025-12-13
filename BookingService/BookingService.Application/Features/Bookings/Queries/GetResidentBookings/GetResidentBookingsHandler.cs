using AutoMapper;
using MediatR;
using BookingService.Application.Dtos;
using BookingService.Domain.Interfaces;

namespace BookingService.Application.Features.Bookings.Queries.GetResidentBookings
{
    public class GetResidentBookingsHandler : IRequestHandler<GetResidentBookingsQuery, List<BookingDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetResidentBookingsHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<List<BookingDto>> Handle(GetResidentBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetByResidentIdAsync(request.ResidentId);
            return _mapper.Map<List<BookingDto>>(bookings);
        }
    }
}