using Business.Abstracts;
using Business.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Business.DependencyResolvers
{
    public static class ServiceCollectionBusinessExtension
    {
        public static IServiceCollection AddBusinessServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
        {
            services
                .AddScoped<IDomainDal, EfDomainDal>()
                .AddScoped<IDomainService, DomainManager>()

                .AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDbContext<ProjectContext>(
                 options => options.UseSqlServer(configuration.GetConnectionString("DomainProject")));

            
            return services;
        }
    }
}
