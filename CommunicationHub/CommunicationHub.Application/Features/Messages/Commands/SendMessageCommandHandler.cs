using CommunicationHub.Application.Dtos;
using CommunicationHub.Application.Interfaces;
using CommunicationHub.Domain.Entities;
using CommunicationHub.Domain.Interfaces;
using MediatR;

namespace CommunicationHub.Application.Features.Messages.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageDto>
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<Conversation> _conversationRepository;
        private readonly INotificationService _notificationService;

        public SendMessageCommandHandler(
            IRepository<Message> messageRepository, 
            IRepository<Conversation> conversationRepository,
            INotificationService notificationService)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _notificationService = notificationService;
        }

        public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            // Verify conversation exists
            var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId);
            if (conversation == null)
            {
                throw new KeyNotFoundException($"Conversation with ID {request.ConversationId} not found.");
            }

            var message = new Message
            {
                Id = Guid.NewGuid(),
                ConversationId = request.ConversationId,
                SenderId = request.SenderId,
                Content = request.Content,
                SentDate = DateTime.UtcNow,
                IsRead = false
            };

            await _messageRepository.AddAsync(message);

            var messageDto = new MessageDto
            {
                Id = message.Id,
                ConversationId = message.ConversationId,
                SenderId = message.SenderId,
                Content = message.Content,
                SentDate = message.SentDate
            };

            // Notify SignalR clients in the conversation group
            await _notificationService.SendMessageNotificationAsync(request.ConversationId, messageDto);

            return messageDto;
        }
    }
}
