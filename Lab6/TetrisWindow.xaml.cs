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
using System.Windows.Threading;

namespace Lab6
{
    /// <summary>
    /// Логика взаимодействия для TetrisWindow.xaml
    /// </summary>
    public partial class TetrisWindow : Window
    {
        private TetrisGame _game;
        private DispatcherTimer _timer;
        private SolidColorBrush[] _colors = { Brushes.LightGray, Brushes.Cyan, Brushes.Blue, Brushes.Orange, Brushes.Yellow, Brushes.Green, Brushes.Purple, Brushes.Red };
        private Style[] _columnStyles;
        public TetrisWindow()
        {
            InitializeComponent();
            Focus();
        }

        /// <summary>
        /// Инициализация DataGrid
        /// </summary>
        private void InitializeDataGrid()
        {
            int boardWidth = _game.Board.GetLength(1);
            _columnStyles = new Style[boardWidth];

            for (int i = 0; i < boardWidth; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Binding = new Binding($"[{i}]"),
                    IsReadOnly = true
                };

                _columnStyles[i] = new Style(typeof(DataGridCell))
                {
                    Setters = 
                    {
                            new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center),
                            new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center),
                            new Setter(Control.BorderThicknessProperty, new Thickness(0)),
                            new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center),
                            new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center)
                    }
                };
                column.CellStyle = _columnStyles[i];
                BoardDataGrid.Columns.Add(column);
            }
        }

        /// <summary>
        /// Игровой тик
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_game.GameOver)
            {
                _timer.Stop();
                GameOver();
                return;
            }

            _game.MoveTetrominoDown();
            UpdateBoardDataGrid();
            UpdateScoreAndLevel();
        }

        /// <summary>
        /// Обновляет DataGrid
        /// </summary>
        private void UpdateBoardDataGrid()
        {
            int[,] board = _game.Board;
            int boardWidth = board.GetLength(1);
            int boardHeight = board.GetLength(0);

            int[,] tempBoard = new int[boardHeight, boardWidth];

            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    tempBoard[y, x] = board[y, x];
                }
            }

            if (!_game.GameOver)
            {
                int[,] tetrominoShape = _game._currentTetromino.Shape;
                int tetrominoHeight = tetrominoShape.GetLength(0);
                int tetrominoWidth = tetrominoShape.GetLength(1);
                for (int y = 0; y < tetrominoHeight; y++)
                {
                    for (int x = 0; x < tetrominoWidth; x++)
                    {
                        if (tetrominoShape[y, x] == 1)
                        {
                            int boardX = _game._currentX + x;
                            int boardY = _game._currentY + y;

                            if (boardX >= 0 && boardX < boardWidth && boardY >= 0 && boardY < boardHeight)
                            {
                                tempBoard[boardY, boardX] = (int)_game._currentTetromino.Type + 1;
                            }
                        }
                    }
                }
            }

            List<int[]> data = new List<int[]>();
            for (int y = 0; y < boardHeight; y++)
            {
                int[] row = new int[boardWidth];
                for (int x = 0; x < boardWidth; x++)
                {
                    row[x] = tempBoard[y, x];
                }
                data.Add(row);
            }
            BoardDataGrid.ItemsSource = data;
            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    DataGridRow row = GetVisualRow(y);
                    if (row != null)
                    {
                        DataGridCell cell = GetVisualCell(row, x);
                        if (cell != null)
                        {
                            var brush = GetBrush(tempBoard[y, x], _colors);
                            Style cellStyle = new Style(typeof(DataGridCell));
                            cellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, brush));
                            cellStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));
                            cellStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
                            cellStyle.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
                            cell.Style = cellStyle;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает экземпляр строки
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private DataGridRow GetVisualRow(int index)
        {
            DataGridRow row = BoardDataGrid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (row == null)
            {
                BoardDataGrid.ScrollIntoView(BoardDataGrid.Items[index]);
                row = BoardDataGrid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            }
            return row;
        }

        /// <summary>
        /// Возвращает экземпляр ячейки
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private DataGridCell GetVisualCell(DataGridRow row, int column)
        {
            DataGridCell cell = BoardDataGrid.Columns[column].GetCellContent(row)?.Parent as DataGridCell;
            return cell;
        }

        /// <summary>
        /// Возвращает определенную кисть
        /// </summary>
        /// <param name="value"></param>
        /// <param name="colors"></param>
        /// <returns></returns>
        private static SolidColorBrush GetBrush(int value, SolidColorBrush[] colors)
        {
            if (value > 0 && value < colors.Length)
            {
                return colors[value];
            }
            return (SolidColorBrush)new BrushConverter().ConvertFromString("#FF393C3C");
        }

        /// <summary>
        /// Обновление полей игровой информации
        /// </summary>
        private void UpdateScoreAndLevel()
        {
            ScoreTextBlock.Text = _game.Score.ToString();
            LevelTextBlock.Text = _game.Level.ToString();
            LinesTextBlock.Text = _game.Lines.ToString();
            TetrisesTextBlock.Text = _game.Tetrises.ToString();
        }

        /// <summary>
        /// Обработка нажатых клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_game.GameOver)
                return;

            switch (e.Key)
            {
                case System.Windows.Input.Key.Left:
                    _game.MoveTetrominoLeft();
                    break;
                case System.Windows.Input.Key.Right:
                    _game.MoveTetrominoRight();
                    break;
                case System.Windows.Input.Key.Down:
                    _game.MoveTetrominoDown();
                    break;
                case System.Windows.Input.Key.Up:
                    _game.RotateTetromino();
                    break;
            }
            UpdateBoardDataGrid();
        }

        /// <summary>
        /// Запускает тетрис
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _game = new TetrisGame();
            InitializeDataGrid();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            UpdateBoardDataGrid();
            UpdateScoreAndLevel();

            StartButton.Content = "Р е с т а р т";
        }

        /// <summary>
        /// Закрашивает все блоки в красный
        /// </summary>
        private void GameOver()
        {
            int[,] board = _game.Board;
            int boardWidth = board.GetLength(1);
            int boardHeight = board.GetLength(0);

            int[,] gameOverBoard = new int[boardHeight, boardWidth];
            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    gameOverBoard[y, x] = board[y, x];
                }
            }

            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    if (gameOverBoard[y, x] != 0)
                    {
                        gameOverBoard[y, x] = 7;
                    }
                }
            }

            List<int[]> data = new List<int[]>();
            for (int y = 0; y < boardHeight; y++)
            {
                int[] row = new int[boardWidth];
                for (int x = 0; x < boardWidth; x++)
                {
                    row[x] = gameOverBoard[y, x];
                }
                data.Add(row);
            }
            BoardDataGrid.ItemsSource = data;

            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    DataGridRow row = GetVisualRow(y);
                    if (row != null)
                    {
                        DataGridCell cell = GetVisualCell(row, x);
                        if (cell != null)
                        {
                            var brush = GetBrush(gameOverBoard[y, x], _colors);
                            cell.Style = new Style(typeof(DataGridCell))
                            {
                                Setters =
                                {
                                    new Setter(DataGridCell.BackgroundProperty, brush),
                                    new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center),
                                    new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center),
                                    new Setter(Control.BorderThicknessProperty, new Thickness(0)),
                                    new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center),
                                    new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center)
                                }
                            };
                        }
                    }
                }
            }
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
    }
}
