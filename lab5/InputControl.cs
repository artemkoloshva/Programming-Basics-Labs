using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    static internal class InputControl
    {
        /// <summary>
        /// Ввод вещественной переменной с проверкой
        /// </summary>
        /// <param name="Text">Текст который будет выводиться пользователю перед вводом переменной</param>
        /// <returns>Возвращает валидную переменную типа double</returns>
        static public double EnterNumberDouble(string text = null)
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
        /// <param name="Text">Текст который будет выводиться пользователю перед вводом переменной</param>
        /// <returns>Возвращает валидную переменную типа int</returns>
        static public double EnterNumberInt(string text = null)
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
    }
}
