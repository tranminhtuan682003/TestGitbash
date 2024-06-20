using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private static List<User> users = new List<User>();

    [HttpPost("register")]
    public IActionResult Register(RegisterModel model)
    {
        // Check if user with email already exists
        if (users.Any(u => u.Email == model.Email))
        {
            return BadRequest("User with this email already exists");
        }

        // Create new user
        var user = new User { Email = model.Email, Password = model.Password };
        users.Add(user);

        return Ok("Registration successful");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginModel model)
    {
        // Check if user exists
        var user = users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
        if (user == null)
        {
            return BadRequest("Invalid email or password");
        }

        return Ok("Login successful");
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        return Ok(users);
    }
}

public class User
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
