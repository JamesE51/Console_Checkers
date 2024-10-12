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

    [Flags]
    public enum Directions
    {
        Up = 0,
        Down = 1,
        Left = 2,

        UpRight = Up,
        UpLeft = Up | Left,

        DownRight = Down,
        DownLeft = Down | Left,
    }


    internal class CheckerBoard
    {
        public ushort[] Board = new ushort[8]; //each 3 bits is a peice First: 0 is empty, 1 is not. Second: 0 is Black, 1 is Red, Third: 0 is Normal, 1 is King
        public CheckerBoard(ushort[] board)
        {
            Board = board;
        }
        public List<ushort[]> GetNextMoveLocations(bool PlayerTurn)
        {
            List<(ushort[] Board, bool Jumped)> PossibleBoardStates = [];
            bool MustJump = false;
            
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < 12; j += 3)
                {
                    List<Directions> PossibleMoves = [];
                    Point CurrPoint = new Point();
                    CurrPoint.Y = i;
                    CurrPoint.X = j;
                    TileType CurrTile = (TileType)((Board[i] >> j) & 7);
                    #region Skipping Bad Cases
                    if (CurrTile.Equals(TileType.None)) //skips tile if tile is empty
                    {
                        continue;
                    }
                    if (CurrTile.HasFlag(TileType.Player))
                    {
                        if (!PlayerTurn) //if curr tile is player tile and not players turn
                        {
                            continue;
                        }
                    }
                    else if (PlayerTurn) //if curr tile is enemy tile and it's player turn
                    {
                        continue;
                    }
                    #endregion
                    if (CurrTile.HasFlag(TileType.Player) || CurrTile.HasFlag(TileType.King))
                    {
                        PossibleMoves.Add(Directions.UpLeft);
                        PossibleMoves.Add(Directions.UpRight);
                    }
                    if(!CurrTile.HasFlag(TileType.Player) || CurrTile.HasFlag(TileType.King))
                    {
                        PossibleMoves.Add(Directions.DownLeft);
                        PossibleMoves.Add(Directions.DownRight);
                    }
                    #region Removing Directions Depending On Wether It's On An Edge
                    if (CurrPoint.Y == 0) //if its the top
                    {
                        PossibleMoves.RemoveAll(x => !x.HasFlag(Directions.Down));
                    }
                    else if (CurrPoint.Y == 7) //if its the bottom
                    { 
                        PossibleMoves.RemoveAll(x => x.HasFlag(Directions.Down));
                    }
                    if (CurrPoint.X == 0) //leftmost
                    {
                        PossibleMoves.RemoveAll(x => x.HasFlag(Directions.Left));
                    }
                    else if (CurrPoint.X == 12) //rightmost
                    {
                        PossibleMoves.RemoveAll(x => !x.HasFlag(Directions.Left));
                    }
                    #endregion
                    
                    for (int k = 0; k < PossibleMoves.Count; k++)
                    {
                        Point Shift = new Point();
                        #region Getting Shifting Needed For Piece
                        if (PossibleMoves[k].HasFlag(Directions.Down))
                        {
                            Shift.Y = CurrPoint.Y + 1;
                        }
                        else
                        {
                            Shift.Y = CurrPoint.Y - 1;
                        }
                        if (PossibleMoves[k].HasFlag(Directions.Left))
                        {
                            Shift.X = CurrPoint.X - 3;
                        }
                        else
                        {
                            Shift.X = CurrPoint.X + 3;
                        }
                        #endregion
                        TileType OtherTile = (TileType)((Board[Shift.Y] >> Shift.X) & 7);
                        if (OtherTile.Equals(TileType.None))
                        {
                            if (MustJump)
                            {
                                continue;
                            }
                            else
                            {
                                ushort[] TempBoardState = Board.ToArray();
                                TempBoardState[CurrPoint.Y] = (ushort)(TempBoardState[CurrPoint.Y] & (~(int)(Math.Pow(2, Shift.X) + Math.Pow(2, Shift.X + 1) + Math.Pow(2, Shift.X + 2)))); //remove the checker
                                TempBoardState[Shift.Y] = (ushort)(TempBoardState[Shift.Y] | (int)OtherTile << Shift.X);
                                PossibleBoardStates.Add((TempBoardState, false));
                            }
                        }
                    }
                }
            }
            //checking possible board states
            #region Returning Board States But Filtering If Jump Is Forced
            List<ushort[]> SurvivingBoardStates = [];
            for (int i = 0; i < PossibleBoardStates.Count; i++)
            {
                if (PossibleBoardStates[i].Jumped == false && MustJump)
                {
                    continue;
                }
                SurvivingBoardStates.Add(PossibleBoardStates[i].Board);
            }
            return SurvivingBoardStates;
            #endregion
        }
        /*
        public List<int[]> CheckPossibleMoves (ushort[] Board, Point CurrPos, List<Directions> Movements)
        {

        }

        public List<int[]> RecursiveJumping(ushort[] Board, Point CurrPos, TileType Curr)
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
        }*/


    }
   
}
