using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using DBGang.AspNetCore.DataProtection.PostgreSQL;

namespace Microsoft.AspNetCore.DataProtection
{
    public static class PostgreSQLDataProtectionExtensions
    {
        public static IDataProtectionBuilder PersistKeysToPostgreSQL(this IDataProtectionBuilder builder, string connectionString)
        {
            builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
            {
                var loggerFactory = services.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
                return new ConfigureOptions<KeyManagementOptions>(options =>
                {
                    options.XmlRepository = new PostgreSQLXmlRepository(loggerFactory, connectionString);
                });
            });

            return builder;
        }
    }
}
