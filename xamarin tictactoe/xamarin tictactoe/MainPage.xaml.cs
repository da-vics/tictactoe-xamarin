using System.ComponentModel;
using tictactoe.ViewModel;
using Xamarin.Forms;

namespace xamarin_tictactoe
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainScreenViewModel _screenViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _screenViewModel = new MainScreenViewModel(Container);
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
