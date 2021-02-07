using System;
using System.Collections.Generic;

namespace Dapper.API.Helpers
{
    #region AzureAdSettings
    public class AzureAd
    {
        public string Instance { get; set; }
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
        public string AuthorizationUrl { get; set; }
        public string Scopes { get; set; }
    }
    #endregion

    #region DistributedMemoryCacheSettings
    public class DistributedMemoryCacheSettings 
    {
        public DefaultProvider DefaultProvider { get; set; }
        public Providers Providers { get; set; }
    }

    public class DefaultProvider { 
        public string Name { get; set; }
    }

    public class Providers
    {
        public Local Local { get; set; }
        public SqlServer SqlServer { get; set; }
        public Redis Redis { get; set; }
    }

    public class Local
    {
        public string Name { get; set; }
    }

    public class SqlServer
    {
        public string Name { get; set; }
        public SqlServerCacheSettings SqlServerCacheSettings { get; set; }
    }

    public class SqlServerCacheSettings
    {
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }

    public class Redis
    {
        public string Name { get; set; }
        public RedisCacheSettings RedisCacheSettings { get; set; }
    }

    public class RedisCacheSettings
    {
        public string Configuration { get; set; }
        public string InstanceName { get; set; }
    }
    #endregion

    #region HealthCheckSettings
    public class HealthCheckSettings
    {
        public string HealthCheckRelativeUrlPath { get; set; }
        public List<HttpHealthChecks> HttpHealthChecks { get; set; }
        public List<PortHealthChecks> PortHealthChecks { get; set; }
        public List<PingHealthChecks> PingHealthChecks { get; set; }
        public List<SqlServerHealthChecks> SqlServerHealthChecks { get; set; }
    }

    public class HttpHealthChecks
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int TimeoutMilliseconds { get; set; }
    }

    public class PortHealthChecks
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int TimeoutMilliseconds { get; set; }
    }

    public class PingHealthChecks
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int TimeoutMilliseconds { get; set; }
    }

    public class SqlServerHealthChecks
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
    #endregion

    #region ApiVersionSettings
    public class ApiVersionSettings
    {
        public int DefaultMajorVersion { get; set; }
        public int DefaultMinorVersion { get; set; }
        public bool AssumeDefaultVersionWhenUnspecified { get; set; }
        public string HeaderVariable { get; set; }
        public bool ReportApiVersions { get; set; }
        public string GroupNameFormat { get; set; }
        public bool SubstituteApiVersionInUrl { get; set; }

    }
    #endregion

    #region ConnectionStrings
    public class ConnectionStrings
    {
        public string ToDo { get; set; }
        public string SqlServerCache { get; set; }
    }
    #endregion

    #region Oauth2Settings
    public class Oauth2Settings
    {
        public List<Oauth2Policies> Oauth2Policies { get; set; }
    }

    public class Oauth2Policies
    {
        public string PolicyName { get; set; }
        public List<Roles> Roles { get; set; }
    }

    public class Roles
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    #endregion

    #region NSwagSettings
    public class NSwagSettings
    {
        public string ActiveDirectoryClientID { get; set; }
        public string SecuritySchemeName { get; set; }
        public string SecuritySchemeDescription { get; set; }
        public string DocumentNamePrefix { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public Contact Contact { get; set; }
        public License License { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class License
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
    }
    #endregion
}
