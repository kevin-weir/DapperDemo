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
    public class ProvinceRespository : IProvinceRespository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        const string provincesSQL =
            @"SELECT *
              FROM Provinces
              INNER JOIN Countries ON Provinces.CountryId = Countries.CountryId";

        public ProvinceRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<IEnumerable<Province>> GetAll()
        {
            var provinces = await GetProvinces(
                provincesSQL,
                param: null,
                whereExpression: null,
                orderByExpression: "Provinces.ProvinceName ASC");

            return provinces;
        }

        public async Task<Province> GetById(int provinceId)
        {
            var provinces = await GetProvinces(
                provincesSQL,
                param: new { ProvinceId = provinceId },
                whereExpression: "Provinces.ProvinceId = @ProvinceId",
                orderByExpression: null);

            return provinces.FirstOrDefault();
        }

        public async Task<IEnumerable<Province>> GetByCountryId(int countryId)
        {
            var provinces = await GetProvinces(
                provincesSQL,
                param: new { CountryId = countryId },
                whereExpression: "Provinces.CountryId = @CountryId",
                orderByExpression: "Provinces.ProvinceName ASC");

            return provinces;
        }

        private async Task<IEnumerable<Province>> GetProvinces(string sql, object param = null, string whereExpression = null, string orderByExpression = null)
        {
            sql = SqlHelpers.SqlBuilder(sql, whereExpression, orderByExpression);

            var provinces = await _connection.QueryAsync<Province, Country, Province>(
                        sql,
                        (province, country) =>
                        {
                            province.Country = country;
                            return province;
                        },
                        param: param,
                        transaction: _transaction,
                        splitOn: $"{nameof(Country.CountryId)}");
           
            return provinces;
        }

        public async Task<Province> Insert(Province province)
        {
            var provinceId = await _connection.InsertAsync<Province>(province, _transaction);

            return await GetById(provinceId);
        }

        public async Task<bool> Update(Province province)
        {
            return await _connection.UpdateAsync<Province>(province, _transaction);
        }

        public async Task<bool> Delete(int provinceId)
        {
            return await _connection.DeleteAsync<Province>(new Province { ProvinceId = provinceId }, _transaction);
        }
    }
}
