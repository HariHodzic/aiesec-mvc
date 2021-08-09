using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aiesec.Data.Context;
using Aiesec.Data.Model.BusinessModel;
using Aiesec.Web.Helper;
using Aiesec.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Aiesec.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly AiesecDbContext _context;
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatController(AiesecDbContext context, IHubContext<ChatHub> chatHub)
        {
            _context = context;
            _chatHub = chatHub;
        }
        
        public async Task<IActionResult> Index()
        {
            var chats = await _context.Chats
                .Include(x => x.Users)
                .Where(x => x.Users.All(y => y.UserId != User.GetUserId()))
                .ToListAsync();
            return View(chats);
        }


        public IActionResult Chat([FromRoute] int id)
        {
            var chat = _context.Chats.Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            chat.Users.Add(new ChatUser
            {
                UserId = int.Parse(userIdClaim),
                Role = UserRole.Admin
            });

            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string text)
        {
            var message = new Message
            {
                ChatId = chatId,
                Text = text,
                Name = User.Identity?.Name
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Chat), new {id = chatId});
        }

        public async Task<IActionResult> JoinRoom(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.GetUserId(),
                Role = UserRole.Member
            };

            await _context.ChatUsers.AddAsync(chatUser);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Chat), new {id});
        }

        [HttpPost("[controller]/[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomId)
        {
            Console.WriteLine();
            await _chatHub.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName)
        {
            await _chatHub.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        // public async Task<IActionResult> SendMessage(string message, int chatId, string roomName)
        // {
        //     var newMessage = new Message
        //     {
        //         ChatId = chatId,
        //         Text = message,
        //         Name = User.Identity?.Name,
        //         Timestamp = DateTime.Now
        //     };
        //
        //     await _context.Messages.AddAsync(newMessage);
        //     await _context.SaveChangesAsync();
        //     await _chatHub.Clients.Group(roomName).SendAsync("ReceiveMessage", message);
        //     return Ok();
        // }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> SendMessage(string message, int roomId)
        {
            var newMessage = new Message
            {
                ChatId = roomId,
                Text = message,
                Name = User.Identity?.Name,
                Timestamp = DateTime.Now
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();

            await _chatHub.Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", new
            {
                Text = newMessage.Text,
                Name = newMessage.Name,
                Timestamp = newMessage.Timestamp.ToString("hh:mm")
            });

            return Ok();
        }

        public async Task<IActionResult> Find()
        {
            var users = await _context.Users
                .Where(x => x.Id != User.GetUserId())
                .ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> CreatePrivateRoom(int userId)
        {
            var chat = new Chat
            {
                Type = ChatType.Private
            };

            chat.Users.Add(new ChatUser
            {
                UserId = userId
            });

            chat.Users.Add(new ChatUser
            {
                UserId = User.GetUserId()
            });

            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Chat), new {id = chat.Id});
        }

        public async Task<IActionResult> Private()
        {
            var chats = await _context.Chats
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Where(x => x.Type == ChatType.Private &&
                            x.Users.Any(y => y.UserId == User.GetUserId()))
                .ToListAsync();
            return View(chats);
        }
    }
}