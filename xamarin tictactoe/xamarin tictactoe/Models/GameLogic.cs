using System;

namespace tictactoe.Models
{
    public class GameLogic
    {

        #region privateMembers
        private int maxSize { get; set; } = 9;

        private bool foundPattern { get; set; }
        #endregion


        #region publicVariables
        public short maxRowSize { get; private set; } = 3;

        public short maxcolSize { get; private set; } = 3;

        public bool PlayerState { get; set; }

        public bool GameState { get; private set; }

        public short[] winSegments;

        public GameEnumStates.BoxState[] tileValues;

        public GameEnumStates.IdentifyWinner winner;
        #endregion


        public GameLogic()
        {
            this.winSegments = new short[3] { 0, 0, 0 };
            this.tileValues = new GameEnumStates.BoxState[9];
            this.winner = new GameEnumStates.IdentifyWinner();
            this.foundPattern = false;
            this.PlayerState = true;
            this.GameState = true;
        }

        public void defaultTileInit()
        {
            for (int i = 0; i < maxSize; ++i)
            {
                tileValues[i] = GameEnumStates.BoxState.free;
            }

            winner = GameEnumStates.IdentifyWinner.NULL;
            this.foundPattern = false;
            this.PlayerState = true;
            this.GameState = true;
        }


        private short? processAI()
        {
            short count = 0;
            short? Aival = null;

            #region Horizontalcheck
            short temp = this.maxRowSize;
            short temp2 = 0;

            for (short j = 0; j < this.maxRowSize; ++j)
            {
                for (short i = temp2; i < temp; ++i)
                {
                    if (tileValues[i] == GameEnumStates.BoxState.zero)
                        break;

                    if (tileValues[i] == GameEnumStates.BoxState.cross)
                        ++count;

                    else
                        Aival = i;
                }

                if (count == 2 && Aival != null)
                    break;

                count = 0;
                temp += this.maxRowSize;
                temp2 += 2 + 1;
            }

            if (count == 2 && Aival != null) return (short)Aival;
            #endregion

            #region VerticalCheck
            count = 0;
            temp = 6;
            temp2 = 0;
            short temp3 = temp2;

            for (short j = 0; j < this.maxcolSize; ++j)
            {
                for (short i = temp2; i <= temp; i += 3)
                {
                    if (tileValues[i] == GameEnumStates.BoxState.zero)
                        break;

                    if (tileValues[i] == GameEnumStates.BoxState.cross)
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
            temp = 8;
            temp2 = 0;
            short incr = 4;

            for (short j = 0; j < this.maxcolSize - 1; ++j)
            {
                for (short i = temp2; i <= temp; i += incr)
                {
                    if (tileValues[i] == GameEnumStates.BoxState.zero)
                        break;

                    if (tileValues[i] == GameEnumStates.BoxState.cross)
                        ++count;

                    else
                        Aival = i;
                }

                if (count == 2)
                    break;

                count = 0;
                temp -= 2;
                temp2 += 2;
                incr -= 2;
            }

            if (count == 2 && Aival != null) return (short)Aival;
            #endregion

            return null;
        }

        #region ComputerTurn
        public int computerPlay()
        {
            int index = 0;

            short? val = processAI();

            if (val != null)
            {
                index = (int)val;
                return index;
            }

            while (true)
            {
                Random random = new Random();
                index = random.Next(0, maxSize - 1);

                if (tileValues[index] == GameEnumStates.BoxState.free)
                    break;
            }

            return index;
        }
        #endregion

        private void getwin(GameEnumStates.BoxState boxState)
        {

            #region Horizontalcheck

            short segindex = 0;
            short temp = this.maxRowSize;
            short temp2 = 0;
            for (short j = 0; j < this.maxRowSize; ++j)
            {
                for (short i = temp2; i < temp; ++i)
                {
                    if (tileValues[i] != boxState)
                    {
                        this.GameState = true;
                        foundPattern = false;
                        break;
                    }

                    else if (tileValues[i] == boxState)
                    {
                        this.winSegments[segindex++] = i;
                        this.GameState = false;
                        foundPattern = true;
                    }
                }

                if (foundPattern)
                    break;

                segindex = 0;
                temp += this.maxRowSize;
                temp2 += 2 + 1;
            }

            #endregion

            if (foundPattern) return; ///


            #region VerticalCheck
            segindex = 0;
            temp = 6;
            temp2 = 0;
            short temp3 = temp2;

            for (short j = 0; j < this.maxcolSize; ++j)
            {
                for (short i = temp2; i <= temp; i += 3)
                {
                    if (tileValues[i] != boxState)
                    {
                        this.GameState = true;
                        foundPattern = false;
                        break;
                    }

                    else if (tileValues[i] == boxState)
                    {
                        this.winSegments[segindex++] = i;
                        this.GameState = false;
                        foundPattern = true;
                    }
                }

                if (foundPattern)
                    break;

                segindex = 0;
                temp += 1;
                temp3 += 1;
                temp2 = temp3;
            }

            #endregion

            if (foundPattern) return; ///


            #region diagonalCheck

            segindex = 0;
            temp = 8;
            temp2 = 0;
            short incr = 4;

            for (short j = 0; j < this.maxcolSize - 1; ++j)
            {
                for (short i = temp2; i <= temp; i += incr)
                {
                    if (tileValues[i] != boxState)
                    {
                        this.GameState = true;
                        foundPattern = false;
                        break;
                    }

                    else if (tileValues[i] == boxState)
                    {
                        this.winSegments[segindex++] = i;
                        this.GameState = false;
                        foundPattern = true;
                    }
                }

                if (foundPattern)
                    break;

                segindex = 0;
                temp -= 2;
                temp2 += 2;
                incr -= 2;
            }
            #endregion
        }

        public void checkGameState()
        {
            if (this.winner == GameEnumStates.IdentifyWinner.NULL)
            {
                foreach (var tile in this.tileValues)
                {
                    if (tile == GameEnumStates.BoxState.free)
                    {
                        this.GameState = true;
                        break;
                    }
                    else
                        this.GameState = false;
                }

                if (this.GameState != true)
                    winner = GameEnumStates.IdentifyWinner.stalemate;
            }
        }

        public void getWinner(GameEnumStates.BoxState tempstate)
        {
            if (this.GameState != false)
            {
                getwin(tempstate);

                if (foundPattern)
                {
                    if (tempstate == GameEnumStates.BoxState.cross)
                        winner = GameEnumStates.IdentifyWinner.player;

                    else if (tempstate == GameEnumStates.BoxState.zero)
                        winner = GameEnumStates.IdentifyWinner.computer;
                }

                this.checkGameState();
            }

        } ///

    }

}
