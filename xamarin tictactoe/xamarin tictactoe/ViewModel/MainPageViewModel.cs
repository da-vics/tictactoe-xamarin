using System.Windows.Input;
using tictactoe.Views.Pages;
using Xamarin.Forms;

namespace tictactoe.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand ToGamePageCommand { get; set; }

        public MainPageViewModel()
        {
            ToGamePageCommand = new Command(DispalyGameMap);
        }

        private void DispalyGameMap()
        {
            Application.Current.MainPage.Navigation.PushAsync(new GamePlayPage());
        }

    }
}
