using CarShop.ViewModels;

namespace CarShop.Pages
{
    public partial class FakturaPage : ContentPage
    {
        public FakturaPage()
        {
            InitializeComponent();
            BindingContext = new FakturaViewModel();
        }
    }

    
}