using Console_Checkers;

class Program
{
    public static void WorkingWriteLine(string Message = "") //Write Line that does not bleed over
    {
        ConsoleColor temp = Console.BackgroundColor;
        Console.Write(Message);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine("");
        Console.BackgroundColor = temp;
    }

    public static void VisualizeGame(short[] GameState) //top left and other red tiles are the empty ones
    {
        const int TileSize = 6;
        const int HalfTheAmountOfTiles = 4;
        Console.Clear();
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
                    if (j == 0 || j == TileSize - 1) //bottom or top of the square
                    {
                        for (int k = 0; k < HalfTheAmountOfTiles; k++)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            for (int l = 0; l < TileSize; l++)
                            {
                                Console.Write("  ");
                            }
                            Console.BackgroundColor = ConsoleColor.White;
                            for (int l = 0; l < TileSize; l++)
                            {
                                Console.Write("  ");
                            }

                        }
                    }
                    else
                    {
                        for (int k = 0; k < HalfTheAmountOfTiles; k++)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write("  ");
                            bool Middle = false;
                            if ((TileSize % 2 == 0 && (j == TileSize / 2 || j == (TileSize / 2) + 1)) || (TileSize % 2 == 1 && j == (TileSize / 2) + 1)) //middle tiles
                            {
                                Middle = true;
                            }
                            if (!Middle)
                            {
                                Console.Write("  ");
                            }
                            #region TheChecker
                            TileType Curr = (TileType)((GameState[i] >> (k * 3)) & 7);
                            if (Curr.HasFlag(TileType.Exists))
                            {
                                if (Curr.HasFlag(TileType.Red))
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            if (Middle)
                            {
                                for (int l = 0; l < TileSize - 2; l++)
                                {
                                    Console.Write("  ");
                                }
                            }
                            else
                            {
                                for (int l = 0; l < TileSize - 4; l++)
                                {
                                    Console.Write("  ");
                                }
                            }
                            #endregion
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write("  ");
                            if (!Middle)
                            {
                                Console.Write("  ");
                            }
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("  ");
                    WorkingWriteLine();
                }
            }
            else
            {
                WorkingWriteLine();
                WorkingWriteLine();
                WorkingWriteLine();
                WorkingWriteLine();
                WorkingWriteLine();
                WorkingWriteLine();
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

        short[] GameState = { 0, 0, 0, 0, 0, 0, 0, 0 };
        VisualizeGame(GameState);
        Console.ReadKey();
    }
}