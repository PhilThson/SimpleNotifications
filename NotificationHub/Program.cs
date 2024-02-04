using NotificationHub;

const string CORS_POLICY = "BasePolicy";
;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddCors(setup => 
{
    setup.AddPolicy(CORS_POLICY, policy =>
        policy
            //.SetIsOriginAllowed(isOriginAllowed: _ => true)
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:9000"));
});

var app = builder.Build();

app.UseCors(CORS_POLICY);

app.MapHub<NotifyHub>("/notifyhub");

app.Run();
