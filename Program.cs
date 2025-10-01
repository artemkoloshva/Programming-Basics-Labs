using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.ConsoleTetris
{
    internal class Program
    {
        static byte height = 10;
        static byte width = 10;
        static byte reserveLines = 3;
        static float delay = 1000f;
        static bool isRightBorrier;
        static bool isLeftBorrier;
        static byte[,] field = new byte[height + reserveLines, width];

        static int score = 0;
        static int lines = 0;
        static int tetris = 0;

        static byte[,] figureL1 = { { 1, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
        static byte[,] figureL2 = { { 0, 0, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
        static byte[,] figureZ1 = { { 1, 1, 0 }, { 0, 1, 1 }, { 0, 0, 0 } };
        static byte[,] figureZ2 = { { 0, 1, 1 }, { 1, 1, 0 }, { 0, 0, 0 } };
        static byte[,] figureI = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 } };
        static byte[,] figureO = { { 1, 1 }, { 1, 1 } };
        static byte[,] figureT = { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
        static byte[][,] figures = { figureL1, figureL2, figureZ1, figureZ2, figureI, figureO, figureT };

        static Random random = new Random();
        static Timer timer = new Timer();

        static private void Main(string[] args)
        {
            SpawnFigure();

            DrawGame();

            timer.Elapsed += Tick;
            timer.Interval = delay;
            timer.Start();

            ConsoleKeyInfo consoleKeyInfo;
            while ((consoleKeyInfo = Console.ReadKey()) != null)
                Input(consoleKeyInfo.Key);

            ExitGame();
        }

        /// <summary>
        /// Считывает нажатую клавишу пользователя
        /// </summary>
        /// <param name="key"></param>
        static private void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.UpArrow:
                    Console.WriteLine("Rotate");
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                default: 
                    break;
            }
        }

        /// <summary>
        /// Переносит фигуру влево
        /// </summary>
        static private void MoveLeft()
        {
            ClearField();
            isRightBorrier = false;
            if (!isLeftBorrier)
            {
                for (int y = 0; y < field.GetLength(0); y++)
                {
                    for (int x = 0; x < field.GetLength(1) - 1; x++)
                    {
                        if (field[y, x] == 0 && field[y, x + 1] == 1)
                        {
                            field[y, x] = 1;
                            field[y, x + 1] = 0;

                            if (x == 0)
                                isLeftBorrier = true;
                        }
                    }
                }
            }
            DrawGame();
        }

        /// <summary>
        /// Переносит фигуру вправо
        /// </summary>
        static private void MoveRight()
        {
            ClearField();
            isLeftBorrier = false;
            if (!isRightBorrier)
            {
                for (int y = field.GetLength(0) - 1; y >= 0; y--)
                {
                    for (int x = field.GetLength(1) - 1; x > 0; x--)
                    {
                        if (field[y, x] == 0 && field[y, x - 1] == 1)
                        {
                            field[y, x] = 1;
                            field[y, x - 1] = 0;

                            if (x == field.GetLength(1) - 1)
                                isRightBorrier = true;
                        }
                    }
                }
            }
            DrawGame();
        }

        /// <summary>
        /// Переносит фигуру вниз
        /// </summary>
        static private void MoveDown()
        {
            ClearField();
            if (CheckDown())
            {
                for (int y = field.GetLength(0) - 1; y >= 0; y--)
                {
                    for (int x = field.GetLength(1) - 1; x >= 0; x--)
                    {
                        if (field[y, x] == 1)
                        {
                            field[y + 1, x] = 1;
                            field[y, x] = 0;
                        }
                    }
                }
            }
            else
            {
                FreezeFigure();
                SpawnFigure();
            }
            DrawGame();
        }

        /// <summary>
        /// Проверяет есть ли снизу граница или блок
        /// </summary>
        static private bool CheckDown()
        {
            bool isDown = true;
            for (int y = 0; y < field.GetLength(0) && isDown; y++)
            {
                for (int x = 0; x < field.GetLength(1) && isDown; x++)
                {
                    if (field[y, x] == 1)
                    {
                        if (y == field.GetLength(0) - 1)
                        {
                            isDown = false;
                        }
                        else if (field[y + 1, x] == 2)
                            isDown = false;
                    }
                }
            }
            return isDown;
        }

        /// <summary>
        /// Замораживает фигуру
        /// </summary>
        static private void FreezeFigure()
        {
            byte countKilledLines = 0;
            byte fullLine = 0;
            for (int y = 0; y < field.GetLength(0); y++)
            {
                fullLine = 0;
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    if (field[y, x] == 1)
                    {
                        field[y, x] = 2;
                    }

                    if (field[y, x] == 2)
                        fullLine++;
                }

                if (fullLine == field.GetLength(1))
                {
                    for(int j = y; j >= reserveLines; j--)
                    {
                        for (int i = 0; i < field.GetLength(1); i++)
                        {
                            field[j, i] = field[j - 1, i];
                        }
                    }
                    lines++;
                    countKilledLines++;
                }
            }

            switch (countKilledLines)
            { 
                case 1:
                    score += 100;
                    break;
                case 2:
                    score += 300;
                    break;
                case 3:
                    score += 700;
                    break;
                case 4:
                    score += 1500;
                    tetris++;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Игровой тик
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static private void Tick(object sender, ElapsedEventArgs e)
        {
            MoveDown();

            timer.Interval = delay;
            timer.Start();
        }

        /// <summary>
        /// Записывает фигуру в матрицу
        /// </summary>
        static private void SpawnFigure()
        {
            isLeftBorrier = false;
            isRightBorrier = false;

            byte[,] figure = figures[random.Next(figures.Length)];

            int spawnCell = (width - figure.GetLength(1)) / 2;

            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = spawnCell; j < spawnCell + figure.GetLength(1); j++)
                {
                    field[i,j] = figure[i,j - spawnCell];
                }
            }
        }

        /// <summary>
        /// Отрисовывает весь игровой интерфейс
        /// </summary>
        /// <param name="map"></param>
        static private void DrawGame()
        {
            string title = "\r\n  ▀▀█▀▀  █▀▀▀ ▀▀█▀▀  █▀▀█ ▀█▀  █▀▀▀█" +
                           "\r\n    █    █▀▀▀   █    █▄▄▀  █   ▀▀▀▄▄" +
                           "\r\n    █    █▄▄▄   █    █  █ ▄█▄  █▄▄▄█";
            string version = "vLab4";
            Console.WriteLine($"{title} {version}\n");
            DrawField(field, "║");
            Console.WriteLine($"Score:{score}   Lines:{lines}   Tetris:{tetris}");
        }

        /// <summary>
        /// Отрисовывает игровое поле
        /// </summary>
        /// <param name="map"></param>
        static private void DrawField(byte[,] field, string barrier)
        {
            for (int y = reserveLines; y < field.GetLength(0); y++)
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.Write(barrier);
                    for (int x = 0; x < field.GetLength(1); x++)
                    {
                        switch (field[y, x])
                        {
                            case 0:
                                Console.Write("    ");
                                break;
                            case 1:
                                Console.Write("░░░░");
                                break;
                            case 2:
                                Console.Write("████");
                                break;
                        }
                    }
                    Console.WriteLine(barrier);
                }
            }
            Console.WriteLine("╚════════════════════════════════════════╝");
        }

        /// <summary>
        /// Обновляет игровое поле
        /// </summary>
        static private void ClearField()
        {
            Console.Clear();
        }

        /// <summary>
        /// Завершает программу
        /// </summary>
        static private void ExitGame()
        {
            Console.WriteLine("Чтобы выйти из игры нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
