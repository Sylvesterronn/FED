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
            
            _=Initialize();
        }

        

        private readonly Database _database;
        public ObservableCollection<FakturaItem> FakturaItems { get; set; }
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
                hourlyRate=HourlyRate
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
        private async Task Initialize()
        {
            var items = await _database.GetFakturaItem();
            FakturaItems = new ObservableCollection<FakturaItem>(items);
        }

        #endregion Methods
    
    }
}