using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Models;
using Dapper.Repository.Services;
using Dapper.Repository.Helpers;

namespace Dapper.Repository
{
    public class CountryRespository : ICountryRespository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        const string countriesSQL =
            @"SELECT *
              FROM Countries";

        public CountryRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            var countries = await GetCountries(
                countriesSQL,
                param: null,
                whereExpression: null,
                orderByExpression: "Countries.CountryName ASC");

            return countries;
        }

        public async Task<Country> GetById(int countryId)
        {
            var countries = await GetCountries(
                countriesSQL,
                param: new { CountryId = countryId },
                whereExpression: "Countries.CountryId = @CountryId",
                orderByExpression: null);

            return countries.FirstOrDefault();
        }

        private async Task<IEnumerable<Country>> GetCountries(string sql, object param = null, string whereExpression = null, string orderByExpression = null)
        {
            sql = SqlHelpers.SqlBuilder(sql, whereExpression, orderByExpression);

            var countries = await _connection.QueryAsync<Country>(
                sql, 
                param: param, 
                transaction: _transaction);
            
            return countries;
        }

        public async Task<Country> Insert(Country country)
       {
            var countryId = await _connection.InsertAsync<Country>(country, _transaction);

            return await GetById(countryId);
        }

        public async Task<bool> Update(Country country)
        {
            return await _connection.UpdateAsync<Country>(country, _transaction);
        }

        public async Task<bool> Delete(int countryId)
        {
            return await _connection.DeleteAsync<Country>(new Country { CountryId = countryId }, _transaction);
        }
    }
}
