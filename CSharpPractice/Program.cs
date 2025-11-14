using System.ComponentModel;

namespace CSharpPractice;

// In C# file name doesn't need to match public class name (unlike Java)
// Namespace is similar to Java package, but not mandatory
// Java: com.example.app | C#: Com.Example.App (PascalCase)
public class Program {
    public static void Main(string[] args) {
        Console.WriteLine("Hello, World!");


        // ---------------- VARIABLES AND DATA TYPES ----------------
        // variable naming conventions similar to Java
        // variables are mostly same as Java instead of boolean (bool in C#)
        // C# string is in lowercase

        string name = "John";
        int age = 30;
        bool isStudent = false;
        long population = 7800000000L;
        double pi = 3.14159;
        char grade = 'A';

        //------------------ Wrapper Classes ----------------
        // C# has built-in methods for primitive types, no separate wrapper classes(Integer) like Java

        // ---------------- BASIC OPERATIONS ----------------

        int a = 10;
        int b = 3;

        // Arithmetic operators (same as Java)
        int sum = a + b;
        Console.WriteLine($"Sum: {sum}. Boom!");

        int difference = a - b;
        int product = a * b;
        int quotient = a / b;
        int remainder = a % b;

        // C# specific: Integer division vs float division
        double division = (double)a / b;  // 3.333... (explicit cast needed)
        Console.WriteLine($"Division result: {division:F5}"); // Format to 5 decimal places

        // Increment/Decrement (same as Java)
        a++;
        b--;

        // Compound assignments (same as Java)
        a += 5;
        b *= 2;

        // String operations
        string firstName = "John";
        string lastName = "Doe";

        // String concatenation (same as Java)
        string fullName = firstName + " " + lastName;

        // C# specific: String interpolation (better than Java)
        string greeting = $"Hello, {firstName}!";  // Java 21+ has similar with STR
        string message = $"{a} + {b} = {sum}";     // Embedded expressions

        // Comparison operators (same as Java)
        bool isEqual = (a == b);
        bool isGreater = (a > b);
        bool isNotEqual = (a != b);

        // Logical operators (same as Java)
        bool andResult = (a > 5 && b < 10);
        bool orResult = (a > 5 || b > 10);
        bool notResult = !(a > 5);

        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Division: {division:F2}");
        Console.WriteLine($"Greeting: {greeting}");

        Console.WriteLine($"Hello! Mr. {firstName}_{lastName}.");

        //-------------------- Conditional Statements (Control Flow)-----------------
        if (age < 18) {
            Console.WriteLine("Minor");
        } else if (age >= 18 && age < 65) {
            Console.WriteLine("Adult");
        } else {
            Console.WriteLine("Senior");
        }

        // Ternary operator (same as Java)
        string group = age < 18 ? "Minor" : (age < 65 ? "Adult" : "Senior");

        // Switch statement (similar to Java, but with pattern matching in newer versions)
        switch (grade) {
            case 'A':
                Console.WriteLine("Excellent");
                break;
            case 'B':
                Console.WriteLine("Good");
                break;
            case 'C':
                Console.WriteLine("Average");
                break;
            default:
                Console.WriteLine("Needs Improvement");
                break;
        }

        // modern swtich expression (C# 8.0+)
        string performance = grade switch {
            'A' => "Excellent",
            'B' => "Good",
            'C' => "Average",
            _ => "Needs Improvement" // default case
        };




        // ---------------- Loops ----------------

        // For loop (same as Java)
        for (int i = 0; i < 5; i++) {
            Console.WriteLine($"i : {i}.");
        }

        // While loop (same as Java)
        int count = 0;
        while (count < 5) {
            Console.WriteLine($"count : {count}.");
            count++;
        }

        int[] numbers = [1, 2, 3, 4, 5];

        // Foreach loop 
        foreach (int number in numbers) {
            Console.WriteLine($"number : {number}.");
        }


        //--------------------  Data Structures  ----------------
        //  Arrays | List | Dictionary | HashSet | Queue | Stack | LinkedList | SortedList | 
        // SortedDictionary | SortedSet | Tuple | ValueTuple | ObservableCollection | 
        // ReadOnlyCollection | ImmutableList | ImmutableDictionary | ImmutableHashSet |
        // ConcurrentDictionary | ConcurrentQueue | ConcurrentStack | Span | Memory


        // ------------------------Arrays (same as Java)-------------------------

        int[] arrNumber = new int[5];

        for (int i = 0; i < arrNumber.Length; i++) {  // .Length instead of .length
            arrNumber[i] = i * 10;
        }

        string[] fruits = { "Apple", "Banana", "Cherry" };

        for (int i = 0; i < fruits.Length; i++) {
            string print = i != fruits.Length - 1 ? $"{fruits[i]} | " : fruits[i];
            Console.Write($"{print}");
        }
        Console.WriteLine();

        // Java has Arrays class with utility methods, C# has Array class with similar methods
        Array.Sort(fruits);
        Array.Reverse(fruits);
        //Array.Clear(fruits, 0, fruits.Length); // Clear all elements

        Console.WriteLine(string.Join(", ", arrNumber));

        Array.Copy(arrNumber, 0, arrNumber, 1, arrNumber.Length - 1); // Shift elements right by 1 but 1st element remains same

        // print array in 1 line
        Console.WriteLine(string.Join(", ", arrNumber));

        // other array methods
        int index = Array.IndexOf(arrNumber, 20);
        bool contains = Array.Exists(arrNumber, element => element == 30);


        // ------------------------Lists (similar to Java ArrayList)------------------------

        List<string> listFruits = new List<string>();
        listFruits.Add("Mango");
        listFruits.Add("Orange");
        listFruits.AddRange(fruits); // Add multiple elements
        listFruits.Remove("Banana");

        int listIndex = listFruits.IndexOf("Cherry");
        Console.WriteLine(string.Join(" - ", listFruits));

        bool listContains = listFruits.Contains("Apple");
        listFruits.RemoveAt(0); // Remove first element
        Console.WriteLine($"After removing first element: {string.Join(" - ", listFruits)}");

        listFruits.Insert(1, "Pineapple"); // Add to the start
        Console.WriteLine($"After inserting Pineapple: {string.Join(" - ", listFruits)}");

        listFruits.Sort(); // Sort the list
        Console.WriteLine($"After sorting: {string.Join(" - ", listFruits)}");

        //listFruits.Clear(); // Remove all elements


        // ------------------------Dictionaries (similar to Java HashMap)------------------------
        Dictionary<string, int> dictAges = new Dictionary<string, int>();
        dictAges["Alice"] = 25;
        dictAges["Bob"] = 30;
        dictAges.Add("Charlie", 35);

        foreach (var kvp in dictAges) {
            Console.WriteLine($"{kvp.Key} is {kvp.Value} years old.");
        }

        Console.WriteLine($"Total number of entries: {dictAges.Count}");
        Console.WriteLine($"Age of Alice: {dictAges["Alice"]}");
        dictAges.Remove("Bob");

        // other important methods
        bool hasCharlie = dictAges.ContainsKey("Charlie");
        dictAges.TryGetValue("Alice", out int aliceAge); // out int intializes a varible if key exists



        // ------------------------ (Methods) ------------------------

        int sumFunction = Add(5, 10);
        Console.WriteLine($"Sum from Add function: {sumFunction}");


    }

    //--------------------------- methods --------------------------------

    // like java for function mandatory to specify return type, parameter types, access modifier
    // unlike java, method name starts with uppercase letter (PascalCase)
    public static int Add(int x, int y) {
        return x + y;
    }

}
