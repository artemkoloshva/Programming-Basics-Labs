using System;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите значение альфа: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Введите значение бетта: ");
            double b = double.Parse(Console.ReadLine());

            double f = Math.Round(Math.PI*(Math.Log(Math.Pow(b,5))/(Math.Sin(a)+1)),2);

            Console.WriteLine(f);
            Console.ReadKey();
        }
    }
}
