using FTsR.Repo;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http.Features;
using FTsR.Models;

namespace FTsR.Hubs;
public class DashboardHub : Hub
{
    ProductRepo productRepo;
    CompaniesRepo companiesRepo;

    public DashboardHub(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        productRepo = new ProductRepo(connectionString);
        companiesRepo = new CompaniesRepo(connectionString);
    }

    public async Task SendProducts()
    {
        var products = productRepo.GetProducts();
        await Clients.All.SendAsync("ReceivedProducts", products);
    }
    public async Task SendCompanies()
    {
        var companies = companiesRepo.GetCompanies();
        await Clients.All.SendAsync("ReceivedCompanies", companies);
    }

    public async Task Say(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}

