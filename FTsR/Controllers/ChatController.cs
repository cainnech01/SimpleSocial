using FTsR.Data;
using FTsR.Hubs;
using FTsR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FTsR.Controllers
{
    public class ChatController : Controller
    {
        private readonly AuthDbContext _authDbContext;
        private readonly MessagesDbContext _messagesDbContext;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<UserModel> _userManager;

        public ChatController(MessagesDbContext messagesDbContext, IHubContext<ChatHub> hubContext, 
            UserManager<UserModel> userManager, AuthDbContext authDbContext)
        {
            _messagesDbContext = messagesDbContext;
            _hubContext = hubContext;
            _userManager = userManager;
            _authDbContext = authDbContext;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var allMessages = await _messagesDbContext.Messages.Where(x =>
                x.FromUserId == user.Id ||
                x.ToUserId == user.Id)
                .ToListAsync();

            var chats = new List<ChatModel>();
            foreach (var i in await _userManager.Users.ToListAsync())
            {
                if (i == user) continue;

                var chat = new ChatModel()
                {
                    MyMessages = allMessages.Where(x => x.FromUserId == user.Id && x.ToUserId == i.Id).ToList(),
                    OtherMessages = allMessages.Where(x => x.FromUserId == i.Id && x.ToUserId == user.Id).ToList(),
                    RecipientName = i.UserName
                };

                var chatMessages = new List<MessageModel>();
                chatMessages.AddRange(chat.MyMessages);
                chatMessages.AddRange(chat.OtherMessages);

                chat.LastMessage = chatMessages.OrderByDescending(x => x.Timestamp).FirstOrDefault();

                chats.Add(chat);
            }

            return View(chats);
        }

        public async Task<IActionResult> SendMessage(string to, string text)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(500);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var recipient = await _authDbContext.Users.SingleOrDefaultAsync(x => x.UserName == to);

            MessageModel message = new MessageModel()
            {
                FromUserId = user.Id,
                ToUserId = recipient.Id,
                Text = text,
                Timestamp = DateTime.Now,
            };

            await _messagesDbContext.AddAsync(message);
            await _messagesDbContext.SaveChangesAsync();

            string connectionId = ChatHub.UsernameConnectionId[recipient.UserName];

            await _hubContext.Clients.Client(connectionId).SendAsync("RecieveMessage", message.Text, message.Timestamp.ToShortTimeString());

            return Ok();
        }
    }
}
