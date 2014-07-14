using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _0x800
{
    class Matrix<T>
    {
        List<T> matrix;
        int width, height;
        internal Matrix(int width, int height, T init)
        {
            this.width = width; this.height = height;
            int length = width * height;
            matrix = new List<T>(length);
            for (int i = 0; i < length; i++)
            {
                matrix.Add(init);
            }
        }
        internal Matrix(int size, T init)
            : this(size, size, init)
        {

        }

        internal T this[int index]
        {
            get
            {
                return matrix[index];
            }
            set
            {
                matrix[index] = value;
            }
        }
        internal T this[int x, int y]
        {
            get
            {
                int pos = (y - 1) * width + x - 1;
                return matrix[pos];
            }
            set
            {
                int pos = (y - 1) * width + x - 1;
                matrix[pos] = value;
            }
        }
        internal List<T> this[int xStart, int xEnd, int yStart, int yEnd]
        {
            get
            {
                int start = (yStart - 1) * width + xStart - 1;
                int end = (yEnd - 1) * width + xEnd - 1;
                return matrix.GetRange(start, end - start + 1);
            }
            set
            {
                int start = (yStart - 1) * width + xStart - 1;
                int end = (yEnd - 1) * width + xEnd - 1;
                for (int i = start; i <= end; i++)
                {
                    matrix[i] = value[0]; value.RemoveAt(0);
                }
            }
        }
        internal IList<T> this[IList<Pair<int>> positions]
        {
            set
            {
                while (positions.Count != 0)
                {
                    this[positions[0][0], positions[0][1]] = value[0];
                    value.RemoveAt(0); positions.RemoveAt(0);
                }
            }
        }

        public override string ToString()
        {
            string res = "";
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    res += this[x + 1, y + 1].ToString().PadLeft(5);
                }
                if (y + 1 < height)
                    res += "\r\n\r\n";
            }
            return res;
        }

        internal List<T> GetRow(int i)
        {
            return this[1, width, i, i];
        }

        internal int Height { get { return height; } }

        internal int Width { get { return width; } }

        internal void RotateCounterClock()
        {
            int h = height; height = width; width = h;

            List<T> matrix_cpy = new List<T>(matrix);
            for (int x = 1; x <= width; x++)
            {
                for (int y = 1; y <= height; y++)
                {
                    this[x, y] = matrix_cpy[(x - 1) * width + y - 1];
                }
            }

            FlipY();
        }
        internal void RotateClock()
        {
            int count = 0;
            while (count < 3)
            {
                count++;
                RotateCounterClock();
            }
        }
        internal void FlipY()
        {
            for (int i = 1; i <= height / 2; i++)
            {
                List<T> row = this[1, width, i, i];
                this[1, width, i, i] = this[1, width, height - i + 1, height - i + 1];
                this[1, width, height - i + 1, height - i + 1] = row;
            }
        }
        internal void FlipX()
        {
            RotateClock();
            FlipY();
            RotateCounterClock();
        }

        public int Size { get { return height * width; } }
    }

    class Pair<T> // Pair wraps two items of the same type
    {
        T val1, val2;
        internal Pair(T val1, T val2)
        {
            this.val1 = val1; this.val2 = val2;
        }
        internal T this[int index]
        {
            get
            {
                if (index == 0) return val1;
                return val2;
            }
            set
            {
                if (index == 0) val1 = value;
                val2 = value;
            }
        }
    }
}
