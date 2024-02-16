using Microsoft.AspNetCore.SignalR.Client;

HubConnection? _connection = null;
string? user = null;

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
            case "4":
                SetUser();
                break;
            default:
                Console.WriteLine("Unknown choice. Please choose option from menu.");
                break;
        };
        await Task.Delay(2500);
    } while (choice != "0");

    Console.WriteLine("Finished!");
}


void DisplayMenu()
{
    if (HasUser())
    {
        Console.WriteLine($"\n---Logged user: {user}---");
    }
    Console.WriteLine("Choose action:");
    Console.WriteLine("\t0. End program");
    Console.WriteLine("\t1. Start connection to Hub");
    Console.WriteLine("\t2. Disconnect from Hub");
    Console.WriteLine("\t3. Send notification to user");
    if (!HasUser())
    {
        Console.WriteLine("\t4. Log in");
    }
}

async Task StartConnection()
{
    if (!HasUser())
    {
        WriteWarning("Please log in.");
        return;
    }
    if (_connection?.State == HubConnectionState.Connected)
    {
        WriteWarning("The connection is already established.");
        return;
    }
    try 
    {
        _connection = new HubConnectionBuilder()
            .WithUrl($"http://localhost:5101/notifyhub?user_id={user}")
            .WithAutomaticReconnect()
            .Build();

        _connection.On<string>("ReceiveNotification", (text) =>
        {
            Console.WriteLine("\n***");
            WriteInfo($"\t\tNotification received: {text}");
            Console.WriteLine("***");
        });

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
    if (_connection?.State != HubConnectionState.Connected) 
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

void SetUser()
{
    string? userName = null;
    while (string.IsNullOrEmpty(userName))
    {
        Console.Write("Enter user name: ");
        userName = Console.ReadLine();
    }
    user = userName;
}

async Task SendNotification()
{
    if (_connection?.State != HubConnectionState.Connected)
    {
        WriteWarning("The connection is not established.");
        return;
    }
    Console.Write("Enter UserId: ");
    var userId = Console.ReadLine();
    Console.Write("Enter notification text: ");
    var text = Console.ReadLine();
    try
    {
        await _connection.InvokeAsync("SendNotification", userId, text);
        Console.WriteLine("Notification has been sent");
    }
    catch (Exception e)
    {
        WriteError("An error occured while sending notification.", e);
    }
}

bool HasUser() => !string.IsNullOrEmpty(user);

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
    Console.ResetColor();
}