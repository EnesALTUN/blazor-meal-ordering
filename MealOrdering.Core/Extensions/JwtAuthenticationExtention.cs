using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MealOrdering.Core.Entities.Configuration;

namespace MealOrdering.Core.Extensions;

public static class JwtAuthenticationExtention
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection service, JwtSetting jwtSetting)
    {
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtSetting.Audience,
                        ValidIssuer = jwtSetting.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey))
                    };
                });


        return service;
    }
}