using System;

namespace Lab6
{
    public enum TetrominoType { I, J, L, O, S, T, Z }

    public class Tetromino
    {
        public TetrominoType Type { get; }
        public int[,] Shape { get; private set; }
        public int Width => Shape.GetLength(1);
        public int Height => Shape.GetLength(0);

        public Tetromino(TetrominoType type)
        {
            Type = type;
            Shape = GetTetrominoShape(type);
        }

        /// <summary>
        /// Возвращает форму указанного тетромино
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int[,] GetTetrominoShape(TetrominoType type)
        {
            switch (type)
            {
                case TetrominoType.I:
                    return new int[,] { { 1, 1, 1, 1 } };
                case TetrominoType.J:
                    return new int[,] { { 1, 0, 0 }, { 1, 1, 1 } };
                case TetrominoType.L:
                    return new int[,] { { 0, 0, 1 }, { 1, 1, 1 } };
                case TetrominoType.O:
                    return new int[,] { { 1, 1 }, { 1, 1 } };
                case TetrominoType.S:
                    return new int[,] { { 0, 1, 1 }, { 1, 1, 0 } };
                case TetrominoType.T:
                    return new int[,] { { 0, 1, 0 }, { 1, 1, 1 } };
                case TetrominoType.Z:
                    return new int[,] { { 1, 1, 0 }, { 0, 1, 1 } };
                default:
                    return new int[0, 0];
            }
        }

        /// <summary>
        /// Поворачивает тетромино направо
        /// </summary>
        public void Rotate()
        {
            int rows = Shape.GetLength(0);
            int cols = Shape.GetLength(1);
            int[,] rotatedShape = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedShape[j, rows - 1 - i] = Shape[i, j];
                }
            }
            Shape = rotatedShape;
        }

        /// <summary>
        /// Поворачивает тетромино обратно
        /// </summary>
        public void RotateBack()
        {
            int rows = Shape.GetLength(0);
            int cols = Shape.GetLength(1);
            int[,] rotatedShape = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedShape[cols - 1 - j, i] = Shape[i, j];
                }
            }
            Shape = rotatedShape;
        }
    }
}