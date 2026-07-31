using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab6
{
    static internal class InputControl
    {
        /// <summary>
        /// Контролирует правильность ввода в TextBlock типа double
        /// </summary>
        static internal bool ControlDoubleTextBox(TextBox tb, string tbBackground)
        {
            SolidColorBrush scbRed = new SolidColorBrush(Colors.Red);
            SolidColorBrush scbTB = new SolidColorBrush((Color)ColorConverter.ConvertFromString(tbBackground));
            double number;
            bool isCheck = false;

            if (!double.TryParse(tb.Text, out number))
            {
                tb.Background = scbRed;
            }
            else
            {
                tb.Background = scbTB;
                isCheck = true;
            }

            return isCheck;
        }

        /// <summary>
        /// Контролирует правильность ввода в TextBlock типа byte
        /// </summary>
        static internal bool ControlByteTextBox(TextBox tb, string tbBackground)
        {
            SolidColorBrush scbRed = new SolidColorBrush(Colors.Red);
            SolidColorBrush scbTB = new SolidColorBrush((Color)ColorConverter.ConvertFromString(tbBackground));
            byte number;
            bool isCheck = false;

            if (!byte.TryParse(tb.Text, out number))
            {
                tb.Background = scbRed;
            }
            else
            {
                tb.Background = scbTB;
                isCheck = true;
            }

            return isCheck;
        }

        /// <summary>
        /// Контролирует правильность ввода в TextBlock типа int
        /// </summary>
        static internal bool ControlInt32TextBox(TextBox tb, string tbBackground)
        {
            SolidColorBrush scbRed = new SolidColorBrush(Colors.Red);
            SolidColorBrush scbTB = new SolidColorBrush((Color)ColorConverter.ConvertFromString(tbBackground));
            int number;
            bool isCheck = false;

            if (!int.TryParse(tb.Text, out number))
            {
                tb.Background = scbRed;
            }
            else
            {
                tb.Background = scbTB;
                isCheck = true;
            }

            return isCheck;
        }

        /// <summary>
        /// Проверка на ввод натуральных чисел
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="tbBackground"></param>
        /// <returns></returns>
        static internal bool ControlNaturalNumbersTextBox(TextBox tb, string tbBackground)
        {
            SolidColorBrush scbRed = new SolidColorBrush(Colors.Red);
            SolidColorBrush scbTB = new SolidColorBrush((Color)ColorConverter.ConvertFromString(tbBackground));
            uint number;
            bool isCheck = false;

            if (!uint.TryParse(tb.Text, out number) || number == 0)
            {
                tb.Background = scbRed;
            }
            else
            {
                tb.Background = scbTB;
                isCheck = true;
            }

            return isCheck;
        }
    }
}
