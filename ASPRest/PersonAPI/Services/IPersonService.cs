using PersonAPI.Models;

namespace PersonAPI.Services;

// Like PersonService interface in Spring Boot
public interface IPersonService
{
    Task<Person> CreatePersonAsync(Person person);
    Task<List<Person>> GetAllPersonsAsync();
    Task<Person?> GetPersonByIdAsync(int id);
    Task<List<Person>> GetPersonsByNameAsync(string name);
    Task<Person?> UpdatePersonAsync(int id, Person person);
    Task<bool> DeletePersonAsync(int id);
}
