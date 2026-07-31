using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isWork = true;

            while (isWork)
            {
                switch (SelectionFunctionMenu())
                {
                    case "1":
                        GuessTheAnswer.StartGame();
                        break;
                    case "2":
                        OutputAutorInfo();
                        break;
                    case "3":
                        ComparingArraySorts cas = new ComparingArraySorts(15);
                        cas.CompareSorting();
                        ComparingArraySorts casWithOutArgs = new ComparingArraySorts();
                        casWithOutArgs.CompareSorting();
                        break;
                    case "4":
                        ConsoleTetris tetris = new ConsoleTetris();
                        tetris.StartGame();
                        break;
                    case "5":
                        isWork = ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Ошибка. Такой функции нет. Попробуйте снова");
                        break;
                }
            }
        }

        /// <summary>
        /// Пользователь выбирает какой функцией, предложенной в меню, хочет воспользоваться
        /// </summary>
        static private string SelectionFunctionMenu()
        {
            string function;

            Console.WriteLine("               M E N U\n" +
                              "......[1] Игра \"Отгадай ответ!\"\n" +
                              "......[2] Информация об авторе\n" +
                              "......[3] Сортировка массивов\n" +
                              "......[4] Игра \"TETRIS\"\n" +
                              "......[5] Выход");

            function = Console.ReadLine();

            return function;
        }

        /// <summary>
        /// Выводит информацию об авторе на экран
        /// </summary>
        static private void OutputAutorInfo()
        {
            Console.WriteLine("Колошва Артём Витальевич - 6103-090301D");
        }

        /// <summary>
        /// Закрывает работу программы
        /// </summary>
        /// <returns></returns>
        static private bool ExitProgram()
        {
            bool isExit = false;

            Console.WriteLine("Для того чтобы выйти из программы нажмите клавишу [Д]\n" +
                                              "Чтобы вернуться назад нажмите [Н]");
            bool flagBack = true;
            while (flagBack)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.L:
                        isExit = true;
                        flagBack = false;
                        break;
                    case ConsoleKey.Y:
                        flagBack = false;
                        break;
                    default:
                        Console.WriteLine("Ошибка. Неизвестная команда. Попробуйте снова");
                        break;
                }
            }

            return !isExit;
        }
    }
}