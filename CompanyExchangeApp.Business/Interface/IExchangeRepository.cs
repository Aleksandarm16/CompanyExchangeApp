using CompanyExchangeApp.Business.Models;

namespace CompanyExchangeApp.Business.Interface
{
    public interface IExchangeRepository
    {
        public Task<IList<Exchange>> GetExchangesAsync();
        public void SetDbString(string connectionString);
    }
}
