using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab6
{
    public class TetrisGame
    {
        private const int BoardWidth = 8;
        private const int BoardHeight = 10;

        private int[,] _board;
        public Tetromino _currentTetromino;
        public int _currentX, _currentY;
        private Random _random;

        public int Score { get; private set; }
        public int Level { get; private set; }

        public int Tetrises { get; private set; }

        public int Lines { get; private set; }
        public bool GameOver { get; private set; }

        public int[,] Board => _board;

        public TetrisGame()
        {
            _board = new int[BoardHeight, BoardWidth];
            _random = new Random();
            Score = 0;
            Level = 1;
            GameOver = false;
            SpawnNewTetromino();
        }

        /// <summary>
        /// Выбирает случайное тетромино и задает ему начальные координаты
        /// </summary>
        private void SpawnNewTetromino()
        {
            int nextTetrominoType = _random.Next(7);
            _currentTetromino = new Tetromino((TetrominoType)nextTetrominoType);
            _currentX = BoardWidth / 2 - _currentTetromino.Width / 2;
            _currentY = 0;

            if (!IsTetrominoValid())
            {
                GameOver = true;
            }
        }

        /// <summary>
        /// Перемещает тетромино вниз 
        /// </summary>
        public void MoveTetrominoDown()
        {
            if (GameOver) return;

            _currentY++;
            if (!IsTetrominoValid())
            {
                _currentY--;
                PlaceTetromino();
                CheckForFullRows();
                SpawnNewTetromino();
            }
        }

        /// <summary>
        /// Перемещает тетромино влево
        /// </summary>
        public void MoveTetrominoLeft()
        {
            if (GameOver) return;

            _currentX--;
            if (!IsTetrominoValid())
            {
                _currentX++;
            }
        }

        /// <summary>
        /// Перемещает тетромино вправо
        /// </summary>
        public void MoveTetrominoRight()
        {
            if (GameOver) return;

            _currentX++;
            if (!IsTetrominoValid())
            {
                _currentX--;
            }
        }

        /// <summary>
        /// Поворачивает тетромино направо
        /// </summary>
        public void RotateTetromino()
        {
            if (GameOver) return;

            _currentTetromino.Rotate();
            if (!IsTetrominoValid())
            {
                _currentTetromino.RotateBack();
            }
        }

        /// <summary>
        /// Проверяет на возможность расположения тетромино на поле
        /// </summary>
        /// <returns></returns>
        private bool IsTetrominoValid()
        {
            for (int y = 0; y < _currentTetromino.Height; y++)
            {
                for (int x = 0; x < _currentTetromino.Width; x++)
                {
                    if (_currentTetromino.Shape[y, x] == 1)
                    {
                        int boardX = _currentX + x;
                        int boardY = _currentY + y;

                        if (boardX < 0 || boardX >= BoardWidth || boardY >= BoardHeight)
                        {
                            return false;
                        }
                        if (boardY >= 0 && _board[boardY, boardX] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Записывает тетромино в поле игры
        /// </summary>
        private void PlaceTetromino()
        {
            for (int y = 0; y < _currentTetromino.Height; y++)
            {
                for (int x = 0; x < _currentTetromino.Width; x++)
                {
                    if (_currentTetromino.Shape[y, x] == 1)
                    {
                        int boardX = _currentX + x;
                        int boardY = _currentY + y;
                        if (boardY >= 0 && boardY < BoardHeight && boardX >= 0 && boardX < BoardWidth)
                        {
                            _board[boardY, boardX] = (int)_currentTetromino.Type + 1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет строку поля на заполненность
        /// </summary>
        private void CheckForFullRows()
        {
            int rowsCleared = 0;
            for (int y = BoardHeight - 1; y >= 0; y--)
            {
                bool fullRow = true;
                for (int x = 0; x < BoardWidth; x++)
                {
                    if (_board[y, x] == 0)
                    {
                        fullRow = false;
                        break;
                    }
                }

                if (fullRow)
                {
                    rowsCleared++;
                    ClearRow(y);
                    Lines++;
                    y++;
                }

            }

            if (rowsCleared == 4)
                Tetrises++;

            if (rowsCleared > 0)
            {
                Score += 100 * rowsCleared * Level;
                if (Score / 1000 > Level)
                {
                    Level++;
                }
            }
        }

        /// <summary>
        /// Удаляет определенную строку и смещает все строки, находящиеся выше, вниз
        /// </summary>
        /// <param name="row"></param>
        private void ClearRow(int row)
        {
            for (int y = row; y > 0; y--)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    _board[y, x] = _board[y - 1, x];
                }
            }
            for (int x = 0; x < BoardWidth; x++)
            {
                _board[0, x] = 0;
            }
        }
    }
}