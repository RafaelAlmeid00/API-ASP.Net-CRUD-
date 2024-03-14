using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserApi.Models;

public class User : IUser
{
    private readonly UserContext _context;

    [Key]
    public long user_id { get; set; }
    public string? user_name { get; set; }

    [Required]
    public string user_username { get; set; }

    [Required]
    public string user_email { get; set; }

    [Required]
    public string user_password { get; set; }

    [JsonConstructor]
    public User(UserContext context)
    {
        _context = context;
    }

    // Construtor padrão necessário para desserialização JSON
    public User() { }

    private bool UserExists(long id)
    {
        return _context.User.Any(e => e.user_id == id);
    }

    public async Task<ActionResult<User>> GetUser(long id)
    {
        return await _context.User.FindAsync(id);
    }

    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<IActionResult> PutUser(long id, User user)
    {
        if (id != user.user_id)
        {
            return new BadRequestResult();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return new NotFoundResult();
            }
            else
            {
                throw;
            }
        }

        return new NoContentResult();
    }

    public async Task<IActionResult> DeleteUser(long id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
        {
            return new NotFoundResult();
        }

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<IActionResult> PostUser(User user)
    {
        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return new CreatedAtActionResult(nameof(GetUser), "User", new { id = user.user_id }, user);
    }
}
