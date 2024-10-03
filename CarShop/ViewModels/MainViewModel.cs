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
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarShop.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private INavigation _navigation;

        public MainViewModel()
        {
           _database = new Database();
            IsListVisible = false;
           _=Initialize();
        }

        private readonly Database _database;

        [ObservableProperty]
        public ObservableCollection<CarShopItem> _carShopItems;
    
        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }


        [ObservableProperty]
        private bool _isListVisible;
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _address;
        [ObservableProperty]
        private string _carBrand;
        [ObservableProperty]
        private string _carModel;
        [ObservableProperty]
        private double _registrationNumber;
        [ObservableProperty]
        private DateTime _handInDate;
        [ObservableProperty]
        private string _carProblem;

        [ObservableProperty]
        private DateTime _selectedDate;
        
        #region  Methods
        [RelayCommand]
        private async Task AddNewCarShopItem()
        {
            var newCarShopItem = new CarShopItem
            {
                customerName = Name,
                customerAddres = Address,
                carBrand = CarBrand,
                carModel = CarModel,
                RegistrationNumber = RegistrationNumber,
                handInDate = HandInDate,
                carProblem = CarProblem
            };
            var inserted=await _database.AddCarShopItem(newCarShopItem);
            await Initialize(); 
            if(inserted!=0)
            {
                CarShopItems.Add(newCarShopItem);
                Name = string.Empty;
                Address = string.Empty;
                CarBrand = string.Empty;
                CarModel = string.Empty;
                RegistrationNumber = 0;
                HandInDate = DateTime.Now;
                CarProblem = string.Empty;
            }

        }

        [RelayCommand]
        private async void LoadFilteredItems()
        {
        try
        {
            Console.WriteLine($"Filter button clicked. Selected date: {SelectedDate.ToShortDateString()}");

            var items = await _database.GetCarShopItemsByDate(SelectedDate);
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                CarShopItems.Clear();
                foreach (var item in items)
                {
                    CarShopItems.Add(item);
                }
                                    // Show the list if there are items
                    IsListVisible = CarShopItems.Count > 0; 

                Console.WriteLine($"Number of filtered items: {CarShopItems.Count()}");
                foreach (var carShopItem in CarShopItems)
                {
                    Name = carShopItem.customerName;
                    CarBrand = carShopItem.carBrand;
                    Console.WriteLine($"Name: {carShopItem.customerName}, Brand: {carShopItem.carBrand}");
                }

                
            });

            
            ErrorMessage = string.Empty;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading items for {SelectedDate.ToShortDateString()}: {ex.Message}";
        }
    }
    private async Task Initialize()
        {
            var items = await _database.GetCarShopItems();
            CarShopItems = new ObservableCollection<CarShopItem>(items);
        }

    #endregion Methods

}
}