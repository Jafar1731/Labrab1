using System.Net.Http.Headers;
using System.IO;
namespace ex
{

    public class MyClass
    {
        public int MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }

        public MyClass(int value1, string value2)
        {
            MyProperty1 = value1;
            MyProperty2 = value2;
        }

        // Метод для создания копии класса
        public MyClass Copy()
        {
            return new MyClass(MyProperty1, MyProperty2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass original = new MyClass(42, "Hello");
            Console.WriteLine($"Original: MyProperty1={original.MyProperty1}, MyProperty2={original.MyProperty2}");

            MyClass copy = original.Copy();
            Console.WriteLine($"Copy: MyProperty1={copy.MyProperty1}, MyProperty2={copy.MyProperty2}");
        }
    }


    class Program1
    {
        //static void Main(string[] args)
        //{
        //    using (FileStream stream = new FileStream("test.txt", FileMode.OpenOrCreate)) ;
        //    {

        //    }

        //}


     
        static void ex1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("20 ");
            }
        }

        static void ex2()
        {
            var random = new Random();
            var i = 3;
            random.Next(i);
            for (i = 0; i < 10; i++)
            {
                Console.Write($" {random.Next(i)} ");
            }
        }
        static void ex3()
        {
            Console.Write("введите число больше 10: ");
            int b = Convert.ToInt32(Console.ReadLine());
            for (int i = 10; i < b; i++)
            {
                Console.Write($" {i * i} ");
            }
        }
        static void ex4()
        {

            Console.Write("введите число меньше 50: ");
            int a = Convert.ToInt32(Console.ReadLine());
            for (int i = 50; i > a; i--)
            {
                Console.Write($" {i * i * i} ");
            }
        }
        static void ex5()
        {
            Console.Write("Введите a: "); 
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите b: ");
            int b = Convert.ToInt32(Console.ReadLine());
            for (int i = a; i < b; i++)
            {
                Console.Write($" {i} ");
            }
        }

        static void ex6()
        {
            double price = 59.99;
            for(int i = 1; i < 21; i++)
            {
                Console.WriteLine($" {i} шт = {i*price} ");
            }
        }

        static void ex7()
        {
            double funt = 0.453;
            for(int i = 1; i < 11; i++)
            {
                Console.WriteLine($" {i} funts = {i*funt} kg ");
            }
        }

        static void ex8()
        {
           
            for(double i = 2.0; i<2.8; i+=0.1)
            {
                Console.WriteLine(i);
            }
        }

        
        
    }
 
}