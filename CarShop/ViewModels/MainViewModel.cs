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
           _=Initialize();
        }

        private readonly Database _database;
        public ObservableCollection<CarShopItem> CarShopItems { get; set; }

        #region  Methods

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

    #endregion INotifyPropertyChanged Implementation
}
}