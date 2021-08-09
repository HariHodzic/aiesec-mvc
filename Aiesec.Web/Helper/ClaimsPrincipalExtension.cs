using System.Security.Claims;

namespace Aiesec.Web.Helper
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetUserId(this ClaimsPrincipal @this)
        {
            return int.Parse(@this.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}