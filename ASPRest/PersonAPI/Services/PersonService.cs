using Microsoft.EntityFrameworkCore;
using PersonAPI.Data;
using PersonAPI.Models;
using PersonAPI.Exceptions;

namespace PersonAPI.Services;

// Like PersonServiceImpl in Spring Boot
public class PersonService : IPersonService
{
    private readonly ApplicationDbContext _context;

    public PersonService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        return await _context.Persons.ToListAsync();
    }

    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        return await _context.Persons.FindAsync(id);
    }

    public async Task<List<Person>> GetPersonsByNameAsync(string name)
    {
        return await _context.Persons
            .Where(p => p.Name == name)
            .ToListAsync();
    }

    public async Task<Person?> UpdatePersonAsync(int id, Person person)
    {
        var existingPerson = await _context.Persons.FindAsync(id);

        if (existingPerson == null)
        {
            return null;
        }

        // Check if email already exists for another person
        var emailExists = await _context.Persons
            .AnyAsync(p => p.Email == person.Email && p.Id != id);

        if (emailExists)
        {
            throw new DuplicateResourceException("Email already in use");
        }

        // Update fields
        existingPerson.Name = person.Name;
        existingPerson.Age = person.Age;
        existingPerson.Email = person.Email;
        existingPerson.Phone = person.Phone;
        existingPerson.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingPerson;
    }

    public async Task<bool> DeletePersonAsync(int id)
    {
        var person = await _context.Persons.FindAsync(id);

        if (person == null)
        {
            return false;
        }

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
        return true;
    }
}
