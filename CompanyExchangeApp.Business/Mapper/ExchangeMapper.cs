using CompanyExchangeApp.Business.Dtos;
using CompanyExchangeApp.Business.Models;

namespace CompanyExchangeApp.Business.Mapper
{
    public class ExchangeMapper
    {
        public static IList<ExchangeDto> MapToExchangeDtoList(IList<Exchange> exchanges)
        {
            return AutoMapperConfig.Mapper.Map<IList<ExchangeDto>>(exchanges);
        }
        public static Exchange MapToType(ExchangeDto exchange)
        {
            return AutoMapperConfig.Mapper.Map<Exchange>(exchange);
        }
    }

}
