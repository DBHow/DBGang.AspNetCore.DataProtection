using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;

namespace DBGang.AspNetCore.DataProtection.PostgreSQL
{
    public class PostgreSQLXmlRepository : IXmlRepository
    {
        private readonly ILogger _logger;
        private readonly string _connectionString;

        public PostgreSQLXmlRepository(ILoggerFactory loggerFactory, string connectionString)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _logger = loggerFactory.CreateLogger<PostgreSQLXmlRepository>();
            _connectionString = connectionString;
        }

        public virtual IReadOnlyCollection<XElement> GetAllElements()
        {
            var list = DbStore.GetAll(_connectionString);
            _logger.LogInformation($"Succeeded to get '{list.Count}' item(s).");
            return list.Select(x => XElement.Parse(x.Value)).ToList().AsReadOnly();
        }

        public void StoreElement(XElement element, string name)
        {
            try
            {
                DbStore.Save(_connectionString, new DataProtectionKey
                {
                    Name = name,
                    Value = element.ToString(SaveOptions.DisableFormatting)
                });
                _logger.LogInformation($"Succeeded to save key '{name}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save key '{name}': {ex.Message}.");
                throw ex;
            }
        }
    }
}
