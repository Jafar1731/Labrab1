using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Dynamic;
using System.Xml.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Emit;

//программа для регистрации на рейс самолета

namespace lab2test
{


    public class BD
    {

        public string? login { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public string? sername { get; set; }
        public int age { get; set; }
        public int position { get; set; }
        public void CopyParameters()
        {
           // копирование параметров экземпляра класса
           position = this.position;
        }

        public void ChangeParameters()
        {
            // изменение параметров экземпляра класса
            if (position > 2)
            position = this.position - 2;
            Console.WriteLine($"\n\t\tОтсалось {position} посадочных мест");
        }

    }

    public struct Getprice
    {
        public int price { get; set; }

        public Getprice(int p)
        {
            price = p;
        }
    }

    class runclass
    {

        static int Price(Getprice getprice)
        {
            getprice.price += 4000;
            return getprice.price;
        }

        


        static void Main(string[] args)
        {
            while (true)
            { 

            string age;
            Getprice get = new Getprice(20500);

            BD bd = new BD();

            Console.WriteLine($"\n\t\tЭто консольная программа для регистрации на рейс самолета\n" +
            $"\t\t\t-----------------------------------------\n");

            Console.WriteLine("Введите количество пассажиров: "); bd.position = Convert.ToInt32(Console.ReadLine());
            bd.CopyParameters();bd.ChangeParameters();
            Console.WriteLine("Введите имя: ");  bd.name = Console.ReadLine();
            Console.WriteLine("Введите фамилию: "); bd.sername = Console.ReadLine();
            Console.WriteLine("Введите возраст: "); bd.age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите логин: "); bd.login = Console.ReadLine();
            Console.WriteLine("Введите пароль: "); bd.password = Console.ReadLine();

            if (bd.age <= 0 || bd.age >= 100)
            {
                Console.WriteLine("Укажите корректный формат возраста !");
            }
            else
            {

                    if (bd.age <= 12)

                    {
                        age = "детский";
                        Console.WriteLine($"\t\t Пассажир {bd.sername} {bd.name} зарегистрирован " +
                        $"на \n\t\t рейс S3252 самолета airbus320," +
                        $" количество пассажиров 1 - ({age}),\n\t\t" +
                        $" подробнее о рейcе можете посмотреть " +
                        $"на сайте s7.ru логин {bd.login} пароль {bd.password}," +
                        $"\n\t\t цена за билет составляет {get.price}р удачного палета !\n\n\n");
                    }
                    else
                    {
                        age = "взрослый";
                        var get1 = get with { price = Price(get) };
                        Console.WriteLine($"\t\t Пассажир {bd.sername} {bd.name} зарегистрирован " +
                       $"на \n\t\t рейс S3252 самолета airbus320," +
                       $" количество пассажиров 1 - ({age}),\n\t\t" +
                       $" подробнее о рейcе можете посмотреть " +
                       $"на сайте s7.ru логин {bd.login} пароль {bd.password}," +
                       $"\n\t\t цена за билет составляет {get1.price}р удачного полета !\n\n");

                    }
                }
            }
        }

    }



}




  

