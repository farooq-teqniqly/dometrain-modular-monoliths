﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{
    public static IServiceCollection AddUserServices(
        this IServiceCollection services,
        ConfigurationManager configuration, 
        ILogger logger, 
        List<Assembly> mediatRAssemblies)
    {
        var connectionString = configuration.GetConnectionString("UsersConnectionString");
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
        });

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UserDbContext>();

        services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();

        mediatRAssemblies.Add(typeof(UsersModuleExtensions).Assembly);

        logger.Information("{Module} module services registered.", "Users");

        return services;
    }
}
