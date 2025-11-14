using Microsoft.AspNetCore.Mvc; // what this line is for? answer: it imports
//  the ASP.NET Core MVC functionalities needed for building web APIs.
// like to use ApiController, HttpGet, IActionResult, ControllerBase, etc.

namespace PersonAPI.Controllers;

[ApiController] // Like @RestController in Spring Boot
[Route("api/[controller]")]  // @RequestMapping("api/person/")
public class PersonController : ControllerBase // extending ControllerBase for API features
{
    [HttpGet("hello")] // Like @GetMapping("/hello") in Spring Boot
    public String GetHello()
    {
        return "Hello from Person Controller!";
    }
}
