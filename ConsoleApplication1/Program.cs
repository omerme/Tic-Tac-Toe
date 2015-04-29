using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string wanttoplay = "yes";
            int playerpoints = 0;
            int computerpoints = 0;
            Console.WriteLine("Welcome to Tic Tac Toe \n In your turn, enter the line number and the column number you wish to play");
            /*add
            char[,] board = new char[3,3]{{'X','-','-'},
                                          {'-','0','-'},
                                          {'-','-','-'}};
            Console.WriteLine(Minimax(board, 1, 10, 'X'));
            Console.ReadKey();
            add*/
            while (wanttoplay.ToLower() != "no")
            {
                int gameresult = Game();
                if (gameresult == 1)
                {
                    Console.WriteLine("The computer won!!");
                    computerpoints++;
                }
                else if (gameresult == -1)
                {
                    Console.WriteLine("You won!!");
                    playerpoints++;
                }
                WhoLeads(playerpoints, computerpoints);
                Console.WriteLine("Do you want to continue?");
                wanttoplay = Console.ReadLine();
            }

        }

        static double Minimax(char[,] state, int player, int depth, char playerinput)
        {//gameover, statevalue, nextstates,
            if (depth == 0 || GameOver(state, player, playerinput) != 2) //GameOver(state, player, playerinput) !=2    added
                return StateValue(state, player, playerinput);
            double maxValue = double.NegativeInfinity;


            foreach (char[,] nextState in NextStates(state, playerinput))
            {
                //if (depth == 10)
                //    Console.WriteLine();
                double nextValue = -Minimax(nextState, -player, depth - 1, SecPlayerInput(playerinput));
                if (nextValue > maxValue)
                    maxValue = nextValue;
            }

            if (double.IsNegativeInfinity(maxValue))
                return StateValue(state, player, playerinput);
            return maxValue;
        }

        private static List<char[,]> NextStates(char[,] state, char playerinput)
            //playerinput (char) changed from player (int)
        {
            List<char[,]> result = new List<char[,]>();
            for (int i=0; i < state.GetLength(0); i++  )
            {
                for (int j=0; j < state.GetLength(1); j++)
                {

                    if (state[i,j] == '-')
                    {
                        char[,] tempstate = (char[,])state.Clone();
                        tempstate[i,j] = playerinput;
                        result.Add(tempstate);
                    }
                }
            }
            return result;
        }

        private static double StateValue(char[,] state, int player, char playerinput)
        {
            //throw new NotImplementedException();
            double value = 0;
            int[,] winarray = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            for (int i = 0; i < winarray.GetLength(0); i++)
            {
                if (state[winarray[i, 0] / 3, winarray[i, 0] % 3] == playerinput && state[winarray[i, 1] / 3, winarray[i, 1] % 3] == playerinput && state[winarray[i, 2] / 3, winarray[i, 2] % 3] == playerinput)
                    value = 100;
                else if (state[winarray[i, 0] / 3, winarray[i, 0] % 3] == SecPlayerInput(playerinput) && state[winarray[i, 1] / 3, winarray[i, 1] % 3] == SecPlayerInput(playerinput) && state[winarray[i, 2] / 3, winarray[i, 2] % 3] == SecPlayerInput(playerinput))
                    value = -100;
            }
            return value;
        }
        static char[,] AddXO(char[,] board, int line, int column, char playerinput)
        {
            if (board[line-1,column-1] == '-')
            {
                board[line-1,column-1] = playerinput;

            }
            return board;
        }
        static void PrintBoard(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void WhoLeads(int playerpoints, int computerpoints)
        {
            if (playerpoints > computerpoints)
                Console.WriteLine(playerpoints + " - " + computerpoints + " You lead!");
            else if (playerpoints == computerpoints)
                Console.WriteLine(playerpoints + " - " + computerpoints + " It's a tie!");
            else
                Console.WriteLine(playerpoints + " - " + computerpoints + " The computer leads!");
        }
        static char SecPlayerInput(char playerinput)
        {
            if (playerinput == 'X')
                return 'O';
            return 'X';
        }
        static int GameOver(char[,] board, int player, char playerinput)
        {
            if (StateValue(board, player, playerinput) == 0)
            {
                if (IsFull(board))
                    return 0;
                return 2;
            }
            else if (Convert.ToInt32(StateValue(board, player, playerinput)) >= 100)
                return 1;
            else
                return -1;
        }
        static bool IsFull (char[,] board)
        {
            foreach (char i in board)
            {
                if (i == '-')
                    return false;
            }
            return true;
        }
        static int Game()
        {
            int player;
            int turncounter = 0;
            char playerinput = 'X';
            Console.WriteLine("Do you want to start?");
            string wanttostart = Console.ReadLine();
            if (wanttostart.ToLower() != "no")
                player = -1;
            else
                player = 1;
     
                char[,] board = new char[3,3]{{'-','-','-'},{'-','-','-'},{'-','-','-'}};
                PrintBoard(board);
                do
                {
                    if (player == -1)
                        board = PlayerTurn(board, playerinput);
                    else
                        board = ComputerTurn(board, playerinput, turncounter);
                    PrintBoard(board);
                    turncounter++;
                    playerinput = SecPlayerInput(playerinput);
                    player = -player;
                } while (GameOver(board, player, playerinput) == 2);

                return GameOver(board, -player, SecPlayerInput(playerinput));
        }
        static char[,] PlayerTurn(char[,] board, char playerinput)
        {
            while (true)
            {
                Console.WriteLine("enter row");
                int row = int.Parse(Console.ReadLine());
                Console.WriteLine("enter column");
                int column = int.Parse(Console.ReadLine());
                if (row <= 3 && column<= 3&& row>=1&&column>=1)
                {
                    if (board[row - 1, column - 1] == '-')
                    {
                        return AddXO(board, row, column, playerinput);
                    }
                }
                Console.WriteLine("Learn to play... Please enter row and col again");
            }
        }
        static char[,] ComputerTurn(char[,] board, char playerinput, int turncounter)
        {
            Console.WriteLine("Computer's turn:");
            double idialscore = double.PositiveInfinity;
            char[,] idialstate = null;
            foreach(char[,] nextstate in NextStates(board, playerinput))
            {
                if (Minimax(nextstate, -1, 4, SecPlayerInput(playerinput)) < idialscore)
                {
                    idialscore = Minimax(nextstate, -1, 4,SecPlayerInput(playerinput));
                    idialstate = nextstate;
                }
                //if (Minimax(nextstate, -1, 4, SecPlayerInput(playerinput)) = idialscore)
                //{
                //    in
                //    if(
                //}
            }
            return idialstate;
        }

    }
}
