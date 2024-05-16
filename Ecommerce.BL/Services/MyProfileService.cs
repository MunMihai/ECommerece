using Ecommerce.BL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ecommerce.BL.Services
{
    public class MyProfileService : IMyProfileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyProfileService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Guid GetUserId()
        {
            // Retrieve the current HTTP context from the HttpContextAccessor
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                throw new InvalidOperationException("HTTP context is not available.");
            }

            // Retrieve the user identifier from the current user claims
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                throw new InvalidOperationException("User identifier claim not found.");
            }

            // Convert the user identifier to a Guid
            if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                throw new InvalidOperationException("Invalid user identifier format.");
            }

            return userId;
        }
    }
}
