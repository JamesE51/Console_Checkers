using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Console_Checkers
{
    
    [Flags]
    public enum TileType
    {
        None = 0,

        Exists = 1,
        Red = 2,
        King = 4,

        BlackPawn = Exists,
        RedPawn = Exists | Red,

        BlackKing = BlackPawn | King,
        RedKing = RedPawn | King
    }
    /*
    internal class CheckerBoard
    {

        public int[] Rows = new int[8]; //each 3 bits is a peice First: 0 is empty, 1 is not. Second: 0 is Black, 1 is Red, Third: 0 is Normal, 1 is King
        public CheckerBoard(int[] rows)
        {
            Rows = rows;
        }
        public List<int[]> GetNextMoves(int[] Board, bool RedTurn)
        {
            List<(int[], bool Jumped)> PossibleBoardStates = new();
            bool MustJump = false;
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < 24; j += 3)
                {
                    int desiredBits = Board[i] >> j;
                    TileType currentTile = (TileType)desiredBits;
                    
                    if (currentTile.HasFlag(TileType.None))
                    {
                        continue;
                    }
                    if(currentTile.HasFlag(TileType.Red)) //if it is red
                    {
                        if (!RedTurn)
                        {
                            continue;
                        }
                        if(i != 0) //if it can move upward
                        {
                            if(j != 0) //if it is not the leftmost
                            {
                                TileType NextTile = (TileType)(Board[i-1] >> (j-3));
                                if(NextTile.HasFlag(TileType.None))
                                {
                                    if (!MustJump)
                                    {
                                        int[] TempBoard = Board.ToArray();
                                        TempBoard[i] = TempBoard[i] & ~(7 >> j); //changing current spot to empty
                                        TempBoard[i - 1] = TempBoard[i - 1] | ((int)NextTile >> (j - 3)); //setting 
                                        PossibleBoardStates.Add((TempBoard, false));
                                    }
                                }
                                else if (!NextTile.HasFlag(TileType.Red))
                                {
                                    
                                }
                            }
                            if (j != 21) //if it is not the rightmost
                            {
                                TileType NextTile = (TileType)(Board[i - 1] >> (j + 3));
                                if (NextTile.HasFlag(TileType.None))
                                {
                                    if (!MustJump)
                                    {
                                        int[] TempBoard = Board.ToArray();
                                        TempBoard[i] = TempBoard[i] & ~(7 >> j);
                                        TempBoard[i - 1] = TempBoard[i - 1] | ((int)NextTile >> (j + 3));
                                        PossibleBoardStates.Add((TempBoard, false));
                                    }
                                }
                                else if (!NextTile.HasFlag(TileType.Red))
                                {

                                }
                            }
                        }
                        if(currentTile.HasFlag(TileType.King)) //if it can move downward
                        {
                            if (j != 0) //if it is not the leftmost
                            {
                                TileType NextTile = (TileType)(Board[i + 1] >> (j - 3));
                                if (NextTile.HasFlag(TileType.None))
                                {
                                    if (!MustJump)
                                    {
                                        int[] TempBoard = Board.ToArray();
                                        TempBoard[i] = TempBoard[i] & ~(7 >> j);
                                        TempBoard[i + 1] = TempBoard[i + 1] | ((int)NextTile >> (j - 3));
                                        PossibleBoardStates.Add((TempBoard, false));
                                    }
                                }
                                else if (!NextTile.HasFlag(TileType.Red))
                                {

                                }
                            }
                            if (j != 21) //if it is not the rightmost
                            {
                                TileType NextTile = (TileType)(Board[i + 1] >> (j + 3));
                                if (NextTile.HasFlag(TileType.None))
                                {
                                    if (!MustJump)
                                    {
                                        int[] TempBoard = Board.ToArray();
                                        TempBoard[i] = TempBoard[i] & ~(7 >> j);
                                        TempBoard[i + 1] = TempBoard[i + 1] | ((int)NextTile >> (j + 3));
                                        PossibleBoardStates.Add((TempBoard,false));
                                    }
                                }
                                else if (!NextTile.HasFlag(TileType.Red))
                                {

                                }
                            }
                        }
                    }

                }
            }

            return null;
        }

        public List<int[]> RecursiveJumping(int[] Board, Point CurrPos, TileType Curr)
        {
            //setting what opposing checker it will look for
            TileType Opp = TileType.Red;
            if (Curr.HasFlag(TileType.Red))
            {
                Opp = TileType.Exists;
            }

            //checking nextdoor tiles
            bool HasMove = false;
            //upright (unlike my spine)
            if (CurrPos.Y > 1 && CurrPos.X < 7) //if there's room to jump up and room to the right
            {

            }
            //upleft
            if (CurrPos.Y > 1 && CurrPos.X > 1) //if it has room to jump up and left
            {

            }
            //downright
            if (CurrPos.Y < 7 && CurrPos.X > 1) //if there is room to the down and room to the right
            {

            }
            //downleft
            if (CurrPos.Y < 7 && CurrPos.X < 7)
            {

            }
            //if no moves
            if (!HasMove)
            {

            }
        }


    }
    */
}
