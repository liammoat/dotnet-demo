namespace DotNetDemo.Core;

public static class GreetingService
{
    public static string GetGreeting(string name, string greeting = "Hello")
    {
        return $"{greeting}, {name}";
    }
}
