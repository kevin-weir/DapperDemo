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
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        const string provinceSQL =
            @"SELECT *
              FROM Province
              INNER JOIN Country ON Province.CountryId = Country.CountryId";

        public ProvinceRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task<IEnumerable<Province>> GetAll()
        {
            var provinces = await GetProvinces(
                provinceSQL,
                param: null,
                whereExpression: null,
                orderByExpression: "Province.ProvinceName ASC");

            return provinces;
        }

        public async Task<Province> GetById(int provinceId)
        {
            var provinces = await GetProvinces(
                provinceSQL,
                param: new { ProvinceId = provinceId },
                whereExpression: "Province.ProvinceId = @ProvinceId",
                orderByExpression: null);

            return provinces.FirstOrDefault();
        }

        public async Task<IEnumerable<Province>> GetByCountryId(int countryId)
        {
            var provinces = await GetProvinces(
                provinceSQL,
                param: new { CountryId = countryId },
                whereExpression: "Province.CountryId = @CountryId",
                orderByExpression: "Province.ProvinceName ASC");

            return provinces;
        }

        private async Task<IEnumerable<Province>> GetProvinces(string sql, object param = null, string whereExpression = null, string orderByExpression = null)
        {
            sql = SqlHelpers.SqlBuilder(sql, whereExpression, orderByExpression);

            var provinces = await connection.QueryAsync<Province, Country, Province>(
                        sql,
                        (province, country) =>
                        {
                            province.Country = country;
                            return province;
                        },
                        param: param,
                        transaction: transaction,
                        splitOn: $"{nameof(Country.CountryId)}");
           
            return provinces;
        }

        public async Task<Province> Insert(Province province)
        {
            var provinceId = await connection.InsertAsync<Province>(province, transaction);

            return await GetById(provinceId);
        }

        public async Task<bool> Update(Province province)
        {
            return await connection.UpdateAsync<Province>(province, transaction);
        }

        public async Task<bool> Delete(int provinceId)
        {
            return await connection.DeleteAsync<Province>(new Province { ProvinceId = provinceId }, transaction);
        }
    }
}
