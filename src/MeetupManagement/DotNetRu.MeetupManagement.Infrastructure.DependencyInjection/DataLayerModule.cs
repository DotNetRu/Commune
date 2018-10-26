namespace DotNetRu.MeetupManagement.Infrastructure.DependencyInjection
{
    using Autofac;
    using DotNetRu.MeetupManagement.Infrastructure.EFCore;
    using Microsoft.EntityFrameworkCore;

    public class DataLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddDbContext<DataContext>((options, configuration) => options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));
            builder.RegisterType<TalkDraftRepository>().AsImplementedInterfaces();
            builder.RegisterType<TalkRehearsalRepository>().AsImplementedInterfaces();
        }
    }
}
