using Microsoft.AspNetCore.Mvc;

namespace PersonAPI.Controllers;

// Like @RestController in Spring Boot
[ApiController]
[Route("api/[controller]")]  // Route: /api/hello (like @RequestMapping in Spring)
public class HelloController : ControllerBase
{
    // Like @GetMapping in Spring Boot
    [HttpGet]
    public IActionResult SayHello()
    {
        return Ok("Hello World from ASP.NET Core!");
    }

    // Like @GetMapping("/{name}") in Spring Boot
    [HttpGet("{name}")]
    public IActionResult SayHelloToName(string name)
    {
        return Ok($"Hello {name} from ASP.NET Core!");
    }
}
