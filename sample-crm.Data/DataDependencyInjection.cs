﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sample_crm.Data.Interfaces;
using sample_crm.Data.Repositories;

namespace sample_crm.Data;

public static class DataDependencyInjection
{
    // Extension method
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddIdentityConfiguration();
        return services;
    }

    // Extension method
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFlowRepository, FlowRepository>();
        services.AddScoped<IFlowStateRepository, FlowStateRepository>();
    }

    // Extension method
    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseMySQL(configuration.GetConnectionString("Database")));
    }

    private static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
}
