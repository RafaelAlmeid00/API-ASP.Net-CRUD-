using Microsoft.AspNetCore.Mvc;
using UserApi.Models;

public interface IUser
{
    public long user_id { get; set; }
    public string? user_name { get; set; }
    public string user_username { get; set; }
    public string user_email { get; set; }
    public string user_password { get; set; }

    Task<ActionResult<IEnumerable<User>>> GetAllUsers();
    Task<ActionResult<User>> GetUser(long id);
    Task<IActionResult> PutUser(long id, User user);
    Task<IActionResult> DeleteUser(long id);
    Task<IActionResult> PostUser(User user);

}
