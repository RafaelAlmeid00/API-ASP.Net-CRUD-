using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Models;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; } = null!;
}