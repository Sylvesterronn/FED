using CarShop.ViewModels;
namespace CarShop.Pages
{
    public partial class KalenderPage : ContentPage
    {
        public KalenderPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
