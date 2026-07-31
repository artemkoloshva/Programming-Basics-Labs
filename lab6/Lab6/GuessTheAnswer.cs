using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    static internal class GuessTheAnswer
    {
        static internal double CalculationFunction(double a, double b)
        {
            double result = Math.Round(Math.PI * (Math.Log(Math.Pow(b, 5)) / (Math.Sin(a) + 1)), 2);

            return result;
        }
    }
}
