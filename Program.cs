using System;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {    
            while (true)
            {
                Console.WriteLine("------[Меню]------\n" +
                              "1. Отгадай ответ\n" +
                              "2. Об авторе\n" +
                              "3. Выход");

                switch (Console.ReadLine())
                {
                    case "1":
                        byte trys = 3;

                        Console.WriteLine("f = pi * ( ln(b^5) / (sin(a) + 1) )");

                        double a = 0;
                        bool trueA = false;
                        while (!trueA)
                        {
                            Console.Write("Введите значение альфа (a): ");
                            trueA = double.TryParse(Console.ReadLine(), out a);
                        }

                        double b = 0;
                        bool trueB = false;
                        while (!trueB)
                        {
                            Console.Write("Введите значение бетта (b): ");
                            trueB = double.TryParse(Console.ReadLine(), out b);
                        }
                        
                        double f = Math.Round(Math.PI * (Math.Log(Math.Pow(b, 5)) / (Math.Sin(a) + 1)), 2);

                        bool falseAnswer = true;

                        while (falseAnswer)
                        {
                            double answer = 0;
                            bool trueAnswer = false;
                            while (!trueAnswer)
                            {
                                Console.Write("Введите ваш предполагаемый ответ: ");
                                trueAnswer = double.TryParse(Console.ReadLine(), out answer);
                            }

                            if (answer != f)
                            {
                                trys--;
                                Console.WriteLine($"Ответ неверный. У вас осталось {trys} попыток");
                            }
                            else
                            {
                                Console.WriteLine($"Ответ верный. Вы справились c {4 - trys}-й попытки");
                                falseAnswer = false;
                            }
                        }

                        break;
                    case "2": Console.WriteLine("Колошва Артём Витальевич - 6103-090301D"); break;
                    case "3":
                        Console.WriteLine("Для того чтобы выйти из программы нажмите клавишу [Д]\n" +
                                              "Чтобы вернуться назад нажмите [Н]");
                        bool flagBack = true;
                        while (flagBack)
                        {
                            switch (Console.ReadKey().Key)
                            {
                                case ConsoleKey.L: return;
                                case ConsoleKey.Y: flagBack = false; break;
                                default: Console.WriteLine("Ошибка. Неизвестная команда. Попробуйте снова");
                                    break;
                            }
                        }
                        break;

                    default: Console.WriteLine("Ошибка. Такой функции нет. Попробуйте снова");
                        break;
                }
            }
        }
    }
}
