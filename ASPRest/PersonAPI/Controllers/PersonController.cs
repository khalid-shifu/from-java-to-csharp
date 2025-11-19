using Microsoft.AspNetCore.Mvc;
using PersonAPI.Models;
using PersonAPI.Services;

namespace PersonAPI.Controllers;

[ApiController] // Like @RestController in Spring Boot
[Route("api/[controller]")]  // @RequestMapping("api/person/")
public class PersonController : ControllerBase
{

    private readonly IPersonService _personService;

    // Constructor injection (Like @Autowired in Spring Boot)
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("hello")]
    public String GetHello()
    {
        return "Hello from Person Controller!";
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePerson([FromBody] Person person)
    {
        var createdPerson = await _personService.CreatePersonAsync(person);
        return Ok(new { message = "Person created", person = createdPerson });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPersons()
    {
        var persons = await _personService.GetAllPersonsAsync();
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonById(int id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetPersonByName(string name)
    {
        var persons = await _personService.GetPersonsByNameAsync(name);
        return Ok(persons);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person person)
    {
        var updatedPerson = await _personService.UpdatePersonAsync(id, person);
        if (updatedPerson == null)
        {
            return NotFound();
        }
        return Ok(updatedPerson);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var result = await _personService.DeletePersonAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok(new { message = "Person deleted" });
    }
}


