using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Contracts;
using UserMicroservice.Infrastructure;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase {

    private readonly IUsersService _userService;
    
    public UsersController(IUsersService usersService) { 
        _userService = usersService;
    }

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] RegisterUserRequest request) {
        var userId = await _userService.Register(request.UserName, request.Email, request.Password, request.role);
        
        return Results.Ok(userId);
    }
    
    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] LoginUserRequest request) {
        var token = await _userService.Login(request.Email, request.Password);
        HttpContext.Response.Cookies.Append("suchatastycookie", token);
        
        return Results.Ok(token);
    }

    [HttpPost("logout")]
    public IResult Logout()
    {
        HttpContext.Response.Cookies.Delete("suchatastycookie");
        return Results.Ok();
    }
    [HttpGet("is-logged-in")]
    public bool IsLoggedIn()
    {
        var cookie = HttpContext.Request.Cookies["suchatastycookie"];
        if (cookie == null)
        {
            return false;
        }

        return true;
    }

    [HttpPost("check-requirenments")]
    public async Task<ActionResult> CheckUserPermissions([FromBody] PayloadRequest request)
    {
        var token = request.Token;
        var requirement = request.Requirement;

        var result = await _userService.CheckPermissions(token, requirement);

        if (result)
        {
            return Ok();
        }
        
        return Unauthorized();
    }
}