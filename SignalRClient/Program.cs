using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    public class Program
    {
        static HubConnection hubConnection;

        static async Task Main(string[] args)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44327/notificationHub")
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (username, msg) => Console.WriteLine($"{username}: {msg}"));

            await hubConnection.StartAsync();

            bool isExit = false;

            while(!isExit) {
                var msg = Console.ReadLine();
                if(msg != "exit")
                    await hubConnection.SendAsync("SendMessage", "Console", msg);
                else
                    isExit = true;
            }

            Console.ReadLine();
        }
    }
}