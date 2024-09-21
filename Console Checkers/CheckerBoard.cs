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

        HasChecker = 1,
        Player = 2,
        King = 4,

        ComPawn = HasChecker,
        PlayerPawn = HasChecker | Player,

        ComKing = ComPawn | King,
        PlayerKing = PlayerPawn | King
    }
    
    internal class CheckerBoard
    {
        public int[] Rows = new int[8]; //each 3 bits is a peice First: 0 is empty, 1 is not. Second: 0 is Black, 1 is Red, Third: 0 is Normal, 1 is King
        public CheckerBoard(int[] rows)
        {
            Rows = rows;
        }
        public List<int[]> GetNextMoves(int[] Board, bool PlayerTurn)
        {
            List<(int[], bool Jumped)> PossibleBoardStates = new();
            bool MustJump = false;
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    if (j == 0) //not the leftmost
                    {
                        if (!MustJump)
                        {

                        }
                    }
                    if (j == 9) //not the rightmos
                    {
                        if (!MustJump)
                        {

                        }
                    }
                }
            }

            return null;
        }

        public List<int[]> RecursiveJumping(int[] Board, Point CurrPos, TileType Curr)
        {
            //setting what opposing checker it will look for
            TileType Opp = TileType.Player;
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
   
}
