namespace Ecommerce.BL.Services;

public class PasswordHasher : IPasswordHasher
{
    public PasswordHasher()
    {
    }
    public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}
