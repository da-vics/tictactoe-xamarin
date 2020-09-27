using System;
using System.Linq;
using System.Windows.Input;
using tictactoe.Models;
using Xamarin.Forms;

namespace tictactoe.ViewModel
{
    public class GamePlayScreenViewModel : BaseViewModel
    {
        private Grid _container { get; set; }
        private readonly GameLogic _gameLogic = new GameLogic();
        private GamePlayMode _gamePlayMode { get; set; }

        public delegate void AlertUser(string title, string message);
        public event AlertUser OnUserAlert;

        public ICommand ResetGame { get; set; }

        public GamePlayScreenViewModel(Grid grid, GamePlayMode playMode)
        {
            _gamePlayMode = playMode;
            _container = grid;
            ResetGame = new Command(Reset);
        }

        public void MapInit()
        {
            _gameLogic.DefaultTileInit();

            _container.Children.Cast<Button>().ToList().ForEach(btn =>
            {
                btn.Clicked += Button_Clicked;
                btn.Text = string.Empty;
                btn.BackgroundColor = Color.White;
                btn.TextColor = Color.Blue;
            });
        }

        public void Reset()
        {
            _gameLogic.DefaultTileInit();

            _container.Children.Cast<Button>().ToList().ForEach(btn =>
            {
                btn.Text = string.Empty;
                btn.BackgroundColor = Color.White;
                btn.TextColor = Color.Blue;
            });
        }

        #region BtnEventCallBack
        private void Button_Clicked(object sender, EventArgs e)
        {
            var btnPressed = (Button)sender;
            var column = Grid.GetColumn(btnPressed);
            var row = Grid.GetRow(btnPressed);

            var gridIndex = column + (row * 3);

            if (_gamePlayMode == GamePlayMode.AgaistComputer)
            {
                if (_gameLogic.FirstPlayerState)
                    playerState(btnPressed, gridIndex, PlayerStates.Player1Turn);

                computerState();
            }

            else
            {
                if (_gameLogic.FirstPlayerState)
                    playerState(btnPressed, gridIndex, PlayerStates.Player1Turn);
                else
                {
                    playerState(btnPressed, gridIndex, PlayerStates.Player2Turn);
                    _gameLogic.FirstPlayerState = true;
                }
            }

            if (_gameLogic.GameState != true)
            {
                if (_gameLogic.winner == IdentifyWinner.stalemate)
                {
                    _container.Children.Cast<Button>().ToList().ForEach(button =>
                    {
                        button.BackgroundColor = Color.Gold;
                        button.TextColor = Color.Black;
                    });

                    OnUserAlert?.Invoke("GameOver", "Stalemate");
                    //Reset();
                }
                else
                    WinLogic();
            }
        }
        #endregion

        void playerState(Button btnPressed, int gridIndex, PlayerStates player)
        {
            var _boxState = new BoxState();
            var playerchar = string.Empty;

            if (player == PlayerStates.Player1Turn)
            {
                _boxState = BoxState.cross;
                playerchar = "X";
            }

            else
            {
                _boxState = BoxState.zero;
                playerchar = "O";
                btnPressed.TextColor = Color.DarkRed;
            }

            if (_gameLogic.GameState)
            {
                if (_gameLogic.tileValues[gridIndex] == BoxState.free)
                {
                    _gameLogic.tileValues[gridIndex] = _boxState;
                    btnPressed.Text = playerchar;
                    _gameLogic.FirstPlayerState = false;
                    _gameLogic.GetWinner(_boxState);
                }

                else
                    OnUserAlert?.Invoke("Invalid", "Box Filled!");
            }///
        }

        void computerState()
        {
            if (!_gameLogic.FirstPlayerState && _gameLogic.GameState)
            {
                var btn = _container.Children.Cast<Button>().ToArray();
                var index = _gameLogic.ComputerPlay();
                btn[index].Text = "O";
                btn[index].TextColor = Color.DarkRed;
                _gameLogic.tileValues[index] = BoxState.zero;
                _gameLogic.FirstPlayerState = true;
                _gameLogic.GetWinner(BoxState.zero);
            }
        }

        void WinLogic()
        {
            bool winnerLogic = _gameLogic.winner == IdentifyWinner.player1
                   || _gameLogic.winner == IdentifyWinner.player2;

            if (winnerLogic)
            {
                var btn = _container.Children.Cast<Button>().ToArray();

                for (int i = 0; i < GameLogic.maxRowSize; ++i)
                    btn[_gameLogic.winSegments[i]].BackgroundColor = Color.Green;

                OnUserAlert?.Invoke("GameOver", $"{_gameLogic.winner.ToString()} wins");
                //Reset();
            }
        }
    }
}