using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;
using NotificationHub;
using NotificationHub.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<NotificationsRegistry>();
builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

builder.Services.AddAuthentication(Constants.MyAuthScheme)
    .AddScheme<AuthenticationSchemeOptions, MyAuthScheme>(Constants.MyAuthScheme, null);

builder.Services.AddSignalR();
builder.Services.AddCors(setup => 
{
    setup.AddPolicy(Constants.CORS_POLICY, policy =>
        policy
            .SetIsOriginAllowed(isOriginAllowed: _ => true)
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:9000"));
});

var app = builder.Build();

app.UseCors(Constants.CORS_POLICY);

app.UseAuthentication();

app.MapHub<NotifyHub>("/notifyhub");

app.MapGet("/notifications", async (context) =>
{
    var registry = app.Services.GetRequiredService<NotificationsRegistry>();
    await context.Response.WriteAsJsonAsync(registry.GetAllNotifications());
});

app.MapGet("/connectedUsers", async (context) =>
{
    var registry = app.Services.GetRequiredService<NotificationsRegistry>();
    await context.Response.WriteAsJsonAsync(registry.GetAllUsers());
});

app.Run();
