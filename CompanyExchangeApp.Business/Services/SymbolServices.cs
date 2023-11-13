using CompanyExchangeApp.Business.Dtos;
using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Mapper;
using CompanyExchangeApp.Business.Models;
using CompanyExchangeApp.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Business.Services
{
    public class SymbolServices: ISymbolService
    {
        private string? _dbConnectionString;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ISymbolRepository _symbolRepository;
        private readonly ITypeRepository _typeRepository;

        public SymbolServices(IExchangeRepository exchangeRepository, ISymbolRepository symbolRepository, ITypeRepository typeRepository)
        {
            _exchangeRepository = exchangeRepository;
            _symbolRepository = symbolRepository;
            _typeRepository = typeRepository;
        }
        public async Task<IList<ExchangeDto>> GetExchangesAsync()
        {
            try
            {
                // Retrieve exchanges from the repository
                IList<Exchange> exchanges = await _exchangeRepository.GetExchangesAsync();

                // Map exchanges to ExchangeDto using the mapping class
                IList<ExchangeDto> exchangesDto = ExchangeMapper.MapToExchangeDtoList(exchanges);

                // Return the mapped DTOs
                return exchangesDto;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred in SymbolService: {ex.Message}");
                return new List<ExchangeDto>();
            }
        }

        public async Task<IList<SymbolDto>> GetAllSymbolsAsync(TypeDto? type = null, ExchangeDto? exchange = null)
        {
            try
            {
                // Retrieve symbols from the repository
                IList<Symbol> symbols = await _symbolRepository.GetAllSymbolsAsync(type?.Name,exchange?.Name);

                // Map symbols to SymbolsDto using the mapping class
                IList<SymbolDto> symbolsDto = SymbolMapper.MapToSymbolDtoList(symbols);

                return symbolsDto;
                
            }
            catch (Exception ex)
            {
                // Handling exceptions and logging an error message
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Returning an empty list of SymbolDto in case of an error
                return new List<SymbolDto>();
            }
        }

        public async Task <IList<TypeDto>> GetTypesAsync()
        {
            try
            {
                // Retrieve types from the repository
                IList<Type> types = await _typeRepository.GetTypesAsync();

                // Map exchanges to TypeDto using the mapping class
                IList<TypeDto> typesDto = TypeMapper.MapToTypeDtoList(types);

                // Return the mapped DTOs
                return typesDto;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred in SymbolService: {ex.Message}");
                return new List<TypeDto>();
            }
        }

        public void SetDbConnectionString(string connectionString)
        {
            _dbConnectionString = connectionString;
            _exchangeRepository.SetDbString(connectionString);
            _typeRepository.SetDbString(connectionString);
            _symbolRepository.SetDbString(connectionString);
        }

        public async Task SaveSymbolAsync(SymbolDto symbolDto)
        {
            try
            {
                Symbol symbol = SymbolMapper.MapToSymbol(symbolDto);
                await _symbolRepository.SaveSymbolAsync(symbol);                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving symbol: {ex.Message}");
            }
        }

        public async Task DeleteSymbolAsync(SymbolDto symbolDto)
        {
            try
            {
                if (symbolDto == null || symbolDto.Id == null)
                {
                    // Handle the case where the symbol or its ID is null
                    return;
                }

                Symbol symbol = SymbolMapper.MapToSymbol(symbolDto);
                await _symbolRepository.DeleteSymbolAsync(symbol);
                
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly (logging, etc.)
                Console.WriteLine($"Error deleting symbol: {ex.Message}");
            }
        }

    }
}
