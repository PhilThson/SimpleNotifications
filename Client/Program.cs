using Microsoft.AspNetCore.SignalR.Client;

HubConnection _connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5101/notifyhub")
    .WithAutomaticReconnect()
    .Build();

_connection.On<string>("ReceiveNotification", (text) =>
{
    WriteInfo($"\t\tNotification received: {text}");
});

{
    Console.WriteLine("Backend Notification Client!\n");
    string choice;
    do
    {
        DisplayMenu();
        choice = Console.ReadLine() ?? "";
        switch (choice)
        {
            case "0":
                break;
            case "1":
                await StartConnection();
                break;
            case "2":
                await EndConnection();
                break;
            case "3":
                await SendNotification();
                break;
            default:
                Console.WriteLine("Unknown choice. Please choose option from menu.");
                break;
        };
        await Task.Delay(3000);
    } while (choice != "0");

    Console.WriteLine("Finished!");
}

static void DisplayMenu()
{
    Console.WriteLine("Choose action:");
    Console.WriteLine("\t0. End program");
    Console.WriteLine("\t1. Start connection to Hub");
    Console.WriteLine("\t2. Disconnect from Hub");
    Console.WriteLine("\t3. Send notification to user");
}

async Task StartConnection()
{
    if (_connection.State == HubConnectionState.Connected)
    {
        WriteWarning("The connection is already established.");
        return;
    }
    try 
    {
        await _connection.StartAsync();
        WriteInfo("Connection started.");
    }
    catch (Exception e)
    {
        WriteError("An error occured on hub connection.", e);
    }
}

async Task EndConnection()
{
    if (_connection.State != HubConnectionState.Connected) 
    {
        WriteWarning("The connection is not established.");
        return;
    }
    try 
    {
        await _connection.DisposeAsync();
        Console.WriteLine("Connection ended.");
    }
    catch (Exception e)
    {
        WriteError("An error occured on connection dispose.", e);
    }
}

async Task SendNotification()
{
    Console.Write("Enter connectionId: ");
    var connectionId = Console.ReadLine();
    Console.Write("Enter notification text: ");
    var text = Console.ReadLine();
    try
    {
        await _connection.InvokeAsync("SendNotification", connectionId, text);
        Console.WriteLine("Notification has been sent");
    }
    catch (Exception e)
    {                
        WriteError("An error occured while sending notification.", e);
    }
}

static void WriteInfo(string message)
{
    WriteWithColor(message, ConsoleColor.Cyan);
}

static void WriteWarning(string message)
{
    WriteWithColor(message, ConsoleColor.Yellow);
}

static void WriteError(string message, Exception? e)
{
    WriteWithColor(message, ConsoleColor.DarkRed);
    if (e is null) return;
    WriteWithColor(e.Message, ConsoleColor.DarkRed);
}

static void WriteWithColor(string message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ForegroundColor = ConsoleColor.White;
}