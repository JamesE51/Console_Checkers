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
        /// <summary>
        /// Removes a checker from a row on the board
        /// </summary>
        /// <param name="BoardLine">The affected row on the board</param>
        /// <param name="CurrX">The least significant bit of the (3-bit) checker to remove</param>
        /// <returns>The updated row</returns>
        private static ushort RemoveChecker(ushort BoardLine, int CurrX) => (ushort)(BoardLine & ~(0B_111 << CurrX));

        //private ushort RemoveChecker(ushort BoardLine, int CurrX) => (ushort)(BoardLine & (~(int)(Math.Pow(2, CurrX) + Math.Pow(2, CurrX + 1) + Math.Pow(2, CurrX + 2))));
        
        
        public ushort[] Board = new ushort[8]; //each 3 bits is a peice First: 0 is empty, 1 is not. Second: 0 is Black, 1 is Red, Third: 0 is Normal, 1 is King
        public CheckerBoard(ushort[] board)
        {
            Board = board;
        }

        public List<(ushort[], Point?)> GetNextMoveLocations(bool PlayerTurn)
        {
            List<(ushort[] Board, Point? Jumped)> PossibleBoardStates = [];
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
                    if (!CurrTile.HasFlag(TileType.Player) || CurrTile.HasFlag(TileType.King))
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
                    if (CurrPoint.X == 0 && (CurrPoint.Y % 2 == 0)) //leftmost
                    {
                        PossibleMoves.RemoveAll(x => x.HasFlag(Directions.Left));
                    }
                    else if (CurrPoint.X == 12 && (CurrPoint.Y % 2 == 1)) //rightmost
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
                            if ((CurrPoint.Y - 1) % 2 == 0)
                            {
                                Shift.X = CurrPoint.X;
                            }
                            else
                            {
                                Shift.X = CurrPoint.X - 3;
                            }

                        }
                        else
                        {
                            if ((CurrPoint.Y - 1) % 2 == 1)
                            {
                                Shift.X = CurrPoint.X;
                            }
                            else
                            {
                                Shift.X = CurrPoint.X + 3;
                            }
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
                                TempBoardState[CurrPoint.Y] = RemoveChecker(TempBoardState[CurrPoint.Y], CurrPoint.X);
                                TempBoardState[Shift.Y] = (ushort)(TempBoardState[Shift.Y] | (int)CurrTile << Shift.X);
                                PossibleBoardStates.Add((TempBoardState, null));
                            }
                        }
                        else
                        {
                            if (((int)OtherTile & 0b_010) != ((int)CurrTile & 0b_010))
                            {

                                //checking if can jump
                                Point BehindTilePoint = new();
                                #region Checking if the tile behind exists and getting the tile behind it
                                if (PossibleMoves[k].HasFlag(Directions.Left))
                                {
                                    if (CurrPoint.X == 0) //if it cannot go left
                                    {
                                        continue;
                                    }
                                    BehindTilePoint.X = CurrPoint.X - 3;
                                }
                                else
                                {
                                    if (CurrPoint.X == 12)
                                    {
                                        continue;
                                    }
                                    BehindTilePoint.X = CurrPoint.X + 3;
                                }
                                if (!PossibleMoves[k].HasFlag(Directions.Down))
                                {
                                    if (CurrPoint.Y < 2)
                                    {
                                        continue;
                                    }
                                    BehindTilePoint.Y = CurrPoint.Y - 2;
                                }
                                else
                                {
                                    if (CurrPoint.Y > 5)
                                    {
                                        continue;
                                    }
                                    BehindTilePoint.Y = CurrPoint.Y + 2;
                                }


                                #endregion
                                TileType TileBehindTile = (TileType)((Board[BehindTilePoint.Y] >> BehindTilePoint.X) & 7);
                                if (!TileBehindTile.Equals(TileType.None)) //if there is a checker behind it, it continues
                                {
                                    continue;
                                }
                                #region The Jumping
                                MustJump = true;
                                ushort[] TempBoardState = Board.ToArray();
                                
                                TempBoardState[CurrPoint.Y] = RemoveChecker(TempBoardState[CurrPoint.Y], CurrPoint.X);
                                TempBoardState[Shift.Y] = RemoveChecker(TempBoardState[Shift.Y],Shift.X); //removes enemy checker (same as above but Shift.X) is the position of the enemy checker
                                TempBoardState[BehindTilePoint.Y] = (ushort)(TempBoardState[BehindTilePoint.Y] | (int)CurrTile << BehindTilePoint.X);
                                PossibleBoardStates.Add((TempBoardState, BehindTilePoint));
                                #endregion
                                //RecursiveJumping(Board,CurrPoint,CurrTile);
                            }
                        }
                    }
                }
            }
            //checking possible board states
            #region Returning Board States But Filtering Out Non Jump Moves If Jump Is Forced
            List<(ushort[], Point?)> SurvivingBoardStates = [];
            for (int i = 0; i < PossibleBoardStates.Count; i++)
            {
                if (PossibleBoardStates[i].Jumped == null && MustJump)
                { 
                    continue; 
                }
                SurvivingBoardStates.Add(PossibleBoardStates[i]);
            }
            
            return (SurvivingBoardStates);
            #endregion
        }
        
        
        public List<(ushort[], Point?)> SingleJumping (ushort[] Board, Point CurrPoint, TileType Curr)
        {
            List<Directions> PossibleMoves = new();
            //setting what opposing checker it will look for
            if (Curr.HasFlag(TileType.Player) || Curr.HasFlag(TileType.King))
            {
                PossibleMoves.Add(Directions.Up);
                PossibleMoves.Add(Directions.UpRight);
            }
            if (!Curr.HasFlag(TileType.Player) || Curr.HasFlag(TileType.King))
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
            if (CurrPoint.X == 0 && (CurrPoint.Y % 2 == 0)) //leftmost
            {
                PossibleMoves.RemoveAll(x => x.HasFlag(Directions.Left));
            }
            else if (CurrPoint.X == 12 && (CurrPoint.Y % 2 == 1)) //rightmost
            {
                PossibleMoves.RemoveAll(x => !x.HasFlag(Directions.Left));
            }
            #endregion
            Point OtherChecker = new();
            for (int i = 0; i < PossibleMoves.Count; i++)
            {
                #region Getting Other Checker Location
                if (PossibleMoves[i].HasFlag(Directions.Down))
                {
                    OtherChecker.Y = CurrPoint.Y + 1;
                }
                else
                {
                    OtherChecker.Y = CurrPoint.Y - 1;
                }
                if (PossibleMoves[i].HasFlag(Directions.Left))
                {
                    if ((CurrPoint.Y - 1) % 2 == 0)
                    {
                        OtherChecker.X = CurrPoint.X;
                    }
                    else
                    {
                        OtherChecker.X = CurrPoint.X - 3;
                    }

                }
                else
                {
                    if ((CurrPoint.Y - 1) % 2 == 1)
                    {
                        OtherChecker.X = CurrPoint.X;
                    }
                    else
                    {
                        OtherChecker.X = CurrPoint.X + 3;
                    }
                }
                #endregion
            }



        }


    }

}
