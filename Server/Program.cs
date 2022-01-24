using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazored.Modal;
using FluentValidation.AspNetCore;
using MealOrdering.Business.DependencyResolvers.AutoFac;
using MealOrdering.Business.ModelMapping.AutoMapper;
using MealOrdering.Server.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;


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

builder.Services.AddAutoMapper(option => option.AddProfile<MappingProfile>());

builder.Services.AddDbContext<MealOrderingDbContext>(config =>
{
    config.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConn"));
    config.EnableSensitiveDataLogging();
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

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();