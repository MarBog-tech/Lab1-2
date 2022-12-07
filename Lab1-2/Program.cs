using ClassLibrary1;

namespace Lab1_2;

class Program
{
    public static void Main(string[] args)
    {
        var c = new DynamicArray<int>();

        c.CollectionChanged += (_, action) =>
            Console.WriteLine(action.Action);

        foreach (var i in Enumerable.Range(1, 10))
            c.AddLast(i);

        Console.WriteLine("Array after filling:");
        c.Print();
        
        Console.WriteLine($"Count is {c.Count}.");
        
        c.RemoveFirst();
        Console.WriteLine("Array after removing first value:");
        c.Print();
        
        c.RemoveLast();
        Console.WriteLine("Array after removing last value:");
        c.Print();
        
        c.AddFirst(99);
        Console.WriteLine("Array after adding first value:");
        c.Print();
        
        Console.Write("print only first value:");
        Console.WriteLine(c.First);
        
        Console.Write("print only last value:");
        Console.WriteLine(c.Last);

        Thread.Sleep(Timeout.Infinite);
    }
}

public static class EnumerableExtensions
{
    public static void Print<T>(this IEnumerable<T> enumerable) =>
        Console.WriteLine($"[{string.Join(", ", enumerable.Select(t => t?.ToString() ?? "null"))}]\n");
}