namespace OOP;

// ---------------- BASIC CLASS DEFINITION ----------------
// Class naming: PascalCase (same as Java)
// Access modifiers: public, private, protected, internal (similar to Java's package-private)
public class Person
{
    // Fields (instance variables) - naming convention: _camelCase with underscore for private fields
    // Java uses camelCase without underscore
    private string _name;
    private int _age;

    // Constructor - same as Java (class name, no return type)
    // C# supports constructor chaining with : this() like Java's this()
    public Person(string name, int age)
    {
        _name = name;
        _age = age;
    }

    // Method naming: PascalCase (Java uses camelCase)
    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {_name}, Age: {_age}");
    }

    // Getters/Setters - traditional Java-style (but C# has better way with Properties)
    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }
}


// ---------------- PROPERTIES (Modern C# way - better than Java getters/setters) ----------------
public class Employee
{
    // Auto-properties - C# syntactic sugar (no equivalent in Java)
    // Compiler automatically creates backing field
    public string Name { get; set; }  // Like: private String name; + getter + setter in Java [auto implemented property]
    // internally it creates: following is the way it was done before auto-properties
    // private string _name;
    // public String Name {
    //     get { return _name; }
    //     set { _name = value; }
    // }


    public int Age { get; set; }
    public decimal Salary { get; set; }  // decimal is better for money than double

    // Property with validation (like custom getter/setter)
    private string _email;
    public string Email
    {
        get { return _email; }
        set
        {
            if (value.Contains("@"))
            {
                _email = value;
            }
            else
            {
                throw new ArgumentException("Invalid email format");
            }
        }
    }

    // Read-only property (only getter, no setter) - like final field with getter in Java
    public string EmployeeId { get; }

    // Computed property (calculated value, no backing field)
    public string FullInfo => $"{Name} - {Age} years old - ${Salary}";  // Expression-bodied member

    // Constructor with property initialization
    public Employee(string name, int age, decimal salary, string email)
    {
        Name = name;
        Age = age;
        Salary = salary;
        Email = email;
        EmployeeId = Guid.NewGuid().ToString().Substring(0, 8);  // Generate unique ID
    }

    // Method overloading (same as Java)
    public void GiveRaise(decimal amount)
    {
        Salary += amount;
    }

    public void GiveRaise(double percentage)
    {
        Salary += Salary * (decimal)percentage / 100;
    }
}


// ---------------- INHERITANCE ----------------
// C# uses : for inheritance (Java uses extends)
// C# uses : for interfaces too (Java uses implements)
public class Manager : Employee // this class extends Employee , if Employee was interface, : would mean implements
{
    public string Department { get; set; }
    public List<string> TeamMembers { get; set; }

    // Constructor with base class initialization (: base() is like super() in Java)
    // if Parent class has no-arg constructor, then
    // using : base() is mandatory? answer: no, it is optional, like 
    // we do not need to call super() in java if parent class has no-arg constructor
    // But if parent class has parameterized constructor and no default constructor, then
    // using : base(params) is mandatory, like java
    public Manager(string name, int age, decimal salary, string email, string department)
        : base(name, age, salary, email)
    {
        Department = department;
        TeamMembers = new List<string>();
    }

    // Method can be virtual in base class and override in derived class
    // Unlike Java where all methods are virtual by default, C# requires explicit 'virtual' keyword
    public void AddTeamMember(string memberName)
    {
        TeamMembers.Add(memberName);
        Console.WriteLine($"{memberName} added to {Name}'s team");
    }

    public void DisplayTeam()
    {
        Console.WriteLine($"Manager: {Name}, Department: {Department}");
        Console.WriteLine($"Team Members ({TeamMembers.Count}):");
        foreach (var member in TeamMembers)
        {
            Console.WriteLine($"  - {member}");
        }
    }

}


// ---------------- METHOD MODIFIERS: virtual vs sealed vs non-virtual ----------------
// Unlike Java where all methods are virtual by default, C# requires explicit keywords
// Base Class
public class Animal
{
    // Non-virtual method (like final method in Java) - CANNOT be overridden
    public void NonVirtualMethod()
    {
        Console.WriteLine("I'm a non-virtual method from Animal - cannot be overridden");
    }

    // Virtual method - CAN be overridden in derived classes
    public virtual void VirtualMethod()
    {
        Console.WriteLine("I'm a virtual method from Animal - can be overridden");
    }

    // Virtual method that will be sealed in derived class
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal makes a sound");
    }
}

// Derived Class
public class Dog : Animal
{
    // ❌ Cannot override non-virtual method
    // public override void NonVirtualMethod() { ... } // Compile Error!

    // ✅ Can override virtual method
    public override void VirtualMethod()
    {
        Console.WriteLine("I'm a virtual method from Dog - still can be overridden by Bulldog");
    }

    // sealed override: Overrides the method BUT prevents further overriding
    // "I'm overriding this once, but my children cannot override it"
    public sealed override void MakeSound()
    {
        Console.WriteLine("Dog barks: Woof! (sealed - Bulldog cannot override this)");
    }
}

// Child class of Dog
public class Bulldog : Dog
{
    // ✅ Can override VirtualMethod (still virtual in Dog)
    public override void VirtualMethod()
    {
        Console.WriteLine("I'm a virtual method from Bulldog");
    }

    // ❌ Cannot override MakeSound (sealed in Dog class)
    // public override void MakeSound() { ... } // Compile Error!
}

// Summary:
// non-virtual: Cannot be overridden at all (like Java final method)
// virtual: Can be overridden in derived classes
// sealed override: Overrides once, then locks it (prevents further overriding)


// ---------------- ABSTRACT CLASS ----------------
// Abstract classes work similarly to Java
public abstract class Shape
{
    public string Color { get; set; }

    public Shape(string color)
    {
        Color = color;
    }

    // Abstract method (must be overridden) - like Java abstract method
    public abstract double CalculateArea();

    // Concrete method (can be used as-is or overridden)
    public virtual void Display() // virtual means can be overridden in derived classes, if you do not
    //  want it to be overridden, use sealed keyword. 
    // if you do not write virtual or sealed, method is non-virtual and cannot be overridden. means it is final in java
    // so to override it in derived class, it must be virtual here
    // can you not write either virtual or sealed? answer: yes. Then it behaves like sealed? answer: no, it behaves like non-virtual
    // sealed vs non-virtual: sealed means it was virtual but now cannot be overridden further, non-virtual means it was never virtual
    {
        Console.WriteLine($"This is a {Color} shape with area: {CalculateArea():F2}");
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(string color, double radius) : base(color)
    {
        Radius = radius;
    }

    // override keyword is mandatory in C# (unlike Java where it's @Override annotation - optional)
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(string color, double width, double height) : base(color)
    {
        Width = width;
        Height = height;
    }

    public override double CalculateArea()
    {
        return Width * Height;
    }

    // Override the virtual method from base class
    public override void Display()
    {
        Console.WriteLine($"Rectangle: {Width}x{Height}, Color: {Color}, Area: {CalculateArea():F2}");
    }
}


// ---------------- INTERFACE ----------------
// Interfaces in C# are similar to Java
// C# 8+ supports default interface methods (like Java 8+)
public interface IPayable
{
    // Interface members are public by default (like Java)
    decimal CalculatePay();

    // Default implementation (C# 8+, like Java 8+)
    void ProcessPayment()
    {
        Console.WriteLine($"Processing payment of ${CalculatePay()}");
    }
}

// A class can inherit one class but implement multiple interfaces
// Use : for both (Java uses extends and implements separately)
// if interface does not have any default implementation, class must implement all methods like java? answer: yes
public class Contractor : IPayable
{
    public string Name { get; set; }
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public Contractor(string name, decimal hourlyRate, int hoursWorked)
    {
        Name = name; // Name can mean _name, can mean getter, can mean setter, is this statement correct? answer: yes, it calls the set accessor of the property Name
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    public decimal CalculatePay()
    {
        return HourlyRate * HoursWorked;
    }
}


// ---------------- STATIC CLASS (C# specific - no direct Java equivalent) ----------------
// Static class: cannot be instantiated, can only contain static members
// Java uses utility classes with private constructor instead
public static class MathUtility
{
    // Static property
    public static double Pi => Math.PI;

    // Static methods
    public static int Square(int x) => x * x;

    public static double Average(params double[] numbers)
    {  // params = varargs in Java
        return numbers.Length > 0 ? numbers.Average() : 0;
    }
}


// ---------------- SEALED CLASS (like final class in Java) ----------------
// Prevents inheritance
public sealed class Configuration
{
    public string AppName { get; set; } = "MyApp";  // Property initialization
    public string Version { get; set; } = "1.0.0";

    // No class can inherit from Configuration (like Java final class)
}


// ---------------- RECORD (C# 9+ - no Java equivalent) ----------------
// Immutable data class with value-based equality
// Similar to Java Records (Java 14+) but more powerful
public record Customer(string Name, string Email, string Phone);

// Record with additional properties
public record Product(string Name, decimal Price)
{
    public string Category { get; init; } = "General";  // init = can only be set during initialization
    public int Stock { get; set; } = 0;  // Can be modified after creation
}


// ---------------- MAIN PROGRAM ----------------
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("============ C# OOP Concepts for Java Developers ============\n");

        // ---------------- Creating Objects ----------------
        // Object creation is same as Java (new keyword)

        Console.WriteLine("--- Basic Class ---");
        Person person1 = new Person("John Doe", 30);
        person1.DisplayInfo();

        // Modern C# - target-typed new (C# 9+) - infers type from left side
        Person person2 = new("Jane Smith", 25);
        person2.DisplayInfo();

        Console.WriteLine("\n--- Properties ---");
        Employee emp1 = new("Alice Johnson", 28, 75000m, "alice@company.com");
        Console.WriteLine($"Employee: {emp1.Name}, ID: {emp1.EmployeeId}");
        Console.WriteLine($"Full Info: {emp1.FullInfo}");

        emp1.GiveRaise(5000m);
        Console.WriteLine($"After raise: ${emp1.Salary}");

        emp1.GiveRaise(10);  // 10% raise
        Console.WriteLine($"After 10% raise: ${emp1.Salary}");

        Console.WriteLine("\n--- Inheritance ---");
        Manager mgr1 = new("Bob Wilson", 35, 95000m, "bob@company.com", "Engineering");
        mgr1.AddTeamMember("Alice Johnson");
        mgr1.AddTeamMember("Charlie Brown");
        mgr1.AddTeamMember("Diana Prince");
        mgr1.DisplayTeam();

        Console.WriteLine("\n--- Method Modifiers: virtual vs sealed vs non-virtual ---");
        Animal animal = new Animal();
        animal.NonVirtualMethod();  // Non-virtual method
        animal.VirtualMethod();     // Virtual method
        animal.MakeSound();         // Virtual method

        Dog dog = new Dog();
        dog.NonVirtualMethod();     // Inherited non-virtual (cannot override)
        dog.VirtualMethod();        // Overridden virtual (can still be overridden)
        dog.MakeSound();            // Sealed override (cannot be overridden further)

        Bulldog bulldog = new Bulldog();
        bulldog.NonVirtualMethod(); // Inherited non-virtual
        bulldog.VirtualMethod();    // Overridden again in Bulldog
        bulldog.MakeSound();        // Inherited sealed method from Dog

        Console.WriteLine("\n--- Abstract Classes & Polymorphism ---");
        // Polymorphism works same as Java
        List<Shape> shapes = new List<Shape> {
            new Circle("Red", 5),
            new Rectangle("Blue", 4, 6),
            new Circle("Green", 3)
        };

        foreach (var shape in shapes)
        {
            shape.Display();  // Calls appropriate override method
        }

        Console.WriteLine("\n--- Interfaces ---");
        IPayable contractor = new Contractor("Mike Davis", 50m, 160);
        contractor.ProcessPayment();

        Console.WriteLine("\n--- Static Class ---");
        Console.WriteLine($"Pi value: {MathUtility.Pi}");
        Console.WriteLine($"Square of 5: {MathUtility.Square(5)}");
        Console.WriteLine($"Average: {MathUtility.Average(10, 20, 30, 40, 50)}");

        Console.WriteLine("\n--- Records (Immutable Objects) ---");
        Customer customer1 = new("John Doe", "john@email.com", "555-1234");
        Customer customer2 = new("John Doe", "john@email.com", "555-1234");

        // Records have value-based equality (unlike classes with reference equality)
        Console.WriteLine($"customer1 == customer2: {customer1 == customer2}");  // True in records!

        // Record with 'with' expression - create copy with modified properties
        Customer customer3 = customer1 with { Phone = "555-9999" };
        Console.WriteLine($"Original: {customer1}");
        Console.WriteLine($"Modified copy: {customer3}");

        Console.WriteLine("\n--- Object Initializer (C# specific) ---");
        // Can set properties during object creation (similar to Java builder pattern)
        var product = new Product("Laptop", 999.99m)
        {
            Category = "Electronics",
            Stock = 50
        };
        Console.WriteLine($"Product: {product.Name}, Price: ${product.Price}, Category: {product.Category}, Stock: {product.Stock}");

        Console.WriteLine("\n--- Collection Initializer ---");
        // Initialize collections inline (similar to Java List.of(), but mutable)
        var employees = new List<Employee> {
            new("Alice", 28, 75000m, "alice@email.com"),
            new("Bob", 35, 85000m, "bob@email.com"),
            new("Charlie", 30, 80000m, "charlie@email.com")
        };

        Console.WriteLine("Employees:");
        foreach (var emp in employees)
        {
            Console.WriteLine($"  - {emp.Name}: ${emp.Salary}");
        }
    }
}
