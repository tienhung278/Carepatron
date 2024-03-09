using api.Data;
using api.Repositories;
using api.Repositories.Contracts;
using api.Services;
using api.Services.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder => builder
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .Build());
    });

    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "Test"));
    services.AddAutoMapper(typeof(Program));
    services.AddScoped<IRepositoryManager, RepositoryManager>();
    services.AddScoped<IServiceManager, ServiceManager>();
}


var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors();
    app.MapControllers();
    app.Run();
}
