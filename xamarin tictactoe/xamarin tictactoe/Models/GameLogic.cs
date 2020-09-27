using System;

namespace tictactoe.Models
{
    public class GameLogic
    {
        #region privateMembers
        private const int maxSize = 9;
        private bool FoundPattern { get; set; } = false;
        #endregion

        #region publicMembers
        public const short maxRowSize = 3;
        public const short maxcolSize = 3;
        public short[] winSegments;

        public bool FirstPlayerState { get; set; } = true;
        public bool GameState { get; private set; } = true;

        public BoxState[] tileValues;
        public IdentifyWinner winner;
        #endregion

        public GameLogic()
        {
            winSegments = new short[3] { 0, 0, 0 };
            tileValues = new BoxState[maxSize];
            winner = new IdentifyWinner();
        }

        public void DefaultTileInit()
        {
            for (int i = 0; i < maxSize; ++i)
                tileValues[i] = BoxState.free;

            winner = IdentifyWinner.NULL;
            FoundPattern = false;
            FirstPlayerState = true;
            GameState = true;
        }

        #region ComputerControl
        private short? LogicalAIComputation(BoxState Piece1, BoxState Piece2)
        {
            short count = 0;
            short? Aival = null;

            #region Horizontalcheck
            short temp = maxRowSize;
            short temp2 = 0;

            for (short j = 0; j < maxRowSize; ++j)
            {
                for (short i = temp2; i < temp; ++i)
                {
                    if (tileValues[i] == Piece1)
                        break;

                    if (tileValues[i] == Piece2)
                        ++count;

                    else
                        Aival = i;
                }

                if (count == 2 && Aival != null)
                    break;

                count = 0;
                temp += maxRowSize;
                temp2 += maxRowSize;
            }

            if (count == 2 && Aival != null) return (short)Aival;
            #endregion

            #region VerticalCheck
            count = 0;
            temp = (maxcolSize * 2);
            temp2 = 0;
            short temp3 = temp2;

            for (short j = 0; j < maxcolSize; ++j)
            {
                for (short i = temp2; i <= temp; i += 3)
                {
                    if (tileValues[i] == Piece1)
                        break;

                    if (tileValues[i] == Piece2)
                        ++count;

                    else
                        Aival = i;
                }


                if (count == 2)
                    break;

                count = 0;
                temp += 1;
                temp3 += 1;
                temp2 = temp3;
            }

            if (count == 2 && Aival != null) return (short)Aival;
            #endregion

            #region diagonalCheck
            count = 0;
            temp = (maxSize - 1);
            temp2 = 0;
            short incr = (maxcolSize + 1);

            for (short j = 0; j < maxcolSize - 1; ++j)
            {
                for (short i = temp2; i <= temp; i += incr)
                {
                    if (tileValues[i] == Piece1)
                        break;

                    if (tileValues[i] == Piece2)
                        ++count;

                    else
                        Aival = i;
                }

                if (count == 2)
                    break;

                count = 0;
                temp -= (maxcolSize - 1);
                temp2 += (maxcolSize - 1);
                incr -= (maxcolSize - 1);
            }

            if (count == 2 && Aival != null) return (short)Aival;
            #endregion

            return null;
        }

        #endregion

        #region ComputerTurn
        public int ComputerPlay()
        {
            int index;

            var val = LogicalAIComputation(BoxState.cross, BoxState.zero);

            if (val == null)
                val = LogicalAIComputation(BoxState.zero, BoxState.cross);

            if (val != null)
            {
                index = (int)val;
                return index;
            }

            while (true)
            {
                Random random = new Random();
                index = random.Next(0, maxSize - 1);

                if (tileValues[index] == BoxState.free)
                    break;
            }

            return index;
        }
        #endregion

        private void Getwin(BoxState boxState)
        {

            #region Horizontalcheck

            short segindex = 0;
            short temp = maxRowSize;
            short temp2 = 0;
            for (short j = 0; j < maxRowSize; ++j)
            {
                for (short i = temp2; i < temp; ++i)
                {
                    if (tileValues[i] != boxState)
                    {
                        this.GameState = true;
                        FoundPattern = false;
                        break;
                    }

                    else if (tileValues[i] == boxState)
                    {
                        this.winSegments[segindex++] = i;
                        this.GameState = false;
                        FoundPattern = true;
                    }
                }

                if (FoundPattern)
                    break;

                segindex = 0;
                temp += maxRowSize;
                temp2 += maxRowSize;
            }

            #endregion

            if (FoundPattern) return; ///

            #region VerticalCheck
            segindex = 0;
            temp = (maxcolSize * 2);
            temp2 = 0;
            short temp3 = temp2;

            for (short j = 0; j < maxcolSize; ++j)
            {
                for (short i = temp2; i <= temp; i += 3)
                {
                    if (tileValues[i] != boxState)
                    {
                        this.GameState = true;
                        FoundPattern = false;
                        break;
                    }

                    else if (tileValues[i] == boxState)
                    {
                        this.winSegments[segindex++] = i;
                        this.GameState = false;
                        FoundPattern = true;
                    }
                }

                if (FoundPattern)
                    break;

                segindex = 0;
                temp += 1;
                temp3 += 1;
                temp2 = temp3;
            }

            #endregion

            if (FoundPattern) return; ///

            #region diagonalCheck

            segindex = 0;
            temp = (maxSize - 1);
            temp2 = 0;
            short incr = (maxcolSize + 1);

            for (short j = 0; j < maxcolSize - 1; ++j)
            {
                for (short i = temp2; i <= temp; i += incr)
                {
                    if (tileValues[i] != boxState)
                    {
                        this.GameState = true;
                        FoundPattern = false;
                        break;
                    }

                    else if (tileValues[i] == boxState)
                    {
                        this.winSegments[segindex++] = i;
                        this.GameState = false;
                        FoundPattern = true;
                    }
                }

                if (FoundPattern)
                    break;

                segindex = 0;
                temp -= (maxcolSize - 1);
                temp2 += (maxcolSize - 1);
                incr -= (maxcolSize - 1);
            }
            #endregion
        }

        public void CheckGameState()
        {
            if (this.winner == IdentifyWinner.NULL)
            {
                foreach (var tile in this.tileValues)
                {
                    if (tile == BoxState.free)
                    {
                        this.GameState = true;
                        break;
                    }
                    else
                        this.GameState = false;
                }

                if (this.GameState != true)
                    winner = IdentifyWinner.stalemate;
            }
        }

        public void GetWinner(BoxState tempstate)
        {
            if (this.GameState != false)
            {
                Getwin(tempstate);

                if (FoundPattern)
                {
                    if (tempstate == BoxState.cross)
                        winner = IdentifyWinner.player1;

                    else if (tempstate == BoxState.zero)
                        winner = IdentifyWinner.player2;
                }

                CheckGameState();
            }

        } ///
    }
}
