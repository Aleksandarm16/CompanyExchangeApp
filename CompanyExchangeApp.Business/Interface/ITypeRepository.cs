using CompanyExchangeApp.Business.Dtos;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Business.Interface
{
    public interface ITypeRepository
    {
        public Task<IList<Type>> GetTypesAsync();
        public void SetDbString(string connectionString);
    }
}
