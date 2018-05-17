using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Core.Shared
{
    public interface IEventBus
    {
        void Publish<T>(T @event);
    }
}
