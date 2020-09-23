using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe.Models
{
    public class GameEnumStates
    {
        public enum BoxState
        {
            /// empty game tile
            free,
            /// game tile with X
            cross,
            /// game tile with O
            zero
        }

        public enum IdentifyWinner
        {
            NULL,
            player,
            computer,
            stalemate
        }

    }
}
