using FTsR.Data;
using FTsR.Models;
using FTsR.Repo;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Logging;
using System;

namespace FTsR.Hubs;
public class DataHub : Hub
{
    PinRepository pinRepo;
    public DataHub(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        pinRepo = new PinRepository(connectionString);
    }

    public async Task SendPins()
    {
        var pins = pinRepo.GetPins();
        await Clients.All.SendAsync("ReceivedPins", pins);
    }

    //public async Task UpdateData(string name, string value, string price)
    //{
    //    //_context.Data.Add(new DataModel { Name = name, Value = value });
    //    _context.Pin.Add(new Companies { Name = name, Type = value, Branch = price });
    //    await _context.SaveChangesAsync();

    //    await Clients.All.SendAsync("ReceiveData", name, value, price);
    //}
}
