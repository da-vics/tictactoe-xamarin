using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tictactoe.ViewModel;
using tictactoe.Models;

namespace tictactoe.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePlayPage : ContentPage
    {
        private GamePlayScreenViewModel _screenViewModel { get; set; }

        public GamePlayPage(GamePlayMode gamePlayMode)
        {
            InitializeComponent();
            _screenViewModel = new GamePlayScreenViewModel(Container, gamePlayMode);
            this.BindingContext = _screenViewModel;
            _screenViewModel.MapInit();

            _screenViewModel.OnUserAlert += (title, message) =>
            {
                DisplayAlert(title, message, "OK");
            };
        }

    }
}