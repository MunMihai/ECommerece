using Ecommerce.Core.Entities;

namespace Ecommerce.BL.Services;

public interface IJwtProvider
{
    string GenerateToken(User user);
}

