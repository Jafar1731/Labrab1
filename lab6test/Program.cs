using System;

public class MyClass
{
    public string NameOfFlight { get; set; }
    public int NumberOfFlight { get; set; }
    public double PriceOfFlight { get; set; }
    public string? NameOfFlight1 { get; set; }
    public int? NumberOfFlight1 { get; set; }
    public double? PriceOfFlight1 { get; set; }
}

public class Program
{
    public static void DisplayInfo(MyClass myObject)
    {
        Console.WriteLine($"Your flight is: {myObject.NameOfFlight ?? "Data not available"}");
        Console.WriteLine($"Number of your flight is: {myObject.NumberOfFlight}");
        Console.WriteLine($"Price of your flight is: {myObject.PriceOfFlight}");
        Console.WriteLine($"Another flight is: {myObject.NameOfFlight1 ?? "Data not available"}");
        Console.WriteLine($"Another number of flight is: {myObject.NumberOfFlight1}");
        Console.WriteLine($"Another price of flight is: {myObject.PriceOfFlight1}");
    }
    
    public void AssignValueIfNeeded(ref string? nullableProperty, string value)
    {
        nullableProperty ??= value;
    }

    public string GetValueOrConstant(MyClass? myObject)
    {
        return myObject?.NameOfFlight ?? "DefaultConstantValue";
    }

    public string GetNullableValueOrConstant(MyClass myObject)
    {
        return myObject.NameOfFlight1 ?? "DefaultConstantValue";
    }

    public static void Main()
    {
        MyClass obj1 = new MyClass
        {
            NameOfFlight = "S7 airlines",
            NumberOfFlight = 11,
            PriceOfFlight = 2500,
            NameOfFlight1 = null,
            NumberOfFlight1 = 10,
            PriceOfFlight1 = null
        };

        MyClass obj2 = new MyClass
        {
            NameOfFlight = "Turkish airlines",
            NumberOfFlight = 12,
            PriceOfFlight = 3000,
            NameOfFlight1 = "American Express",
            NumberOfFlight1 = null,
            PriceOfFlight1 = 2000
        };

        DisplayInfo(obj1);
        DisplayInfo(obj2);

#pragma warning disable CS8602
        obj1.NameOfFlight = null;
#pragma warning restore CS8602
    }
}