using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DpmWebsite
{
    public class DpmService
    {
        public DpmService(IDbConnection connection)
        {
            _connection = connection;
        }

        public IReadOnlyList<DbPlugin> ShowPlugins()
        {
            return _connection.Query<DbPlugin>("select plugin_name as Name, plugin_description as Description from information_schema.plugins;").ToList();
        }
        private readonly IDbConnection _connection;
    }
}
