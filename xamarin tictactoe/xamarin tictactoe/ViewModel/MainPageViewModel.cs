using System.Windows.Input;
using tictactoe.Models;
using tictactoe.Views.Pages;
using Xamarin.Forms;

namespace tictactoe.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand AgaisntComputer { get; set; }
        public ICommand AgaisntHuman { get; set; }
        public ICommand DisplayInfo { get; set; }

        public MainPageViewModel()
        {
            AgaisntComputer = new Command(() => { Application.Current.MainPage.Navigation.PushAsync(new GamePlayPage(GamePlayMode.AgaistComputer)); });
            AgaisntHuman = new Command(() => { Application.Current.MainPage.Navigation.PushAsync(new GamePlayPage(GamePlayMode.AgaistHuman)); });
            DisplayInfo = new Command(() => { Application.Current.MainPage.DisplayAlert("About", "Open Source Andriod tic-tac-toe", "ok"); });
        }
    }
}
