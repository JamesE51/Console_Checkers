using Console_Checkers;
using System.Drawing;

class Program
{
    public static void WorkingClear()
    {
        ConsoleColor SavedColor = Console.BackgroundColor;
        Console.BackgroundColor = ConsoleColor.Black;
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 31; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine("\x1b[3J"); //thanks stack overflow https://stackoverflow.com/questions/75471607/console-clear-doesnt-clean-up-the-whole-console
        }
        Console.SetCursorPosition(0, 0);
        Console.BackgroundColor = SavedColor;
    }
   
    public static void WorkingWriteLine(string text = "")
    {
        ConsoleColor temp = Console.BackgroundColor;
        Console.Write(text);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine();
        Console.BackgroundColor = temp;
    }

    public static void VisualizeGame(ushort[] GameState) //top left and other red tiles are the empty ones
    {
        ConsoleColor PlayerColor = ConsoleColor.Red;
        ConsoleColor ComColor = ConsoleColor.Blue;
        ConsoleColor TileColorOne = ConsoleColor.Black;
        ConsoleColor TileColorTwo = ConsoleColor.White;
        ConsoleColor KingColor = ConsoleColor.Gray;
        const int TileSize = 6;
        const int HalfTheAmountOfTiles = 4;

        #region Top Border
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.Write("  ");
        for (int i = 0; i < HalfTheAmountOfTiles * 2 * TileSize; i++)
        {
            Console.Write("  ");
        }
        Console.Write("  ");
        WorkingWriteLine();
        #endregion
        for (int i = 0; i < GameState.Length; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < TileSize; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("  "); //left side of border
                    #region Top Or Bottom of Checkerboard
                    if (j == 0 || j == TileSize - 1) //bottom or top of the square
                    {
                        for (int k = 0; k < HalfTheAmountOfTiles; k++)
                        {
                            Console.BackgroundColor = TileColorOne;
                            for (int l = 0; l < TileSize; l++)
                            {
                                Console.Write("  ");
                            }
                            Console.BackgroundColor = TileColorTwo;
                            for (int l = 0; l < TileSize; l++)
                            {
                                Console.Write("  ");
                            }

                        }
                    }
                    #endregion
                    else
                    {
                        #region Middle Sections
                        for (int k = 0; k < HalfTheAmountOfTiles; k++)
                        {
                            Console.BackgroundColor = TileColorOne;
                            Console.Write("  ");
                            bool Middle = false;
                            if ((TileSize % 2 == 0 && (j == TileSize / 2 || j == (TileSize / 2) - 1)) || (TileSize % 2 == 1 && j == (TileSize / 2) + 1)) //middle tiles
                            {
                                Middle = true;
                            }
                            TileType Curr = (TileType)((GameState[i] >> (k * 3)) & 7);
                            //Edges of the piece
                            if (Middle)
                            {
                                if (Curr.HasFlag(TileType.HasChecker))
                                {
                                    if (Curr.HasFlag(TileType.Player))
                                    {
                                        Console.BackgroundColor = PlayerColor;
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = ComColor;
                                    }
                                }
                            }
                            ConsoleColor SavedColor = Console.BackgroundColor;
                            Console.Write("  ");

                            if (Curr.HasFlag(TileType.HasChecker))
                            {
                                if (Curr.HasFlag(TileType.Player))
                                {
                                    Console.BackgroundColor = PlayerColor;
                                }
                                else
                                {
                                    Console.BackgroundColor = ComColor;
                                }
                                if (Curr.HasFlag(TileType.King) && Middle)
                                {
                                    Console.BackgroundColor = KingColor;
                                }
                            }
                            Console.Write("    ");
                            Console.BackgroundColor = SavedColor;
                            Console.Write("  ");
                            Console.BackgroundColor = TileColorOne;
                            Console.Write("  ");
                            #region White Sqaures
                            Console.BackgroundColor = TileColorTwo;
                            for (int l = 0; l < TileSize; l++)
                            {
                                Console.Write("  ");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    //right border
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("  ");
                    WorkingWriteLine();
                }
            }
            else
            {
                for (int j = 0; j < TileSize; j++)
                {
                    //left side of border
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("  ");
                    for (int k = 0; k < HalfTheAmountOfTiles; k++)
                    {
                        #region White Tiles
                        Console.BackgroundColor = TileColorTwo;
                        for (int l = 0; l < TileSize; l++)
                        {
                            Console.Write("  ");
                        }
                        #endregion
                        if (j == 0 || j == TileSize - 1) //if top or bottom of tile
                        {
                            Console.BackgroundColor = TileColorOne;
                            for (int l = 0; l < TileSize; l++)
                            {
                                Console.Write("  ");
                            }
                        }
                        else
                        {
                            #region MiddleSections

                            Console.BackgroundColor = TileColorOne;
                            Console.Write("  ");
                            bool Middle = false;
                            if ((TileSize % 2 == 0 && (j == TileSize / 2 || j == (TileSize / 2) - 1)) || (TileSize % 2 == 1 && j == (TileSize / 2) + 1)) //middle tiles
                            {
                                Middle = true;
                            }
                            TileType Curr = (TileType)((GameState[i] >> (k * 3)) & 7);
                            //Edges of the piece
                            if (Middle)
                            {
                                if (Curr.HasFlag(TileType.HasChecker))
                                {
                                    if (Curr.HasFlag(TileType.Player))
                                    {
                                        Console.BackgroundColor = PlayerColor;
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = ComColor;
                                    }
                                }
                            }
                            ConsoleColor SavedColor = Console.BackgroundColor;
                            Console.Write("  ");
                            if (Curr.HasFlag(TileType.HasChecker))
                            {
                                if (Curr.HasFlag(TileType.Player))
                                {
                                    Console.BackgroundColor = PlayerColor;
                                }
                                else
                                {
                                    Console.BackgroundColor = ComColor;
                                }
                                if (Curr.HasFlag(TileType.King) && Middle)
                                {
                                    Console.BackgroundColor = KingColor;
                                }
                            }
                            Console.Write("    ");
                            Console.BackgroundColor = SavedColor;
                            Console.Write("  ");
                            Console.BackgroundColor = TileColorOne;
                            Console.Write("  ");


                            #endregion
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("  ");
                    WorkingWriteLine();


                }
            }
        }
        #region Bottom Border
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.Write("  ");
        for (int i = 0; i < HalfTheAmountOfTiles * 2 * TileSize; i++)
        {
            Console.Write("  ");
        }
        Console.Write("  ");
        #endregion

    }

    public static void Main(string[] args)
    {
        #region Clearin console to avoid line bleeding
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        #endregion


        ushort[] GameState = { 0, 0, 0, 0, 0, 0, 0b_000_000_011_000, 0b_000_000_011_000 };
        CheckerBoard GameBoard = new CheckerBoard(GameState);


        VisualizeGame(GameBoard.Board);

      //  GameBoard.SingleJumping(GameBoard.Board,new Point(3,7),TileType.PlayerPawn);
        
        
        
        List<(ushort[], Point?)> NextBoards = GameBoard.GetNextMoveLocations(true);
        for (int i = 0; i < NextBoards.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                WorkingWriteLine();
            }
            VisualizeGame(NextBoards[i].Item1);
        }

        Console.ReadKey();
    }
}