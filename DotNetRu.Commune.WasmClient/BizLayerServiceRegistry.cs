using System;
using System.Diagnostics.CodeAnalysis;
using DotNetRu.Commune.GithubFileSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetRu.Commune.WasmClient
{
    /// <summary>
    /// Класс регистрации сервисов бизнес-логики
    /// </summary>
    internal static class BizLayerServiceRegistry
    {
        /// <summary>
        /// Регистрация служб бизнеслогики в контейнере
        /// </summary>
        /// <param name="services">коллекция служб</param>
        /// <returns>она же для соединения в цепочку</returns>
        /// <exception cref="ArgumentNullException">если переданная коллекция была null</exception>
        public static IServiceCollection AddBizLogic(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            // здесь регистрируются службы слоя бизнес-логики
            services.TryAddSingleton<ClientFactory>();
            return services;
        }
    }
}
