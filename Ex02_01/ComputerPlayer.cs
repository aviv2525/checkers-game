using System;
using System.Collections.Generic;

namespace Ex02_01
{
    internal class ComputerPlayer : MoveHandler
    {
        public ComputerPlayer(int size, string name, string chooseVsComPlayer) : base(size, name, chooseVsComPlayer)
        {
        }

        public void MakeMove(ref bool ExecutedMove)
        {
            var validMoves = GetAllValidMoves('O');
            if (validMoves.Count == 0)
            {
                Console.WriteLine("Computer has no valid moves!");
                ExecutedMove = false;
            }

            var random = new Random();
            var chosenMove = validMoves[random.Next(validMoves.Count)];

            Console.WriteLine($"Computer move: {chosenMove.From}>{chosenMove.To}");
            //base.MakeMove($"{chosenMove.From}>{chosenMove.To}");
            ExecutedMove = true;
        }

        private List<Move> GetAllValidMoves(char piece)
        {
            var validMoves = new List<Move>();

            for (int row = 0; row < m_Size; row++)
            {
                for (int col = 0; col < m_Size; col++)
                {
                    if (m_Board[row, col] == piece)
                    {
                        foreach (var direction in GetMoveDirections())
                        {
                            int newRow = row + direction.Row;
                            int newCol = col + direction.Col;

                            // Validate the new position is within bounds
                            if (IsWithinBounds(newRow, newCol) && IsMoveAllowed(row, col, newRow, newCol))
                            {
                                validMoves.Add(new Move
                                {
                                    From = $"{(char)('A' + col)}{(char)('a' + row)}",
                                    To = $"{(char)('A' + newCol)}{(char)('a' + newRow)}"
                                });
                            }
                        }
                    }
                }
            }

            return validMoves;
        }

        private List<Direction> GetMoveDirections()
        {
            return new List<Direction>
            {
                new Direction { Row = -1, Col = -1 },
                new Direction { Row = -1, Col = 1 },
                new Direction { Row = 1, Col = -1 },
                new Direction { Row = 1, Col = 1 },
                new Direction { Row = -2, Col = -2 }, // Capture moves
                new Direction { Row = -2, Col = 2 },
                new Direction { Row = 2, Col = -2 },
                new Direction { Row = 2, Col = 2 }
            };
        }

        private bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < m_Size && col >= 0 && col < m_Size;
        }

        private class Direction
        {
            public int Row { get; set; }
            public int Col { get; set; }
        }

        private class Move
        {
            public string From { get; set; }
            public string To { get; set; }
        }
    }
}
