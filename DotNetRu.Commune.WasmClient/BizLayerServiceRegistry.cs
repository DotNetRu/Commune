using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

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
        [return:NotNull] public static IServiceCollection AddBizLogic([NotNull]this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            // здесь регистрируются службы слоя бизнес-логики
            return services;
        }
    }
}
