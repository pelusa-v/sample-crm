using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace sample_crm.API;

public static class ApiDependencyInjection
{
    public static void AddJwt(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
    }
}
