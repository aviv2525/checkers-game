using Ex02_01;
using System;

internal class Program
{
    static void Main(string[] args)
    {
        string name, Name2 = null;

        // Enter name
        Console.WriteLine("Enter your name (Maximum 20 characters, no spaces, or press 'Q' to quit):");
        name = Console.ReadLine();
        if (CheckQuit(name)) return;

        while (name.Length > 20 || name.Contains(" "))
        {
            Console.WriteLine("Invalid Name! Enter your name (Maximum 20 characters, no spaces, or press 'Q' to quit):");
            name = Console.ReadLine();
            if (CheckQuit(name)) return;
        }

        // Choose opponent
        Console.WriteLine("Choose opponent: 1 for Computer, 2 for Player (or press 'Q' to quit):");
        string opponentChoice = Console.ReadLine();
        if (CheckQuit(opponentChoice)) return;

        while (opponentChoice != "1" && opponentChoice != "2")
        {
            Console.WriteLine("Invalid Input! Enter 1 for Computer, 2 for Player (or press 'Q' to quit):");
            opponentChoice = Console.ReadLine();
            if (CheckQuit(opponentChoice)) return;
        }

        if (opponentChoice == "2")
        {
            Console.WriteLine("Enter Second Player name (or press 'Q' to quit):");
            Name2 = Console.ReadLine();
            if (CheckQuit(Name2)) return;
        }

        // Enter board size
        Console.WriteLine("Enter board size (6, 8, or 10, or press 'Q' to quit):");
        string boardSizeInput = Console.ReadLine();
        if (CheckQuit(boardSizeInput)) return;

        int boardSize;
        while (!int.TryParse(boardSizeInput, out boardSize) || (boardSize != 6 && boardSize != 8 && boardSize != 10))
        {
            Console.WriteLine("Invalid Input! Enter 6, 8, or 10 (or press 'Q' to quit):");
            boardSizeInput = Console.ReadLine();
            if (CheckQuit(boardSizeInput)) return;
        }

        // Initialize GameManager and ComputerPlayer
        char Turn = 'X';
        MoveHandler Player1 = new MoveHandler(boardSize, name, opponentChoice);
        //MoveHandler secondPlayer = new MoveHandler(boardSize, Name2, opponentChoice);
        //ComputerPlayer computerPlayer = new ComputerPlayer(boardSize, "Computer", opponentChoice);
        Player1.DisplayBoard();

        while (true)
        {
            // Human player's turn
            Console.WriteLine($"{name}, it's your turn! Enter your move in the format RowCol>RowCol (e.g., Bc>Cd), or press 'Q' to quit:");
            string move = Console.ReadLine();
            if (CheckQuit(move))
            {
                Console.WriteLine("Game ended by user.");
                break;
            }

            while (!Player1.MakeMove(move, Turn))
            {
                move = Console.ReadLine();
            }
            //player.Eats()
            // while  (player.Eats){Console.ReadLine()}
            Console.Clear();
            Player1.DisplayBoard();

            if (opponentChoice == "2")
            {
                Turn = 'O';
                Console.WriteLine($"{Name2}, it's your turn! Enter your move in the format RowCol>RowCol (e.g., Bc>Cd), or press 'Q' to quit:");
                move = Console.ReadLine();
                if (CheckQuit(move))
                {
                    Console.WriteLine("Game ended by user.");
                    break;
                }
                while (!Player1.MakeMove(move, Turn))
                {
                    Console.WriteLine("invallid move try again (Have to eat if you can)");
                    move = Console.ReadLine();
                }
                Console.Clear();
                Player1.DisplayBoard();
                Turn = 'X';
            }

            // Check if it's against the computer and make the computer's move
            /* if (opponentChoice == "1")
             {
                 bool ExeutedMove = true;
                 Console.WriteLine("Computer's turn...");
                 computerPlayer.MakeMove(ref ExeutedMove);
                 if (!ExeutedMove)
                 {
                     Console.WriteLine("Computer has no valid moves left. Game over!");
                     break;
                 }
                 Console.Clear();
                 Player1.DisplayBoard();
             }*/
        }
    }

    static string GetCurrentPlayer(bool Turn, string player1Name, string player2Name)
    {
        return Turn ? player1Name : player2Name;
    }
    static bool CheckQuit(string input)
    {
        return input?.Equals("Q", StringComparison.OrdinalIgnoreCase) == true;
    }

}
