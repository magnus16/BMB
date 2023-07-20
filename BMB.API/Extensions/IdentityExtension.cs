using System.Security.Claims;
using System.Security.Principal;

namespace BMB.API.Extensions
{
    public static class IdentityExtension
    {
        public static string GetUserId(this IIdentity identity)
        {
            IEnumerable<Claim> claims = ((ClaimsIdentity)identity).Claims;
            var userId = claims.Where(c => c.Type == "Id").SingleOrDefault();
            return userId.Value;
        }

    }
}
