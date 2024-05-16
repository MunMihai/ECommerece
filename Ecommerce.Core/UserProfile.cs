using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core;

public class UserProfile
{
    [Key]
    public Guid UserId { get; set; }
}