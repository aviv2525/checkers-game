using System;
using System.Dynamic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;


namespace Ex02_01
{
    internal class CheckersBoard
    {
        public char[,] m_Board { get; private set; }
        protected int m_Size { get; private set; }
        private string m_Name { get; set; }
        private string m_ChooseVsComPlayer { get; set; }
        public CheckersBoard(int m_Size, string m_name, string m_ChooseVsComPlayer)
        {
            this.m_Name = m_Name;
            this.m_Size = m_Size;
            this.m_ChooseVsComPlayer = m_ChooseVsComPlayer;
            m_Board = GenerateBoard(m_Size);
        }

        public void DisplayBoard()
        {
            int size = m_Board.GetLength(0);
            Console.Write(" ");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"  {(char)('a' + i)} ");
            }
            Console.WriteLine("\n  " + new string('=', size * 4));

            for (int i = 0; i < size; i++)
            {
                Console.Write($"{(char)('A' + i)}|");
                for (int j = 0; j < size; j++)
                {
                    Console.Write($" {m_Board[i, j]} |");
                }
                Console.WriteLine("\n  " + new string('=', size * 4));
            }
        }

        private char[,] GenerateBoard(int size)
        {
            char[,] board = new char[size, size];
            char xPlayer = 'X';
            char oPlayer = 'O';
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i < size / 2 - 1 && (i + j) % 2 == 1)
                    {
                        board[i, j] = oPlayer;
                    }
                    else if (i > size / 2 && (i + j) % 2 == 1)
                    {
                        board[i, j] = xPlayer;
                    }
                    else
                    {
                        board[i, j] = ' ';
                    }
                }
            }
            return board;
        }
    }
}