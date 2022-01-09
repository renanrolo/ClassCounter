# ClassCounter

This is a simple exemple of how to identify on your project excessive object creation (I'm using it with Entity Framework to see when a query execution create more objects than it should).

The idea is simple:
Create a class with a static propertie that will hold the quantity of classes created

```
public class Counter
{
    public static Dictionary<string, int> counter = new Dictionary<string, int>();
    public static Debouncer debouncer = new Debouncer(2000);

    public Counter()
    {
        var className = this.GetType().Name;
        if (counter.ContainsKey(className))
        {
            counter[className] = ++counter[className];
            return;
        }

        counter.Add(className, 1);

        debouncer.Debounce(InformClassCreation);
    }

    private void InformClassCreation()
    {
        lock (counter)
        {
            Console.WriteLine("...");
            while (counter.Keys.Any())
            {
                var key = counter.Keys.First();
                var value = counter[key];
                Console.WriteLine($"Counter: class '{key}' were created '{value}' times");
                counter.Remove(key);
            }
            Console.WriteLine("...");
        }
    }
}
```

Usage
```
public class Pokemon : Counter
{
    public string Name { get; set; }
}
```

I'm using a Debounce Pattern here to reduce the quantity of lines printed on console, If the Debounce isn't responding the way you want or it's too complicated, remove the Debounce:

```
public class Counter
{
    public static Dictionary<string, int> counter = new Dictionary<string, int>();
    public static Debouncer debouncer = new Debouncer(2000);

    public Counter()
    {
        var className = this.GetType().Name;
        if (counter.ContainsKey(className))
        {
            counter[className] = ++counter[className];
        }
        else
        {
            counter.Add(className, 1);
        }

        Console.WriteLine($"Counter: class '{className}' were created '{counter[className]}' times");
    }
}
```


Print:
![teste](/Documentation/CounterPrintExemple.png)
