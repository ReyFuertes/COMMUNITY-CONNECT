using CommunicationHub.Application.Dtos;
using MediatR;

namespace CommunicationHub.Application.Features.Messages.Commands
{
    public class SendMessageCommand : IRequest<MessageDto>
    {
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public required string Content { get; set; }
    }
}
