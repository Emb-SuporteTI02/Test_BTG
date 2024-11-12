using Test_BTG.ViewModel;
using Test_BTG.Views;

namespace Test_BTG
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModelCliente();
        }

        private async void OnCadastrarClienteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastrarClientes());
        }
    }
}
