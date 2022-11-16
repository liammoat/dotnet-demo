if (args.Length <= 0)
{
    Console.WriteLine("Expected argument <name>");
    return;
}

string name = args[0];
string greeting = GetGreeting(name, "Good afternoon");

Console.WriteLine(greeting);