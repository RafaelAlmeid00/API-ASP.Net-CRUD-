using UserApi.Models;

public class UserFactory(UserContext context) : IUserFactory
{
    private readonly UserContext _context = context;

    public User CreateUser()
    {
        return new User(_context);
    }
}
