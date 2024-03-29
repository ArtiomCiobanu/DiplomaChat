﻿using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing;
using DiplomaChat.DataAccess.Contracts.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Rooms.Notifications.LeaveChatRoom
{
    public class LeaveGameSessionNotificationHandler : IRequestHandler<LeaveChatRoomNotificationCommand>
    {
        private readonly IMessageQueuePublisher _leaveChatRoomPublisher;
        private readonly IDiplomaChatContext _diplomaChatContext;

        public LeaveGameSessionNotificationHandler(
            IMessageQueueConnection messageQueueConnection,
            IDiplomaChatContext diplomaChatContext)
        {
            _leaveChatRoomPublisher = messageQueueConnection.CreatePublisher("LeaveChatRoomQueue");
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<Unit> Handle(LeaveChatRoomNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var user = await _diplomaChatContext.EntitySet<User>()
                    .Where(u => u.Id == request.UserId)
                    .Select(u => new { u.Id })
                    .TopOneAsync(cancellationToken);

                if (user != null)
                {
                    var message = new LeaveGameSessionNotification
                    {
                        UserId = request.UserId
                    };

                    _leaveChatRoomPublisher.PublishMessage(message);
                    _leaveChatRoomPublisher.Dispose();
                }
            }

            return Unit.Value;
        }
    }

    public class LeaveGameSessionNotification
    {
        public Guid UserId { get; set; }
    }
}