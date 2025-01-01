using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Ex02_01
{
    internal class MoveHandler : CheckersBoard
    {
        string m_ChooseVsComPlayer = null;
        public MoveHandler(int size, string name, string chooseVsComPlayer) : base(size, name, chooseVsComPlayer)
        {
        }
        public virtual bool MakeMove(string playerMove, char Turn)
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
            if (!IsTurnPlayer(from, Turn))
            {
                Console.WriteLine("Invalid move! It is not your turn.");
                return false;
            }

            if (!IsValidPosition(from) || !IsValidPosition(to))
            {
                Console.WriteLine("Invalid positions! Please check your input.");
                return false;
            }

            // Convert positions to board indices
            int fromCol = from[1] - 'a';
            int fromRow = from[0] - 'A';
            int toCol = to[1] - 'a';
            int toRow = to[0] - 'A';

            // Validate move indices
            if (!IsMoveAllowed(fromRow, fromCol, toRow, toCol))
            {
                Console.WriteLine("This move is not Allowed");
                return false;
            }
            // ForEach m_Board[i,j] == 'X'
            // string StringPos = "i + 'a' + j + 'A' "
            if(EatOption(from, Turn) == true)
            { // 'קיים צעד אכילה אפשרי יש לבצע צעד אכילה בלבד בשלב זה
                return false;
            }
            // Perform the move
            m_Board[toRow, toCol] = m_Board[fromRow, fromCol];
            m_Board[fromRow, fromCol] = ' ';

            // Handle potential multi-capture
            /*  if (Math.Abs(toRow - fromRow) == 2 && CanContinueCapture(toRow, toCol))
              {
                  Console.WriteLine("You can continue capturing!");
                  DisplayBoard(); // Show updated board for the player's next move
                  return false;  // Player's turn continues
              }*/

            return true; // Turn ends if no more captures are possible
        }

        public string Win_Draw(int player1Points, int player2Points)
        {
            bool player1HasMoves = HasValidMoves('X');
            bool player2HasMoves = HasValidMoves('O');

            if (!player1HasMoves || player1Points == 0)
            {
                return "Player 2 Wins!";
            }
            else if (!player2HasMoves || player2Points == 0)
            {
                return "Player 1 Wins!";
            }
            else if (!player1HasMoves && !player2HasMoves)
            {
                return "Draw!";
            }

            return null; // Game is still ongoing
        }

        private bool HasValidMoves(char playerPiece)
        {
            for (int row = 0; row < m_Size; row++)
            {
                for (int col = 0; col < m_Size; col++)
                {
                    if (m_Board[row, col] == playerPiece)
                    {
                        // Check if the piece has at least one valid move
                        if (HasAnyValidMove(row, col))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool HasAnyValidMove(int row, int col)
        {
            // Check all potential directions for valid moves
            int[][] directions = new int[][]
            {
                new int[] { 1, 1 },   // Down-right
                new int[] { 1, -1 },  // Down-left
                new int[] { -1, 1 },  // Up-right
                new int[] { -1, -1 }  // Up-left
            };

            foreach (var direction in directions)
            {
                int newRow = row + direction[0];
                int newCol = col + direction[1];

                // Check simple move
                if (newRow >= 0 && newRow < m_Size && newCol >= 0 && newCol < m_Size && m_Board[newRow, newCol] == ' ')
                {
                    return true;
                }

                // Check capture move
                int captureRow = row + direction[0] * 2;
                int captureCol = col + direction[1] * 2;
                if (captureRow >= 0 && captureRow < m_Size && captureCol >= 0 && captureCol < m_Size &&
                    m_Board[newRow, newCol] != ' ' && m_Board[newRow, newCol] != m_Board[row, col] && m_Board[captureRow, captureCol] == ' ')
                {
                    return true;
                }
            }

            return false;
        }

        protected bool IsValidPosition(string position)
        {
            if (position.Length != 2)
            {
                return false;
            }
            char row = position[0]; // Row is uppercase (A-Z)
            char col = position[1]; // Column is lowercase (a-z)

            return row >= 'A' && row < 'A' + m_Size && col >= 'a' && col < 'a' + m_Size;
        }

        protected bool IsMoveAllowed(int FromRow, int FromCol, int ToRow, int ToCol)
        {
            if (m_Board[FromRow, FromCol] == ' ') // Ensure the starting position has a piece
            {
                return false;
            }

            int rowDiff = Math.Abs(ToRow - FromRow);
            int colDiff = Math.Abs(ToCol - FromCol);

            if (rowDiff == 1 && colDiff == 1 && m_Board[ToRow, ToCol] == ' ') // Simple move
            {
                return true;
            }

            if (rowDiff == 2 && colDiff == 2) // Capture move
            {
                int captureRow = (FromRow + ToRow) / 2;
                int captureCol = (FromCol + ToCol) / 2;
                if (m_Board[captureRow, captureCol] != ' ' &&
                    m_Board[captureRow, captureCol] != m_Board[FromRow, FromCol] && m_Board[ToRow, ToCol] == ' ')
                {
                    m_Board[captureRow, captureCol] = ' '; // Clear captured piece
                    return true;
                }
            }
            return false;
        }

        protected bool IsTurnPlayer(string MovePosition, char Turn)
        {
            bool Check = false;
            int Row = MovePosition[0] - 'A'; // Adjusted for row (uppercase)
            int Col = MovePosition[1] - 'a'; // Adjusted for column (lowercase)

            char piece = m_Board[Row, Col];
            if (piece == 'O' && Turn == 'X')
            {
                Console.WriteLine("It is X's turn. O cannot move now.");
            }
            else if (piece == 'X' && Turn == 'O')
            {
                Console.WriteLine("It is O's turn. X cannot move now.");

            }
            else { Check = true; }
            return Check;
        }

        public bool EatOption(string CurrentPosition, char turn)
        {

            int RowPosition = CurrentPosition[0] - 'A';
            int ColPosition = CurrentPosition[1] - 'a';


            bool ValidToEat = false;


            if (turn == 'X' || turn == 'K')
            {
                bool Xleft = BorderEatChecking(RowPosition - 2, ColPosition - 2);
                bool Xright = BorderEatChecking(RowPosition - 2, ColPosition + 2);
                if (Xleft)
                {
                    if (m_Board[RowPosition - 1, ColPosition - 1] == 'O' ||
                        m_Board[RowPosition - 1, ColPosition - 1] == 'U')
                    {
                        if (m_Board[RowPosition - 2, ColPosition - 2] == ' ')
                        {
                            ValidToEat = true;
                        }
                    }
                }
                if (Xright)
                {
                    if (m_Board[RowPosition - 1, ColPosition + 1] == 'O' ||
                         m_Board[RowPosition - 1, ColPosition + 1] == 'U')
                    {
                        if (m_Board[RowPosition - 1, ColPosition + 1] == ' ')
                            ValidToEat = true;
                    }
                }
            }
            else // O Turn if (turn == 'O' || turn == 'U')
            {
                bool Oright = BorderEatChecking(RowPosition + 2, ColPosition + 2);
                bool Oleft = BorderEatChecking(RowPosition + 2, ColPosition - 2);
                if (Oright)
                {
                    if (m_Board[RowPosition + 1, ColPosition + 1] == 'X' ||
                        m_Board[RowPosition + 1, ColPosition + 1] == 'K')
                    {
                        if (m_Board[RowPosition + 2, ColPosition + 2] == ' ')
                        {
                            ValidToEat = true;
                        }
                    }
                }
                if (Oleft)
                {
                    if (m_Board[RowPosition + 1, ColPosition - 1] == 'X' ||
                        m_Board[RowPosition + 1, ColPosition - 1] == 'K')
                    {
                        if (m_Board[RowPosition + 2, ColPosition - 2] == ' ')
                        {
                            ValidToEat = true;
                        }
                    }

                }

            }
            return ValidToEat;
        }
                
        private bool BorderEatChecking(int RowPosition,int ColPosition)
        {
            return RowPosition >= 0 && RowPosition < m_Size && 
                   ColPosition >= 0 && ColPosition < m_Size;
            
        }
    }

}


/*        protected bool CanContinueCapture(int row, int col)
          {
              // Check all possible capture directions
              int[][] directions = new int[][]
              {
                  new int[] { 2, 2 },  // Down-right
                  new int[] { 2, -2 }, // Down-left
                  new int[] { -2, 2 }, // Up-right
                  new int[] { -2, -2 } // Up-left
              };

              foreach (var direction in directions)
              {
                  int newRow = row + direction[0];
                  int newCol = col + direction[1];
                  int captureRow = (row + newRow) / 2;
                  int captureCol = (col + newCol) / 2;

                  // Check if the move is a valid capture
                  if (newRow >= 0 && newRow < m_Size &&
                      newCol >= 0 && newCol < m_Size &&
                      m_Board[captureRow, captureCol] != ' ' &&
                      m_Board[captureRow, captureCol] != m_Board[row, col] &&
                      m_Board[newRow, newCol] == ' ')
                  {
                      return true; // A further capture is possible
                  }
              }

              return false; // No further captures possible
          }*/