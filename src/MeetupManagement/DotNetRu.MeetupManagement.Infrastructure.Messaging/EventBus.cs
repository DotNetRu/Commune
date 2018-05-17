using System;
using DotNetRu.MeetupManagement.Core.Shared;

namespace DotNetRu.MeetupManagement.Infrastructure.Messaging
{
    public class EventBus : IEventBus
    {
        public void Publish<T>(T @event)
        {
            // some logic to publish message like _rabbit.Publish(@event);
            throw new NotImplementedException();
        }
    }
}
