using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using DotNetRu.MeetupManagement.Core.Drafts;

namespace DotNetRu.MeetupManagement.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DraftTalkService>().AsImplementedInterfaces();
            builder.RegisterType<TalkTryoutService>().AsImplementedInterfaces();
        }
    }
}
