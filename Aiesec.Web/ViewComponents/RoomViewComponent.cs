using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aiesec.Data.Context;
using Aiesec.Data.Model.BusinessModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aiesec.Web.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly AiesecDbContext _aiesecDbContext;

        public RoomViewComponent(AiesecDbContext aiesecDbContext)
        {
            _aiesecDbContext = aiesecDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var chats = await _aiesecDbContext.ChatUsers
                .Include(x => x.Chat)
                .Where(x => x.UserId == userId && x.Chat.Type == ChatType.Room)
                .Select(x => x.Chat)
                .ToListAsync();

            return View(chats);
        }
    }
}