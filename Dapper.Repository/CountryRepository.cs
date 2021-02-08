using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Models;
using Dapper.Repository.Interfaces;
using Dapper.Domain.Models;

namespace Dapper.Repository
{
    public class CountryRespository : ICountryRespository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        const string countrySQL =
            @"SELECT *
              FROM Country";

        public CountryRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task<IEnumerable<CountryDtoQuery>> GetAll()
        {
            return await GetCountries(countrySQL, param: null);
        }

        public async Task<CountryDtoQuery> GetById(int countryId)
        {
            var sql = countrySQL + " WHERE Country.CountryId = @CountryId";

            return (await GetCountries(sql, param: new { CountryId = countryId })).FirstOrDefault();
        }

        private async Task<IEnumerable<CountryDtoQuery>> GetCountries(string sql, object param = null)
        {
            var countries = await connection.QueryAsync<CountryDtoQuery>(
                sql, 
                param: param, 
                transaction: transaction);
            
            return countries;
        }

        public async Task<CountryDtoQuery> Insert(Country country)
        {
            var countryId = await connection.InsertAsync<Country>(country, transaction);

            return await GetById(countryId);
        }

        public async Task<bool> Update(Country country)
        {
            return await connection.UpdateAsync<Country>(country, transaction);
        }

        public async Task<bool> Delete(int countryId)
        {
            return await connection.DeleteAsync<Country>(new Country { CountryId = countryId }, transaction);
        }

        public async Task<Country> GetEntityById(int countryId)
        {
            return await connection.GetAsync<Country>(countryId);
        }


    }
}
