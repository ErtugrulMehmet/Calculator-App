using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_App
{
    public class StartUp
    {
        public IConfiguration _configuration { get; set; }
        public StartUp()
        {
            var basePath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;

            var builder = new ConfigurationBuilder().SetBasePath(basePath).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppContext>(options =>
               options.UseSqlServer(_configuration.GetConnectionString("DataBaseConnectionString")));

            services.AddSingleton<ITableMethod, TableMethod>();
            services.AddSingleton<IUserAction, UserAction>();
        }
       
    }
}
