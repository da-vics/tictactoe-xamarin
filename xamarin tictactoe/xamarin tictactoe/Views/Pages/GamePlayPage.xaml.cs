using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tictactoe.ViewModel;

namespace tictactoe.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePlayPage : ContentPage
    {
        private GamePlayScreenViewModel _screenViewModel { get; set; }

        public GamePlayPage()
        {
            InitializeComponent();
            _screenViewModel = new GamePlayScreenViewModel(Container);
            this.BindingContext = _screenViewModel;
            _screenViewModel.MapInit();
            _screenViewModel.OnUserAlert += AlertUser;
        }

        private void AlertUser(string title, string message)
        {
            DisplayAlert(title, message, "OK");
        }
    }
}