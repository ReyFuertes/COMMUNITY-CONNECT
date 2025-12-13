using AutoMapper;
using MediatR;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces;

namespace BookingService.Application.Features.Amenities.Commands.SetBookingRules
{
    public class SetBookingRulesHandler : IRequestHandler<SetBookingRulesCommand, bool>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IRepository<BookingRule> _bookingRuleRepository; // Assuming generic repo for rules
        private readonly IMapper _mapper;

        public SetBookingRulesHandler(IAmenityRepository amenityRepository, IRepository<BookingRule> bookingRuleRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _bookingRuleRepository = bookingRuleRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SetBookingRulesCommand request, CancellationToken cancellationToken)
        {
            var amenity = await _amenityRepository.GetByIdAsync(request.AmenityId);
            if (amenity == null) return false;

            // Simple implementation: remove all existing rules and add new ones
            // In a real app, this would involve comparing and updating/deleting specific rules
            if (amenity.Rules != null && amenity.Rules.Any())
            {
                foreach (var rule in amenity.Rules.ToList()) // ToList() to avoid modification during enumeration
                {
                    await _bookingRuleRepository.DeleteAsync(rule);
                }
            }

            foreach (var ruleDto in request.Rules)
            {
                var rule = _mapper.Map<BookingRule>(ruleDto);
                rule.Id = Guid.NewGuid();
                rule.AmenityId = request.AmenityId;
                await _bookingRuleRepository.AddAsync(rule);
            }
            
            return true;
        }
    }
}