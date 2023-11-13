using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyExchangeApp.Business.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private string _dbConnectionString;
        public async Task<IList<Models.Type>> GetTypesAsync()
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    dbContext.Database.EnsureCreated();
                    return await dbContext.Types.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred in TypeRepository: {ex.Message}");
                throw;
            }
        }

        public void SetDbString(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
    }
}
