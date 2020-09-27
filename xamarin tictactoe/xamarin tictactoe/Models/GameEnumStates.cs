namespace tictactoe.Models
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
        player1,
        player2,
        stalemate
    }

    public enum GamePlayMode
    {
        AgaistComputer,
        AgaistHuman
    }

    public enum PlayerStates
    {
        Player1Turn,
        Player2Turn
    }
}
