using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using DotNetRu.MeetupManagement.Core.Shared;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class EFCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DraftTalkRepository>().AsImplementedInterfaces();
            builder.RegisterType<TalkTryoutRepository>().AsImplementedInterfaces();
        }
    }
}
