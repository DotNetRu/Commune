namespace DotNetRu.MeetupManagement.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;

    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddDbContext<TContext>(this ContainerBuilder builder, Action<DbContextOptionsBuilder, IConfiguration> optionsAction = null)
            where TContext : DbContext
        {
            if (optionsAction != null)
            {
                builder.Register<DbContextOptions<TContext>>(context => DbContextOptionsFactory<TContext>(optionsBuilder =>
                {
                    IConfiguration config = context.Resolve<IConfiguration>();
                    optionsAction(optionsBuilder, config);
                }));
            }
            else
            {
                builder.Register<DbContextOptions<TContext>>(context => DbContextOptionsFactory<TContext>(null));
            }

            builder.Register<DbContextOptions>(p => p.Resolve<DbContextOptions<TContext>>()).InstancePerLifetimeScope();
            builder.RegisterType<TContext>();

            return builder;
        }

        private static DbContextOptions<TContext> DbContextOptionsFactory<TContext>(Action<DbContextOptionsBuilder> optionsAction)
            where TContext : DbContext
        {
            var options = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
            if (optionsAction != null)
            {
                var builder = new DbContextOptionsBuilder<TContext>(options);
                optionsAction(builder);
                options = builder.Options;
            }

            return options;
        }
    }
}
