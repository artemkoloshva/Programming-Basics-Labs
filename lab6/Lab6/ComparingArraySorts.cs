using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class ComparingArraySorts
    {
        private int length;
        private int[] array;
        private string timeBabbleSort;
        private string timeInsertionSort;
        public ComparingArraySorts()
        {
            this.length = 10;
            this.array = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            this.timeBabbleSort = TimeSpan.MinValue.ToString();
            this.timeInsertionSort = TimeSpan.MinValue.ToString();
        }

        public ComparingArraySorts(int length)
        {
            this.length = length;
            this.array = new int[this.length];
            this.timeBabbleSort = TimeSpan.MinValue.ToString();
            this.timeInsertionSort = TimeSpan.MinValue.ToString();
        }

        public ComparingArraySorts(int[] array)
        {
            this.array = array;
            this.length = array.Length;
            this.timeBabbleSort = TimeSpan.MinValue.ToString();
            this.timeInsertionSort = TimeSpan.MinValue.ToString();
        }

        public int LengthArray
        {
            get { return this.length; }
        }

        public int[] Array
        {
            get { return this.array; }
        }

        public string TimeBabbleSort
        {
            get { return this.timeBabbleSort; }
        }

        public string TimeInsertionSort
        {
            get { return this.timeInsertionSort; }
        }

        /// <summary>
        /// Записывает быстроту работы 2х массивов
        /// </summary>
        public void CompareSorting()
        {
            Stopwatch timer = new Stopwatch();

            int[] cloneArray = CloneArray(array);

            timer.Start();
            BabbleSort(array);
            timer.Stop();
            timeBabbleSort = timer.Elapsed.ToString();

            timer.Restart();
            InsertionSort(cloneArray);
            timer.Stop();
            timeInsertionSort = timer.Elapsed.ToString();
        }

        /// <summary>
        /// Получить среднее арифметическое ячеек
        /// </summary>
        /// <returns></returns>
        public double GetArevageValue()
        {
            return array.Average();
        }

        /// <summary>
        /// Получить минимальное значение в массиве
        /// </summary>
        /// <returns></returns>
        public int GetMinValue()
        {
            return array.Min();
        }

        /// <summary>
        /// Получить максимальное значение
        /// </summary>
        /// <returns></returns>
        public int GetMaxValue()
        {
            return array.Max();
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
