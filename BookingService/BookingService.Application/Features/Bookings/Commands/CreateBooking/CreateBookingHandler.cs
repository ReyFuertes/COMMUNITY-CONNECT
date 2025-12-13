using AutoMapper;
using MediatR;
using BookingService.Application.Dtos;
using BookingService.Application.Interfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using BookingService.Domain.Interfaces;

namespace BookingService.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IFinanceIntegrationClient _financeClient;
        private readonly INotificationClient _notificationClient;
        private readonly IUserAndUnitIntegrationClient _userAndUnitClient;
        private readonly IMapper _mapper;

        public CreateBookingHandler(IBookingRepository bookingRepository, IAmenityRepository amenityRepository, IFinanceIntegrationClient financeClient, INotificationClient notificationClient, IUserAndUnitIntegrationClient userAndUnitClient, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _amenityRepository = amenityRepository;
            _financeClient = financeClient;
            _notificationClient = notificationClient;
            _userAndUnitClient = userAndUnitClient;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // Validate Resident and Unit
            if (!await _userAndUnitClient.ResidentExistsAsync(request.ResidentId))
            {
                throw new Exception("Resident not found.");
            }
            if (!await _userAndUnitClient.UnitExistsAsync(request.UnitId))
            {
                throw new Exception("Unit not found.");
            }

            var amenity = await _amenityRepository.GetByIdAsync(request.AmenityId);
            if (amenity == null) throw new Exception("Amenity not found.");

            // Check Availability (Simplified for now)
            var existingBookings = await _bookingRepository.GetByAmenityIdAndDateRangeAsync(
                request.AmenityId, request.StartTime, request.EndTime);
            if (existingBookings.Any(b => b.Status == BookingStatus.Approved || b.Status == BookingStatus.Pending))
            {
                throw new Exception("Amenity is already booked for this time slot.");
            }

            // Apply Booking Rules (Simplified)
            // Example: Max duration rule
            var maxDurationRule = amenity.Rules.FirstOrDefault(r => r.RuleType == BookingRuleType.MaxDuration);
            if (maxDurationRule != null && TimeSpan.TryParse(maxDurationRule.Value, out var maxDuration))
            {
                if ((request.EndTime - request.StartTime) > maxDuration)
                {
                    throw new Exception($"Booking exceeds maximum duration of {maxDuration.TotalHours} hours.");
                }
            }

            // Create Booking
            var booking = _mapper.Map<Booking>(request);
            booking.Id = Guid.NewGuid();
            booking.Amenity = amenity; // Attach amenity for mapping AmenityName later
            booking.Status = amenity.RequiresApproval ? BookingStatus.Pending : BookingStatus.Approved;
            booking.CreatedAt = DateTime.UtcNow;

            // Handle Payment
            if (amenity.BookingFee > 0 || amenity.SecurityDeposit > 0)
            {
                var totalAmount = amenity.BookingFee + amenity.SecurityDeposit;
                var description = $"Booking for {amenity.Name} on {request.StartTime:g}";
                var transactionId = await _financeClient.InitiateAmenityBookingPaymentAsync(request.ResidentId, booking.Id, totalAmount, description);
                booking.PaymentTransactionId = transactionId;
            }

            await _bookingRepository.AddAsync(booking);

            // Send Notification
            if (booking.Status == BookingStatus.Approved)
            {
                await _notificationClient.SendBookingConfirmationAsync(booking.ResidentId, amenity.Name, booking.StartTime);
            }
            // else if (booking.Status == BookingStatus.Pending) // Send pending approval notification

            return _mapper.Map<BookingDto>(booking);
        }
    }
}