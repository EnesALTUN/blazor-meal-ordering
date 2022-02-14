using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazored.Modal;
using FluentValidation.AspNetCore;
using MealOrdering.Business.DependencyResolvers.AutoFac;
using MealOrdering.Business.ModelMapping.AutoMapper;
using MealOrdering.Server.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MealOrdering.Core.Extensions;
using MealOrdering.Core.Entities.Configuration;

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

var _jwtSetting = builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>();

builder.Services.AddJwtAuthentication(_jwtSetting);

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