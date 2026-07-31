using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
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
                        StartGame();
                        break;
                    case "2":
                        OutputAutorInfo();
                        break;
                    case "3":
                        SortArray();
                        break;
                    case "4":
                        isWork = ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Ошибка. Такой функции нет. Попробуйте снова");
                        break;
                }
            }
        }

        /// <summary>
        /// Ввод вещественной переменной с проверкой
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        static private double EnterNumberDouble(string text = null)
        {
            double number = 0;

            do
            {
                if (text != null)
                {
                    Console.Write(text);
                }
            }
            while (!double.TryParse(Console.ReadLine(), out number));

            return number;
        }

        /// <summary>
        /// Ввод целочисленной переменной с проверкой
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        static private int EnterNumberInt(string text = null)
        {
            int number = 0;

            do
            {
                if (text != null)
                {
                    Console.Write(text);
                }
            }
            while (!int.TryParse(Console.ReadLine(), out number));

            return number;
        }

        /// <summary>
        /// Выводит массив с целочисленными элементами на экран
        /// </summary>
        /// <param name="array"></param>
        static private void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write(" ");
                }
                Console.Write(array[i]);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Расчитывает функцию
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static private double CalculationFunction(double a, double b)
        {
            double result = Math.Round(Math.PI * (Math.Log(Math.Pow(b, 5)) / (Math.Sin(a) + 1)), 2);

            return result;
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
                              "......[4] Выход");

            function = Console.ReadLine();

            return function;
        }

        /// <summary>
        /// Игра "Отгадай ответ!"
        /// </summary>
        static private void StartGame()
        {
            Console.WriteLine("     pi * ln(b^5)\n" +
                              "f = -------------\n" +
                              "     sin(a) + 1");

            double a = EnterNumberDouble("Введите значение альфа (а): ");
            double b = EnterNumberDouble("Введите значение бетта (b): ");

            double f = CalculationFunction(a, b);

            if (CheckAnswerGame(f))
            {
                Console.WriteLine("!!!ВЫ ПОБЕДИЛИ!!!");
            }
            else
            {
                Console.WriteLine($"ВЫ ПРОИГРАЛИ((( Правильный ответ: {f}");
            }
        }

        /// <summary>
        /// Сверяет ответ пользователя с результатом функции
        /// </summary>
        /// <param name="TrueAnswer"></param>
        static private bool CheckAnswerGame(double trueAnswer)
        {
            bool isWin = false;

            double answer = 0;

            for (byte trys = 3; trys > 0; trys--)
            {
                answer = EnterNumberDouble("Ваш предполагаемый ответ: ");

                if (answer != trueAnswer)
                {
                    Console.WriteLine($"Ответ неверный. У вас осталось {trys - 1} попыток");
                }
                else
                {
                    Console.WriteLine($"Ответ верный. Вы справились c {4 - trys}-й попытки");
                    isWin = true;
                    trys = 0;
                }
            }

            return isWin;
        }

        /// <summary>
        /// Выводит информацию об авторе на экран
        /// </summary>
        static private void OutputAutorInfo()
        {
            Console.WriteLine("Колошва Артём Витальевич - 6103-090301D");
        }

        /// <summary>
        /// Сравнивает быстроту работы 2х массивов
        /// </summary>
        static private void SortArray()
        {
            Stopwatch timer = new Stopwatch();

            int length = GetLength();
            int[] array = new int[length];

            FillArray(array, 100);

            int[] cloneArray = CloneArray(array);          

            timer.Start();
            BabbleSort(array);
            timer.Stop();
            Console.WriteLine($"Затраченное время пузырька: {timer.Elapsed}");

            timer.Restart();
            InsertionSort(cloneArray);
            timer.Stop();
            Console.WriteLine($"Затраченное время вставок: {timer.Elapsed}");

            if (array.Length <= 10)
            {
                Console.Write("Изначальный массив: ");
                PrintArray(array);

                Console.Write("Массив, полученный сортировкой пузырьком: ");
                PrintArray(BabbleSort(array));
                
                Console.Write("Массив, полученный сортировкой вставками: ");
                PrintArray(InsertionSort(cloneArray));                             
            }
            else
            {
                Console.WriteLine("Массивы не могут быть выведены на экран, так как длина массива больше 10");
            }
        }

        /// <summary>
        /// Получает длину массива
        /// </summary>
        /// <returns></returns>
        static private int GetLength()
        {
            bool flag = true;
            int length = 0;

            while (flag)
            {
                length = EnterNumberInt("Введите длинну массива: ");

                if (length > 0)
                    flag = false;
                else
                    Console.WriteLine("Введенное значение должно быть больше 0");
            }
            return length;
        }

        /// <summary>
        /// Заполняет массив рандомными числами
        /// </summary>
        /// <param name="array"></param>
        /// <param name="maxElement"></param>
        static private void FillArray(int[] array, int maxElement)
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(maxElement);
            }
        }

        /// <summary>
        /// Клонирует массив
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        static private int[] CloneArray(int[] array)
        {
            int[] cloneArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                cloneArray[i] = array[i];
            }

            return cloneArray;
        }

        /// <summary>
        /// Пузырьковая сортировка массива
        /// </summary>
        /// <param name="Array"></param>
        /// <returns></returns>
        static private int[] BabbleSort(int[] sortArray)
        {
            int temp = 0;

            for (int i = 0; i < sortArray.Length; i++)
            {
                for (int j = 0; j < sortArray.Length - 1; j++)
                {
                    if (sortArray[j] > sortArray[j + 1])
                    {
                        temp = sortArray[j + 1];
                        sortArray[j + 1] = sortArray[j];
                        sortArray[j] = temp;
                    }
                }
            }

            return sortArray;
        }

        /// <summary>
        /// Сортировка массива вставками
        /// </summary>
        /// <param name="Array"></param>
        /// <returns></returns>
        static private int[] InsertionSort(int[] sortArray)
        {
            for (int i = 1; i < sortArray.Length; i++)
            {
                int key = sortArray[i];
                int j = i - 1;

                while (j >= 0 && sortArray[j] > key)
                {
                    sortArray[j + 1] = sortArray[j];
                    j--;
                }

                sortArray[j + 1] = key;
            }

            return sortArray;
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