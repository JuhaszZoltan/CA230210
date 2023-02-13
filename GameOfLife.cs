using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace CA230210
{
    internal class GameOfLife
    {
        private Random random;
        public bool[,] Matrix { get; set; }

        private bool[,] NextState()
        {
            bool[,] next = new bool[Matrix.GetLength(0), Matrix.GetLength(1)];

            for (int r = 0; r < Matrix.GetLength(0); r++)
            {
                for (int c = 0; c < Matrix.GetLength(1); c++)
                {
                    int non = NoNeighbours(r, c);
                    if (Matrix[r, c] && (non == 2 || non == 3)) next[r, c] = true;
                    else if (non == 3) next[r, c] = true;
                }
            }
            return next;
        }

        private int NoNeighbours(int r, int c)
        {
            int non = 0;
            for (int nri = r - 1; nri <= r + 1; nri++)
            {
                for (int nci = c - 1; nci <= c + 1; nci++)
                {
                    if (nri >= 0 &&
                        nci >= 0 &&
                        nri <= Matrix.GetLength(0) - 1 &&
                        nci <= Matrix.GetLength(1) - 1 &&
                        (nri != r || nci != c) &&
                        Matrix[nri, nci]) non++;
                }
            }
            return non;
        }

        public void Simulate()
        {
            Draw();
            Thread.Sleep(120);
            Matrix = NextState();
            AnsiConsole.Cursor.SetPosition(0, 0);
        }

        public void Draw()
        {
            for (int r = 0; r < Matrix.GetLength(0); r++)
            {
                for (int c = 0; c < Matrix.GetLength(1); c++)
                {
                    AnsiConsole.Write(Matrix[r, c] ? '\u2588' : ' ');
                }
                AnsiConsole.Write('\n');
            }
        }

        public GameOfLife(int row, int col, int seed)
        {

            Console.SetWindowSize(col, row + 1);
            Console.CursorVisible = false;

            random = new(seed);
            Matrix = new bool[row, col];

            for (int ri = 0; ri < row; ri++)
            {
                for (int ci = 0; ci < col; ci++)
                {
                    Matrix[ri, ci] = random.Next(2) == 1;
                }
            }
        }
    }
}
