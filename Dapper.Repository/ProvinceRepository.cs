using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Models;
using Dapper.Repository.Interfaces;
using Dapper.Domain.Models;
using Dapper.Repository.Helpers;

namespace Dapper.Repository
{
    public class ProvinceRespository : IProvinceRespository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        const string provinceSQL =
            @"SELECT *
              FROM Province";

        public ProvinceRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task<IEnumerable<ProvinceDtoQuery>> GetAll()
        {
            var provinces = await GetProvinces(
                provinceSQL,
                param: null,
                whereExpression: null,
                orderByExpression: "Province.ProvinceName ASC");

            return provinces;
        }

        public async Task<ProvinceDtoQuery> GetById(int provinceId)
        {
            var provinces = await GetProvinces(
                provinceSQL,
                param: new { ProvinceId = provinceId },
                whereExpression: "Province.ProvinceId = @ProvinceId",
                orderByExpression: null);

            return provinces.FirstOrDefault();
        }

        public async Task<IEnumerable<ProvinceDtoQuery>> GetByCountryId(int countryId)
        {
            var provinces = await GetProvinces(
                provinceSQL,
                param: new { CountryId = countryId },
                whereExpression: "Province.CountryId = @CountryId",
                orderByExpression: "Province.ProvinceName ASC");

            return provinces;
        }

        private async Task<IEnumerable<ProvinceDtoQuery>> GetProvinces(string sql, object param = null, string whereExpression = null, string orderByExpression = null)
        {
            sql = SqlHelpers.SqlBuilder(sql, whereExpression, orderByExpression);

            var provinces = await connection.QueryAsync<ProvinceDtoQuery>(
                sql, 
                param: param, 
                transaction: transaction);
            
            return provinces;
        }

        public async Task<ProvinceDtoQuery> Insert(Province province)
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

        public async Task<Province> GetEntityById(int provinceId)
        {
            return await connection.GetAsync<Province>(provinceId);
        }
    }
}
