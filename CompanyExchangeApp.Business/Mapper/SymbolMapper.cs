using CompanyExchangeApp.Business.Dtos;
using CompanyExchangeApp.Business.Models;

namespace CompanyExchangeApp.Business.Mapper
{
    public class SymbolMapper
    {
        public static IList<SymbolDto> MapToSymbolDtoList(IList<Symbol> symbols)
        {
            return AutoMapperConfig.Mapper.Map<IList<SymbolDto>>(symbols);
        }
        public static Symbol MapToSymbol(SymbolDto symbol)
        {
            return AutoMapperConfig.Mapper.Map<Symbol>(symbol);
        }
    }
}
