using WildForest.Api;
using WildForest.Api.SignalR.Hubs;
using WildForest.Application;
using WildForest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapForwarder("/{**all}", "http://localhost:5173/");
    app.UseExceptionHandler("/error");
    app.MapHub<ChatHub>("/chatHub");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}