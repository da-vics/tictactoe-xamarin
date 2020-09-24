﻿using System;
using System.Linq;
using tictactoe.Models;
using Xamarin.Forms;

namespace tictactoe.ViewModel
{
    public class GamePlayScreenViewModel : BaseViewModel
    {

        private Grid _container { get; set; }
        private GameLogic _gameLogic = new GameLogic();

        public delegate void AlertUser(string title, string message);
        public event AlertUser OnUserAlert;

        public GamePlayScreenViewModel(Grid grid)
        {
            _container = grid;
        }

        public void MapInit()
        {
            _gameLogic.defaultTileInit();

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
            _gameLogic.defaultTileInit();

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

            if (_gameLogic.PlayerState && _gameLogic.GameState)
            {

                if (_gameLogic.tileValues[gridIndex] == GameEnumStates.BoxState.free)
                {
                    _gameLogic.tileValues[gridIndex] = GameEnumStates.BoxState.cross;
                    btnPressed.Text = "X";
                    _gameLogic.PlayerState = false;
                    _gameLogic.getWinner(GameEnumStates.BoxState.cross);
                }

                else
                    OnUserAlert?.Invoke("Invalid", "Box Filled!");
            }///


            if (!_gameLogic.PlayerState && _gameLogic.GameState)
            {
                var btn = _container.Children.Cast<Button>().ToArray();
                var index = _gameLogic.computerPlay();
                btn[index].Text = "O";
                btn[index].TextColor = Color.Red;
                _gameLogic.tileValues[index] = GameEnumStates.BoxState.zero;
                _gameLogic.PlayerState = true;
                _gameLogic.getWinner(GameEnumStates.BoxState.zero);
            }

            if (_gameLogic.GameState != true)
            {
                if (_gameLogic.winner == GameEnumStates.IdentifyWinner.stalemate)
                {
                    _container.Children.Cast<Button>().ToList().ForEach(button =>
                    {
                        button.BackgroundColor = Color.Gold;
                        button.TextColor = Color.Black;
                    });

                    OnUserAlert?.Invoke("GameOver", "Stalemate");
                    Reset();
                }
                else
                    winLogic();
            }
        }
        #endregion

        void winLogic()
        {
            bool winnerLogic = _gameLogic.winner == GameEnumStates.IdentifyWinner.player
                   || _gameLogic.winner == GameEnumStates.IdentifyWinner.computer;

            if (winnerLogic)
            {
                var btn = _container.Children.Cast<Button>().ToArray();

                for (int i = 0; i < _gameLogic.maxRowSize; ++i)
                {
                    btn[_gameLogic.winSegments[i]].BackgroundColor = Color.Green;
                }

                OnUserAlert?.Invoke("GameOver", $"{_gameLogic.winner.ToString()} wins");
                Reset();
            }
        }
    }
}