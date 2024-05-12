using FTsR.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FTsR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AuthDbContext _dbContext;
        public static Dictionary<string, string> UsernameConnectionId = new();

        public ChatHub(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetConnectionId(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (UsernameConnectionId.ContainsKey(username))
            {
                UsernameConnectionId[username] = Context.ConnectionId;
            }
            else
            {
                UsernameConnectionId.Add(username, Context.ConnectionId);
            }

            return Context.ConnectionId;
        }
    }
}
