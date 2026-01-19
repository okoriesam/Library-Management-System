using LibraryManagementSystems.Application;
using LibraryManagementSystems.Application.Services;
using LibraryManagementSystems.Domain.Entity;
using LibraryManagementSystems.Infrastructure.Helper;
using LibraryManagementSystems.Infrastructure.Persistence.Data;
using LibraryManagementSystems.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Infrastructure
{
    public static class InfrastrutureService
    {
        public static IServiceCollection AddInfrastrutureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<SecurityHelper>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService,  UserService>();

            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            ApplicationService.ApplicationLayerServices(services);
            return services;
        }
    }
}
