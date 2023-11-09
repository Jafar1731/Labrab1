using System;

namespace Lab4
{
    class Program
    {
        public string? Plane { get; set; }
        public string? airport { get; set; }
        public string? Airport
        {
            get { return airport; }
            set { airport = value; }
        }
        
        private int flight;
        public int Flight
        {
            get { return flight; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Flight cannot be a negative value!");
                }
                else
                {
                    flight = value;
                }
            }
        }
        

        public bool Passengers
        {
            get { return Flight < 25; }
        }

        public string GetInfo()
        {
            string emptiness = "";

            emptiness += $"\tPlane: {Plane}\n";
            emptiness += $"\tAirport: {Airport}\n";
            emptiness += $"\tIs passengers < 25 ?: {Passengers}\n";
           // emptiness += $"\tFlight of the plane: {Flight}\n";

            Console.ForegroundColor = ConsoleColor.Green;

            return emptiness;
        }

        public string GetInfo(bool includeIntProperty)
        {
            string emptiness = GetInfo();

            if (includeIntProperty)
            {
                emptiness += $"\tFlight of the plane: {Flight} <= this choose with (true)\n";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            
            return emptiness;
        }
    }

    class Run
    {
        static void Main()
        {
            Program program = new Program();

            program.Plane = "AirBus 320";

            program.Flight = 5;

            program.Airport = "Sheremetevo";

            Console.WriteLine(program.GetInfo());

            Console.WriteLine("\n   - - - - - - - - - - - - - - - - - - - - - - -   \n");

            Console.WriteLine(program.GetInfo(false));

            Console.ReadKey();
        }
    }
}
