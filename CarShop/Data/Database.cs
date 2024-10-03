using CarShop.Models;
using CarShop.Models; // Assuming CarShopItem is in CarShop.Models namespace
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Data
{
    internal class Database
    {
        private readonly SQLiteAsyncConnection _connection;

        public Database()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "MauiTodoV8.db");
            Console.WriteLine($"Database path: {databasePath}");
            //string _dbEncryptionKey = SecureStorage.GetAsync("dbKey").Result;

            //if (string.IsNullOrEmpty(_dbEncryptionKey))
            //{
            //    Guid g = new Guid();
            //    _dbEncryptionKey = g.ToString();
            //    SecureStorage.SetAsync("dbKey", _dbEncryptionKey);
            //}

            //var dbOptions = new SQLiteConnectionString(databasePath, true, key: _dbEncryptionKey);
            var dbOptions = new SQLiteConnectionString(databasePath, true);

            _connection = new SQLiteAsyncConnection(dbOptions);

            _ = Initialise();
        }

        private async Task Initialise()
        {
            await _connection.CreateTableAsync<CarShopItem>();
            await _connection.CreateTableAsync<FakturaItem>();
        }

        public async Task<List<CarShopItem>> GetCarShopItems()
        {
            return await _connection.Table<CarShopItem>().ToListAsync();
        }

        public async Task<List<FakturaItem>>GetFakturaItem()
        {
            return await _connection.Table<FakturaItem>().ToListAsync();
        }

        public async Task<CarShopItem> GetCarShopItem(int id)
        {
            var query = _connection.Table<CarShopItem>().Where(t => t.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> AddCarShopItem(CarShopItem item)
        {
            return await _connection.InsertAsync(item);
        }

        public async Task<int> AddFakturaItem(FakturaItem item)
        {
            return await _connection.InsertAsync(item);
        }

        public async Task<int> DeleteCarShopItem(CarShopItem item)
        {
            return await _connection.DeleteAsync(item);
        }

        public async Task<int> UpdateCarShopItem(CarShopItem item)
        {
            return await _connection.UpdateAsync(item);
        }

        public async Task<List<CarShopItem>> GetCarShopItemsByDate(DateTime date)
        {
            Console.WriteLine($"Querying database for items with hand-in date: {date}");
            
            long ticks=date.Ticks;
            Console.WriteLine($"Querying database for items with hand-in date: {ticks}");
            try
            {
                var items = await _connection.Table<CarShopItem>().ToListAsync();
                return items.Where(item => item.handInDate.Ticks == ticks).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error querying database: {ex.Message}");
                throw; // Re-throw the exception to be caught in your ViewModel
            }
        }

        internal async Task<IEnumerable<CarShopItem>> FakturaItems()
        {
            throw new NotImplementedException();
        }
    }
}
