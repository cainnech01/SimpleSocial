using FTsR.Data;
using FTsR.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Logging;
using System;

namespace FTsR.Hubs;
public class DataHub : Hub
{
    private readonly CompaniesDbContext _context;

    public DataHub(CompaniesDbContext context)
    {
        _context = context;
    }

    public async Task UpdateData(string name, string value, string price)
    {
        //_context.Data.Add(new DataModel { Name = name, Value = value });
        _context.Companies.Add(new Companies { Name = name, Type = value, Branch = price });
        await _context.SaveChangesAsync();

        await Clients.All.SendAsync("ReceiveData", name, value, price);
    }
}
