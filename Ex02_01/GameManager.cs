using System;
/*
namespace Ex02_01
{
    internal class GameManager : CheckersBoard
    {
        public GameManager(int size, string name, string opponentChoice) : base(size, name, opponentChoice) { }

        public bool Move(string playerMove)
        {
            // Validate input format
            if (string.IsNullOrEmpty(playerMove) || !playerMove.Contains(">") || playerMove.Length != 5)
            {
                Console.WriteLine("Invalid format! Please enter in the format RowCol>RowCol.");
                return false;
            }

            var positions = playerMove.Split('>');
            if (positions.Length != 2)
            {
                Console.WriteLine("Invalid move format! Please use the format RowCol>RowCol.");
                return false;
            }

            string from = positions[0];
            string to = positions[1];

            // Validate individual positions
            if (!ValidatePosition(from) || !ValidatePosition(to))
            {
                Console.WriteLine("Invalid positions! Please check your input.");
                return false;
            }

            // Convert positions to board indices
            int fromRow = from[1] - 'a';
            int fromCol = from[0] - 'A';
            int toRow = to[1] - 'a';
            int toCol = to[0] - 'A';

            // Validate move indices
            if (!ValidateIndices(fromRow, fromCol, toRow, toCol))
            {
                Console.WriteLine("Invalid move! Please try again.");
                return false;
            }

            // Perform the move
            m_Board[toRow, toCol] = m_Board[fromRow, fromCol];
            m_Board[fromRow, fromCol] = ' ';

            return true;
        }

        private bool ValidatePosition(string position)
        {
            if (string.IsNullOrEmpty(position) || position.Length != 2)
            {
                return false;
            }

            char col = position[0];
            char row = position[1];

            return col >= 'A' && col < 'A' + m_Size &&
                   row >= 'a' && row < 'a' + m_Size;
        }

        private bool ValidateIndices(int fromRow, int fromCol, int toRow, int toCol)
        {
            return fromRow >= 0 && fromRow < m_Size &&
                   fromCol >= 0 && fromCol < m_Size &&
                   toRow >= 0 && toRow < m_Size &&
                   toCol >= 0 && toCol < m_Size;
        }

        public void LogicValidation(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowDiff = Math.Abs(toRow - fromRow);
            int colDiff = Math.Abs(toCol - fromCol);

            if (rowDiff == 1 && colDiff == 1)
            {
                Console.WriteLine("Valid move.");
            }
            else
            {
                Console.WriteLine("Invalid move. Only diagonal moves are allowed.");
            }
        }
    }
}
*/