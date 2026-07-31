using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab6
{
    /// <summary>
    /// Логика взаимодействия для ComparingArraySortsWindow.xaml
    /// </summary>
    public partial class ComparingArraySortsWindow : Window
    {
        private ComparingArraySorts cas;
        public ComparingArraySortsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Посчитать среднее арифметическое значений в массиве
        /// </summary>
        private void AverageArray_Click(object sender, RoutedEventArgs e)
        {
            if (ArrayDataGrid.ItemsSource is List<int[]> items && items.Count > 0)
            {
                int[] row = items[0];

                int[] numbers = new int[row.Length];

                for (int i = 0; i < row.Length; i++)
                {
                    numbers[i] = row[i];
                }

                AverageTB.Text = Math.Round(numbers.Average(),1).ToString();
            }
        }

        /// <summary>
        /// Красит в цвет минимальное и максимальное значение в массиве
        /// </summary>
        private void SearchMaxAndMinArray(object sender, RoutedEventArgs e)
        {
            if (ArrayDataGrid.ItemsSource is List<int[]> items && items.Count > 0)
            {
                int[] row = items[0];

                int[] numbers = new int[row.Length];

                for (int i = 0; i < row.Length; i++)
                {
                    numbers[i] = row[i];
                }

                ColorDataGridCells(ArrayDataGrid, numbers, numbers.Min(), Brushes.Blue, numbers.Max(), Brushes.Red);
            }
        }

        /// <summary>
        /// Сортирует массив и выводит на экран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (ArrayDataGrid.ItemsSource is List<int[]> items && items.Count > 0)
            {
                int[] row = items[0];

                int[] numbers = new int[row.Length];

                for (int i = 0; i < row.Length; i++)
                {
                    numbers[i] = row[i];
                }

                cas = new ComparingArraySorts(numbers);
                cas.CompareSorting();

                BabbleSortTB.Text = cas.TimeBabbleSort;
                InsertSortTB.Text = cas.TimeInsertionSort;

                CreateColumnDG(numbers);
            }
        }

        /// <summary>
        /// Создает массив
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateArray(object sender, RoutedEventArgs e)
        {
            if (InputControl.ControlNaturalNumbersTextBox(LenghtTextBox, "#FF202123"))
            {
                if (int.Parse(LenghtTextBox.Text) > 10)
                    WarningTB.Visibility = Visibility.Visible;
                else
                    WarningTB.Visibility = Visibility.Collapsed;

                int[] numbers = new int[int.Parse(LenghtTextBox.Text)];

                for (int i = 0; i < numbers.Length; i++)
                    numbers[i] = 0;

                CreateColumnDG(numbers);
            }
        }

        /// <summary>
        /// Генерирует случайный массив
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomArrayButton_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            int[] numbers = new int[random.Next(5,11)];

            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = random.Next(100);

            CreateColumnDG(numbers);
        }

        /// <summary>
        /// Создает дефолтный массив
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultArrayButton_Click(object sender, RoutedEventArgs e)
        {
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            CreateColumnDG(numbers);
        }

        /// <summary>
        /// Возвращает пользователя в меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Создает колонки на основе массива в DataGrid
        /// </summary>
        /// <param name="array"></param>
        private void CreateColumnDG(int[] array)
        {
            ArrayDataGrid.Columns.Clear();

            for (int i = 0; i < array.Length; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Binding = new System.Windows.Data.Binding($"[{i}]")
                };
                ArrayDataGrid.Columns.Add(column);
            }
            ArrayDataGrid.ItemsSource = new List<int[]> { array };
        }

        /// <summary>
        /// Закрашивает ячейки таблицы по совпавшему значению
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="data"></param>
        /// <param name="targetValue"></param>
        /// <param name="brush"></param>
        private void ColorDataGridCells(DataGrid dataGrid, int[] data, int targetMinValue, SolidColorBrush brushMin, int targetMaxValue, SolidColorBrush brushMax)
        {
            if (data == null || data.Length == 0 || dataGrid == null)
            {
                return;
            }

            for (int i = 0; i < data.Length; i++)
            {
                Style cellStyle = new Style(typeof(DataGridCell));
                cellStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center));
                int index = i;
                cellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new Func<object, Brush>(o =>
                {
                    if (o != null)
                    {
                        int currentNumber = data[index];
                        if (currentNumber == targetMinValue)
                        {
                            return brushMin;
                        }
                        if (currentNumber == targetMaxValue)
                        {
                            return brushMax;
                        }
                    }
                    return null;
                }).Invoke(data[i])));
                dataGrid.Columns[i].CellStyle = cellStyle;
            }
        }
    }
}
