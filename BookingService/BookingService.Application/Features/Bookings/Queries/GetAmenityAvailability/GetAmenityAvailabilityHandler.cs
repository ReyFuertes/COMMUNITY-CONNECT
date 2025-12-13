using MediatR;
using BookingService.Domain.Enums;
using BookingService.Domain.Interfaces;

namespace BookingService.Application.Features.Bookings.Queries.GetAmenityAvailability
{
    public class GetAmenityAvailabilityHandler : IRequestHandler<GetAmenityAvailabilityQuery, List<TimeSpan>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAmenityRepository _amenityRepository;

        public GetAmenityAvailabilityHandler(IBookingRepository bookingRepository, IAmenityRepository amenityRepository)
        {
            _bookingRepository = bookingRepository;
            _amenityRepository = amenityRepository;
        }

        public async Task<List<TimeSpan>> Handle(GetAmenityAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var amenity = await _amenityRepository.GetByIdAsync(request.AmenityId);
            if (amenity == null) return new List<TimeSpan>();

            var bookingsForDay = await _bookingRepository.GetByAmenityIdAndDateRangeAsync(
                request.AmenityId, request.Date.Date, request.Date.Date.AddDays(1).AddTicks(-1));

            var availableSlots = new List<TimeSpan>();
            // Simplified: Assume amenity is available from 9 AM to 5 PM, 1-hour slots
            for (int hour = 9; hour <= 17; hour++)
            {
                var slotStart = request.Date.Date.AddHours(hour);
                var slotEnd = slotStart.AddHours(1);

                bool isBooked = bookingsForDay.Any(b =>
                    (b.Status == BookingStatus.Approved || b.Status == BookingStatus.Pending) &&
                    (slotStart < b.EndTime && slotEnd > b.StartTime));

                if (!isBooked)
                {
                    availableSlots.Add(TimeSpan.FromHours(hour));
                }
            }

            return availableSlots;
        }
    }
}