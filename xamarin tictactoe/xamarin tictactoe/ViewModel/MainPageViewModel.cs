using System.Windows.Input;
using tictactoe.Views.Pages;
using Xamarin.Forms;

namespace tictactoe.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand ToGamePageCommand { get; set; }
        public ICommand DisplayInfo { get; set; }

        public MainPageViewModel()
        {
            ToGamePageCommand = new Command(() => { Application.Current.MainPage.Navigation.PushAsync(new GamePlayPage()); });
            DisplayInfo = new Command(() => { Application.Current.MainPage.DisplayAlert("About", "Open Source Andriod tic-tac-toe", "ok"); });
        }
    }
}
