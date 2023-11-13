using CompanyExchangeApp.Business.Dtos;
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
    public class SymbolRepository : ISymbolRepository
    {
        private string _dbConnectionString;

        public async Task DeleteSymbolAsync(Symbol symbol)
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    dbContext.Database.EnsureCreated();

                    // Find the symbol in the database by its ID
                    Symbol symbolToDelete = await dbContext.Symbols.FindAsync(symbol.Id);

                    if (symbolToDelete != null)
                    {
                        // Remove the symbol from the database
                        dbContext.Symbols.Remove(symbolToDelete);
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // Handle the case where the symbol with the specified ID is not found
                        Console.WriteLine($"Error deleting symbol specified ID is not found");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred in SymbolRepository: {ex.Message}");
                throw;
            }
        }

        public async Task<IList<Symbol>> GetAllSymbolsAsync(string? type = null, string? exchange = null)
        {
            try
            {

                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    dbContext.Database.EnsureCreated();

                    IQueryable<Symbol> query = dbContext.Symbols
                             .Include(s => s.Exchange)
                             .Include(s => s.Type);


                    if (!string.IsNullOrEmpty(type) && type != "ALL")
                    {
                        query = query.Where(s => s.Type.Name == type);
                    }

                    if (!string.IsNullOrEmpty(exchange) && exchange != "ALL")
                    {
                        query = query.Where(s => s.Exchange.Name == exchange);
                    }

                    IList<Symbol> symbols = await query.ToListAsync();

                    return symbols;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred in SymbolRepository: {ex.Message}");
                throw;
            }
        }

        public async Task SaveSymbolAsync(Symbol symbol)
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    // Ensure the database is created
                    dbContext.Database.EnsureCreated();

                    // Check if the associated Type and Exchange records exist
                    Models.Type existingType = await dbContext.Types.FindAsync(symbol.TypeId);
                    Exchange existingExchange = await dbContext.Exchanges.FindAsync(symbol.ExchangeId);

                    if (existingType == null || existingExchange == null)
                    {
                        // Type or Exchange record doesn't exist, handle accordingly (throw an exception, log, etc.)
                        Console.WriteLine("Type or Exchange record not found. Symbol not saved.");
                        return;
                    }

                    // Set navigation properties
                    symbol.Type = existingType;
                    symbol.Exchange = existingExchange;

                    // Check if the symbol already exists in the database
                    Symbol existingSymbol = await dbContext.Symbols.FindAsync(symbol.Id);

                    if (existingSymbol != null)
                    {
                        // Symbol exists, update its properties
                        dbContext.Entry(existingSymbol).CurrentValues.SetValues(symbol);
                    }
                    else
                    {
                        // Symbol doesn't exist, add it to the database
                        dbContext.Symbols.Add(symbol);
                    }

                    // Set the state of related entities to Unchanged to prevent them from being added or updated
                    dbContext.Entry(existingType).State = EntityState.Unchanged;
                    dbContext.Entry(existingExchange).State = EntityState.Unchanged;

                    // Save changes to the database
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred in SymbolRepository: {ex.Message}");
                throw;
            }
        }

        public void SetDbString(string connectionString)
        {
           _dbConnectionString = connectionString;
        }
    }
}
