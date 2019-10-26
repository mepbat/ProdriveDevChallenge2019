using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleAI_CS
{
    class Program
    {
        public static int[,] grid;
        public static int gridSize;
        static void Main(string[] args)
        {
            gridSize = 11;
            var rng = new Random();

            //read which player are we
            int player = int.Parse(Console.ReadLine());

            //new empty board 11x11
            grid = new int[gridSize, gridSize];

            //game
            for (int moveCount = 0; moveCount < (gridSize * gridSize); ++moveCount)
            {
                //if we player 2 - even moves, player 1 - uneven
                if ((moveCount % 2) == player)
                {
                    //FIRST TURN
                    if (moveCount < 2)
                    {
                        //take the middle point
                        int x = 5;
                        int y = 5;
                        //if it is not taken
                        if (grid[x, y] == 0)
                        {
                            grid[x, y] = 1;
                            Console.WriteLine($"{x} {y}");
                        } else
                        {
                            if (grid[x - 1, y] == 0)
                                grid[x - 1, y] = 1;
                            Console.WriteLine($"{x - 1} {y}");
                        }
                    } else
                    //if it's not first turn
                    {
                        //MY FUNCTION

                        int[] move = trySmartGreedy();
                        if (move[0] == -1 && move[1] == -1)
                        {
                            move = doSneakyMove();
                            if (move[0] == -1 && move[1] == -1)
                            {
                                move = doSomeMove();
                                grid[move[0], move[1]] = 1;

                                //write coordinates of move
                                Console.WriteLine($"{move[0]} {move[1]}");
                            } else
                            {
                                grid[move[0], move[1]] = 1;

                                //write coordinates of move
                                Console.WriteLine($"{move[0]} {move[1]}");
                            }
                        }
                        else
                        {
                            grid[move[0], move[1]] = 1;
                            //write coordinates of move
                            Console.WriteLine($"{move[0]} {move[1]}");
                        }
                        
                    }

                 

                }
                //if not our move
                else
                {
                    //read another player's move
                    var c = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    //set that grid place is taken by other player
                    grid[c[0], c[1]] = 2;
                }
            }
        }

        public static int[] trySmartGreedy()
        {
            int[] move = new int[2];
            move[0] = -1;
            move[1] = -1;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        //if there's no wall above
                        if (i > 0)
                        {
                            //try row upper                                   
                            if (grid[i - 1, j] == 0)
                            {
                                int NO = 0;
                                //if there's row above possible point
                                if(i - 1 > 0)
                                {
                                    //if there's column to the left to possible point
                                    if(j > 0)
                                    {
                                        //check if it forms square
                                        if(grid[i - 2, j] == 1 && grid[i - 2, j - 1] == 1 && grid[i - 1, j - 1] == 1)
                                        {
                                            NO++;
                                        }
                                    } 

                                    //if there is column to the right and row above
                                    if (j < gridSize - 1)
                                    {
                                        //check if it forms square
                                        if (grid[i - 2, j] == 1 && grid[i - 2, j + 1] == 1 && grid[i - 1, j + 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }
                                }

                                //if there's row below possible point
                                //there always is cause there's our initial point
                                    //if there's column to the left to possible point
                                    if (j > 0)
                                    {
                                        //check if it forms square
                                        if (grid[i - 1, j - 1] == 1 && grid[i, j] == 1 && grid[i, j - 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }

                                    //if there is column to the right and row below
                                    if (j < gridSize - 1)
                                    {
                                        if (grid[i, j] == 1 && grid[i, j + 1] == 1 && grid[i - 1, j + 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }

                                //if no == 0 means there're no squares formed
                                if (NO == 0)
                                {
                                    move[0] = i - 1;
                                    move[1] = j;
                                    return move;
                                }
                            }
                        }

                        //if there's no wall on the left
                        if (j > 0)
                        {
                            //try row on the left
                            if (grid[i, j - 1] == 0)
                            {
                                int NO = 0;
                                //if there's row above possible point
                                if (i > 0)
                                {
                                    //if there's column to the left to possible point
                                    if (j > 1)
                                    {
                                        //check if it forms square
                                        if (grid[i, j - 2] == 1 && grid[i - 1, j - 2] == 1 && grid[i - 1, j - 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }

                                    //if there is column to the right and row above
                                    //of course there is
                                    if (j - 1 < gridSize - 1)
                                    {
                                        if (grid[i, j] == 1 && grid[i - 1, j - 1] == 1 && grid[i - 1, j] == 1)
                                        {
                                            NO++;
                                        }
                                    }
                                }

                                //if there's row below possible point
                                if (i < gridSize - 1)
                                {
                                    //if there's column to the left to possible point
                                    if (j > 1)
                                    {
                                        //check if it forms square
                                        if (grid[i, j - 2] == 1 && grid[i + 1, j - 1] == 1 && grid[i + 1, j - 2] == 1)
                                        {
                                            NO++;
                                        }
                                    }

                                    //if there is column to the right and row below
                                    if (j - 1 < gridSize - 1)
                                    {
                                        if (grid[i, j] == 1 && grid[i + 1, j] == 1 && grid[i + 1, j - 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }
                                }
                                //if no == 0 means there're no squares formed
                                if (NO == 0)
                                {
                                    move[0] = i;
                                    move[1] = j - 1;
                                    return move;
                                }
                            }
                        }

                        //if there's no wall below
                        if (i < gridSize - 1)
                        {
                            //try row lower
                            if (grid[i + 1, j] == 0)
                            {
                                int NO = 0;
                                //if there's row above possible point
                                if (i + 1 > 0)
                                {
                                    //if there's column to the left to possible point
                                    if (j > 0)
                                    {
                                        //check if it forms square
                                        if (grid[i, j] == 1 && grid[i, j - 1] == 1 && grid[i + 1, j - 1] == 1)
                                        {
                                            NO++;
                                        }
                                    } 

                                    //if there is column to the right and row above
                                    if (j < gridSize - 1)
                                    {
                                        if (grid[i, j] == 1 && grid[i + 1, j + 1] == 1 && grid[i, j + 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }
                                }

                                //if there's row below possible point
                                if (i + 1 < gridSize - 1)
                                {
                                    //if there's column to the left to possible point
                                    if (j > 1)
                                    {
                                        //check if it forms square
                                        if (grid[i + 1, j - 1] == 1 && grid[i + 2, j - 1] == 1 && grid[i + 2, j] == 1)
                                        {
                                            NO++;
                                        }
                                    }

                                    //if there is column to the right
                                    if (j < gridSize - 1)
                                    {
                                        if (grid[i + 2, j] == 1 && grid[i + 2, j + 1] == 1 && grid[i + 1, j + 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }
                                }
                                //if no == 0 means there're no squares formed
                                if (NO == 0)
                                {
                                    move[0] = i + 1;
                                    move[1] = j;
                                    return move;
                                }
                            }
                        }

                        //if there's no wall on the right
                        if (j < gridSize - 1)
                        {
                            //try row on the right
                            if (grid[i, j + 1] == 0)
                            {
                                int NO = 0;
                                //if there's row above possible point
                                if (i > 0)
                                {
                                    //if there's column to the left to possible point
                                    if (j + 1 > 0)
                                    {
                                        //check if it forms square
                                        if (grid[i - 1, j] == 1 && grid[i - 1, j + 1] == 1 && grid[i, j] == 1)
                                        {
                                            NO++;
                                        }
                                    }

                                    //if there is column to the right and row above
                                    if (j + 1 < gridSize - 1)
                                    {
                                        if (grid[i, j + 2] == 1 && grid[i - 1, j + 1] == 1 && grid[i - 1, j + 2] == 1)
                                        {
                                            NO++;
                                        }
                                    }
                                }

                                //if there's row below possible point
                                if (i < gridSize - 1)
                                {
                                    //if there's column to the left to possible point
                                    if (j + 1 > 0)
                                    {
                                        //check if it forms square
                                        if (grid[i, j] == 1 && grid[i + 1, j] == 1 && grid[i + 1, j + 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }

                                    //if there is column to the right
                                    if (j + 1 < gridSize - 1)
                                    {
                                        if (grid[i, j + 2] == 1 && grid[i + 1, j + 2] == 1 && grid[i + 1, j + 1] == 1)
                                        {
                                            NO++;
                                        }
                                    }
                                }
                                //if no == 0 means there're no squares formed
                                if (NO == 0)
                                {
                                    move[0] = i;
                                    move[1] = j + 1;
                                    return move;
                                }

                            }
                        }
                    }

                }
            }
            return move;
        }
        public static int[] stupidGreedy()
        {
            int[] move = new int[2];
            move[0] = -1;
            move[1] = -1;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        //if there's no wall above
                        if (i > 0)
                        {
                            //try row upper                                   
                            if (grid[i - 1, j] == 0)
                            {
                                move[0] = i - 1;
                                move[1] = j;
                                return move;
                            }
                        }
                        //if there's no wall on the left
                        if (j > 0)
                        {
                            //try row on the left
                            if (grid[i, j - 1] == 0)
                            {
                                move[0] = i;
                                move[1] = j - 1;
                                return move;
                            }
                        }

                        //if there's no wall below
                        if (i < gridSize - 1)
                        {
                            //try row lower
                            if (grid[i + 1, j] == 0)
                            {
                                move[0] = i + 1;
                                move[1] = j;
                                return move;
                            }
                        }

                        //if there's no wall on the right
                        if (j < gridSize - 1)
                        {
                            //try row on the right
                            if (grid[i, j + 1] == 0)
                            {
                                move[0] = i;
                                move[1] = j + 1;
                                return move;
                            }
                        }

                    }

                }
            }

            return move;
        }

        public static int[] doSneakyMove()
        {
            int[] move = new int[2];
            move[0] = -1;
            move[1] = -1;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (i > 0)
                    {
                        if (grid[i - 1, j] == 2 && grid[i, j] == 0)
                        {
                            move[0] = i;
                            move[1] = j;
                            return move;
                        }
                    }
                    
                    if (j > 0)
                    {
                        if (grid[i, j - 1] == 2 && grid[i, j] == 0)
                        {
                            move[0] = i;
                            move[1] = j;
                            return move;
                        }
                    }

                    if (j < gridSize - 1)
                    {
                        if (grid[i, j + 1] == 2 && grid[i, j] == 0)
                        {
                            move[0] = i;
                            move[1] = j;
                            return move;
                        }
                    }

                    if (i < gridSize - 1)
                    {
                        if (grid[i + 1, j] == 2 && grid[i, j] == 0)
                        {
                            move[0] = i;
                            move[1] = j;
                            return move;
                        }
                    }
                }
            }
            return move;
        }

        public static int[] doSomeMove()
        {
            int[] move = new int[2];
            move[0] = -1;
            move[1] = -1;
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        if (grid[i, j] == 0)
                        {
                            move[0] = i;
                            move[1] = j;
                            return move;
                        }
                    }
                }
            return move;
        }

        
    }
}
