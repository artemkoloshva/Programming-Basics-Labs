using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    static internal class GuessTheAnswer
    {
        /// <summary>
        /// Запускает игру
        /// </summary>
        static public void StartGame()
        {
            PrintFunction();
            double result = CalculationFunction
            (
                InputControl.EnterNumberDouble("Введите значение альфа (а): "), 
                InputControl.EnterNumberDouble("Введите значение бетта (b): ")
            );
            if (CheckAnswerGame(result))
                Console.WriteLine("!!!ВЫ ПОБЕДИЛИ!!!");
            else
                Console.WriteLine($"ВЫ ПРОИГРАЛИ((( Правильный ответ: {result}");
        }

        /// <summary>
        /// Выводит функцию в консоль
        /// </summary>
        static public void PrintFunction()
        {
            Console.WriteLine("     pi * ln(b^5)\n" +
                              "f = -------------\n" +
                              "     sin(a) + 1");
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
        /// Сверяет ответ пользователя с результатом функции
        /// </summary>
        /// <param name="TrueAnswer"></param>
        static private bool CheckAnswerGame(double trueAnswer)
        {
            bool isWin = false;

            double answer = 0;

            for (byte trys = 3; trys > 0; trys--)
            {
                answer = InputControl.EnterNumberDouble("Ваш предполагаемый ответ: ");

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
    }
}
