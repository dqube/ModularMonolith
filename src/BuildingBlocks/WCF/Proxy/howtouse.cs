// using System.Diagnostics.Metrics;

// namespace CompanyName.MyProjectName.BuildingBlocks.Wcf;

// public interface ICountryServiceWrapper
// {
//     IEnumerable<Country> GetCountries();
//     Task<IEnumerable<Country>> GetCountriesAsync();
//     void SaveCountry(Country country);
//     Task SaveCountryAsync(Country country);
//     void Clear();
//     Task ClearAsync();
// }

// public class CountryServiceWrapper : ICountryServiceWrapper
// {
//     private readonly IWcfProxy<ICountryService> clientProxy;


// public CountryServiceWrapper(IWcfProxy<ICountryServiceChannel> proxy)
//     {
//         clientProxy = proxy;
//     }

// public IEnumerable<Country> GetCountries()
//     {
//         var request = new GetCountriesRequest();
//         var result = clientProxy.Execute(client => client.GetCountries(request));
//         return result.GetCountriesResult.AsEnumerable().Select(MapCountryDtoToCountry);
//     }

// public async Task<IEnumerable<Country>> GetCountriesAsync()
//     {
//         var request = new GetCountriesRequest();
//         var result = await clientProxy.Execute(client => client.GetCountriesAsync(request));
//         return result.GetCountriesResult.AsEnumerable().Select(MapCountryDtoToCountry);
//     }

// public void SaveCountry(Country country)
//     {
//         var request = new SaveCountryRequest(MapCountryToCountryDto(country));
//         clientProxy.Execute(client => client.SaveCountry(request));
//     }

// public async Task SaveCountryAsync(Country country)
//     {
//         var request = new SaveCountryRequest(MapCountryToCountryDto(country));
//         await clientProxy.Execute(client => client.SaveCountryAsync(request));
//     }

// public void Clear()
//     {
//         var request = new ClearRequest();
//         clientProxy.Execute(client => client.Clear(request));
//     }

// public async Task ClearAsync()
//     {
//         var request = new ClearRequest();
//         await clientProxy.Execute(client => client.ClearAsync(request));
//     }

// private CountryDto MapCountryToCountryDto(Country country)
//     {
//         return new CountryDto
//         {
//             Code = country.Code,
//             Name = country.Name
//         };
//     }
//     private Country MapCountryDtoToCountry(CountryDto country)
//     {
//         return new Country
//         {
//             Code = country.Code,
//             Name = country.Name
//         };
//     }

// }


// services.AddTransient<IWcfProxy<IPersonService>>(service => new WcfProxy<IPersonServiceChannel>(endpointsConfig.PersonService));
// services.AddTransient<IWcfProxy<ICountryServiceChannel>>(service => new WcfProxy<ICountryServiceChannel>(endpointsConfig.CountryService));
// services.AddScoped<ICountryServiceWrapper, CountryServiceWrapper>();
// services.AddScoped<IPersonServiceWrapper, PersonServiceWrapper>();