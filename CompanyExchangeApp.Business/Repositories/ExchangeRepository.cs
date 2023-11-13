using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyExchangeApp.Business.Repositories
{
    public class ExchangeRepository : IExchangeRepository
    {
        private string _dbConnectionString;

        public async Task<IList<Exchange>> GetExchangesAsync()
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    dbContext.Database.EnsureCreated();
                    return await dbContext.Exchanges.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred in ExchangeRepository: {ex.Message}");
                throw; 
            }
        }

        public void SetDbString(string connectionString)
        {
           _dbConnectionString = connectionString;
        }
    }
}
