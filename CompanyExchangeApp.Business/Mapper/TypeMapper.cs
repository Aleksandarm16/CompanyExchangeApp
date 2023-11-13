using CompanyExchangeApp.Business.Dtos;

namespace CompanyExchangeApp.Business.Mapper
{
    public class TypeMapper
    {
        public static IList<TypeDto> MapToTypeDtoList(IList<Models.Type> types)
        {
            return AutoMapperConfig.Mapper.Map<IList<TypeDto>>(types);
        }
        public static Models.Type  MapToType(TypeDto type)
        {
            return AutoMapperConfig.Mapper.Map<Models.Type>(type);
        }
    }
}
