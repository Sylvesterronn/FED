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
    public partial class FakturaViewModel : ObservableObject
    {
        private INavigation _navigation;

        public FakturaViewModel()
        {
            _database = new Database();
             FakturaItems = new ObservableCollection<FakturaItem>();
            _=Initialize();
        }

        

        private readonly Database _database;
        [ObservableProperty]
        public ObservableCollection<FakturaItem> fakturaItems;

        [ObservableProperty]
        public string _mechanicName;
        [ObservableProperty]
        public string _materials;
        [ObservableProperty]
        public double _materialsCost;
        [ObservableProperty]
        public double _hours; 
        [ObservableProperty]
        public double _hourlyRate;

        [ObservableProperty]
        public int _carShopItemId;

        [ObservableProperty]
        private bool _isListVisible;
    

        #region Methods
        [RelayCommand]
        private async Task AddNewFakturaItem()
        {
            var newFakturaItem = new FakturaItem
            {
                mechanicName=MechanicName,
                materials=Materials,
                materialsCost=MaterialsCost,
                hours=Hours,
                hourlyRate=HourlyRate,
                carShopItemId=CarShopItemId
            };
            var inserted = await _database.AddFakturaItem(newFakturaItem);
            await Initialize();
            if (inserted!=0)
            {
                FakturaItems.Add(newFakturaItem);
                MechanicName=string.Empty;
                Materials=string.Empty;
                MaterialsCost=0;
                Hours=0;
                HourlyRate=0;
            }
        }

        [RelayCommand]  
        private async Task GetFakturaItem()
        {
            Console.WriteLine($"Querying database for FakturaItem");
            try
            {
                var items = await _database.GetFakturaItem();
                FakturaItems = new ObservableCollection<FakturaItem>(items);
                IsListVisible = FakturaItems.Count > 0; 

                Console.WriteLine($"Number of faktura items: {FakturaItems.Count()}");
                foreach (var FakturaItem in FakturaItems)
                {
                    MechanicName = FakturaItem.mechanicName;
                    Materials = FakturaItem.materials;

                    Console.WriteLine($"Mechanic Name: {FakturaItem.mechanicName}, Materials: {FakturaItem.materials}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
    
        }

        [RelayCommand]
        private async Task DeleteFakturaItem(FakturaItem fakturaItem)
        {
            try
            {
                var deleted = await _database.DeleteFakturaItem(fakturaItem);
                if (deleted!=0)
                {
                    FakturaItems.Remove(fakturaItem);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
        private async Task Initialize()
        {
            var items = await _database.GetFakturaItem();
            FakturaItems = new ObservableCollection<FakturaItem>(items);
            IsListVisible = false; // Show list only if there are items

        }

        #endregion Methods
    
    }
}