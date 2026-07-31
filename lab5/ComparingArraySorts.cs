using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab5
{
    internal class ComparingArraySorts
    {
        private int length;
        private int[] array;

        public ComparingArraySorts()
        {
            this.length = 10;
            this.array = new int[length];
        }

        public ComparingArraySorts(int lengthValue)
        {
            this.length = lengthValue;
            this.array = new int[length];
        }

        public int LengthArray
        {
            get { return this.length; }
        }

        public int[] Array
        {
            get { return this.array; }
        }

        /// <summary>
        /// Сравнивает быстроту работы 2х массивов
        /// </summary>
        public void CompareSorting()
        {
            Stopwatch timer = new Stopwatch();

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
        /// Выводит массив с целочисленными элементами на экран
        /// </summary>
        /// <param name="array"></param>
        public void PrintArray(int[] array)
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
        /// Заполняет массив рандомными числами
        /// </summary>
        /// <param name="array"></param>
        /// <param name="maxElement"></param>
        private void FillArray(int[] array, int maxElement)
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
        private int[] CloneArray(int[] array)
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
        private int[] BabbleSort(int[] sortArray)
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
        private int[] InsertionSort(int[] sortArray)
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
    }
}
