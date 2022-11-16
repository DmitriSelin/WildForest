using Microsoft.AspNetCore.Mvc.Infrastructure;
using WildForest.Api.Common.Errors;
using WildForest.Application;
using WildForest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddSingleton<ProblemDetailsFactory, WildForestProblemDetailsFactory>();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment()) 
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}