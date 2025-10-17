using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Timers;

namespace Lab5
{
    internal class ConsoleTetris
    {
        private const byte height = 10;
        private const byte width = 10;
        private const byte reserveLines = 3;
        private float delay = 1000f;
        private byte[,] field = new byte[height + reserveLines, width];
        private static byte[,] figureL1 = { { 1, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
        private static byte[,] figureL2 = { { 0, 0, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
        private static byte[,] figureZ1 = { { 1, 1, 0 }, { 0, 1, 1 }, { 0, 0, 0 } };
        private static byte[,] figureZ2 = { { 0, 1, 1 }, { 1, 1, 0 }, { 0, 0, 0 } };
        private static byte[,] figureI = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 } };
        private static byte[,] figureO = { { 1, 1 }, { 1, 1 } };
        private static byte[,] figureT = { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
        private byte[][,] figures = { figureL1, figureL2, figureZ1, figureZ2, figureI, figureO, figureT };

        private int score = 0;
        private int lines = 0;
        private int tetris = 0;

        private bool isRightBorrier;
        private bool isLeftBorrier;

        static Random random = new Random();
        static System.Timers.Timer timer = new System.Timers.Timer();

        /// <summary>
        /// Запускает игру
        /// </summary>
        public void StartGame()
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
        private void Input(ConsoleKey key)
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
        private void MoveLeft()
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
        private void MoveRight()
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
        private void MoveDown()
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
        private bool CheckDown()
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
        private void FreezeFigure()
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
                    for (int j = y; j >= reserveLines; j--)
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
        private void Tick(object sender, ElapsedEventArgs e)
        {
            MoveDown();

            timer.Interval = delay;
            timer.Start();
        }

        /// <summary>
        /// Записывает фигуру в матрицу
        /// </summary>
        private void SpawnFigure()
        {
            isLeftBorrier = false;
            isRightBorrier = false;

            byte[,] figure = figures[random.Next(figures.Length)];

            int spawnCell = (width - figure.GetLength(1)) / 2;

            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = spawnCell; j < spawnCell + figure.GetLength(1); j++)
                {
                    field[i, j] = figure[i, j - spawnCell];
                }
            }
        }

        /// <summary>
        /// Отрисовывает весь игровой интерфейс
        /// </summary>
        /// <param name="map"></param>
        private void DrawGame()
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
        private void DrawField(byte[,] field, string barrier)
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
        private void ClearField()
        {
            Console.Clear();
        }

        /// <summary>
        /// Завершает программу
        /// </summary>
        private void ExitGame()
        {
            Console.WriteLine("Чтобы выйти из игры нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
