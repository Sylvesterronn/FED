using CarShop.Models;
using CarShop.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarShop.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        

        public MainViewModel()
        {
           _database = new Database();
           AddToCarShopCommand = new Command(async () => await AddNewCarShopItem());
           _=Initialize();
        }

        private readonly Database _database;
        public ObservableCollection<CarShopItem> CarShopItems { get; set; }
        public string NewCustomerName { get; set; }
        public string NewCustomerAddress { get; set; }
        public string NewCarBrand { get; set; }
        public string NewCarModel { get; set; }
        public double NewRegistrationNumber { get; set; }
        public DateTime NewHandInDate { get; set; }
        public string NewCarProblem { get; set; }

        public ICommand AddToCarShopCommand{get;set;}

        #region  Methods

        private async Task AddNewCarShopItem()
        {
            var newCarShopItem = new CarShopItem
            {
                customerName = NewCustomerName,
                customerAddres = NewCustomerAddress,
                carBrand = NewCarBrand,
                carModel = NewCarModel,
                RegistrationNumber = NewRegistrationNumber,
                handInDate = NewHandInDate,
                carProblem = NewCarProblem
            };
            var inserted=await _database.AddCarShopItem(newCarShopItem);
            await Initialize(); 
            if(inserted!=0)
            {
                CarShopItems.Add(newCarShopItem);
                NewCustomerName = string.Empty;
                NewCustomerAddress = string.Empty;
                NewCarBrand = string.Empty;
                NewCarModel = string.Empty;
                NewRegistrationNumber = 0;
                NewHandInDate = DateTime.Now;
                NewCarProblem = string.Empty;
                RaisePropertyChanged(nameof(NewCustomerName), nameof(NewCustomerAddress), nameof(NewCarBrand), nameof(NewCarModel), nameof(NewRegistrationNumber), nameof(NewHandInDate), nameof(NewCarProblem));
            }

        }

    private async Task Initialize()
        {
            var items = await _database.GetCarShopItems();
            CarShopItems = new ObservableCollection<CarShopItem>(items);
        }

    #endregion Methods

    #region INotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void RaisePropertyChanged(params string[] properties)
    {
        foreach(var propertyName in properties)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }

    #endregion INotifyPropertyChanged Implementation
}
}