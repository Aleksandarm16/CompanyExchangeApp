using CompanyExchangeApp.Business.Models;

namespace CompanyExchangeApp.Business.Interface
{

    public interface ISymbolRepository
    {
        public Task<IList<Symbol>> GetAllSymbolsAsync(string? type = null, string? exchange = null);
        public Task DeleteSymbolAsync(Symbol symbol);
        public Task SaveSymbolAsync(Symbol symbol);
        public void SetDbString(string connectionString);
    }
}
