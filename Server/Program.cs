using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazored.Modal;
using FluentValidation.AspNetCore;
using MealOrdering.Business.DependencyResolvers.AutoFac;
using MealOrdering.Business.ModelMapping.AutoMapper;
using MealOrdering.Server.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MealOrdering.Core.Entities.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(p => p.RegisterModule(new AutofacBusinessModule()));

builder.Services.AddControllersWithViews()
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Program>();
            });
builder.Services.AddRazorPages();
builder.Services.AddBlazoredModal();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MealOrderingAPI", Version = "v1" });
});

builder.Services.AddAutoMapper(option => option.AddProfile<MappingProfile>());

builder.Services.AddDbContext<MealOrderingDbContext>(config =>
{
    config.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConn"));
    config.EnableSensitiveDataLogging();
});

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));

builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JwtSetting:Audience"],
        ValidIssuer = builder.Configuration["JwtSetting:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSetting:SecurityKey"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "MealOrderingAPI v1"));

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();