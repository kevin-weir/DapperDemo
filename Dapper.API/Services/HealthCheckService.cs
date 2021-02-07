//// This service uses the following Nuget patches to provide API health check functionality
//// Microsoft.Extensions.Diagnostics.HealthChecks
//// AspNetCore.HealthChecks.SqlServer

//// Refer to:
//// https://docs.microsoft.com/en-ca/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.0

//using System;
//using System.Net.Http;
//using System.Net.NetworkInformation;
//using System.Net.Sockets;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Diagnostics.HealthChecks;
//using ToDo.API.Helpers;

//namespace ToDo.API.Services
//{
//    public static class HealthCheckService
//    {
//        public static void AddHealthCheckService(this IServiceCollection services, IConfiguration configuration)
//        {
//            var healthCheckSettings = configuration.GetSection(nameof(HealthCheckSettings)).Get<HealthCheckSettings>();

//            if (healthCheckSettings.HttpHealthChecks != null && healthCheckSettings.HttpHealthChecks.Count > 0) 
//            {
//                foreach (var httpCheck in healthCheckSettings.HttpHealthChecks)
//                {
//                    services.AddHealthChecks().AddCheck(httpCheck.Name, 
//                        new HttpHealthCheck(httpCheck.Url, httpCheck.TimeoutMilliseconds));
//                }
//            }

//            if (healthCheckSettings.PingHealthChecks != null && healthCheckSettings.PingHealthChecks.Count > 0)
//            {
//                foreach (var pingCheck in healthCheckSettings.PingHealthChecks)
//                {
//                    services.AddHealthChecks().AddCheck(pingCheck.Name, 
//                        new PingHealthCheck(pingCheck.Host, pingCheck.TimeoutMilliseconds));
//                }
//            }

//            if (healthCheckSettings.PortHealthChecks != null && healthCheckSettings.PortHealthChecks.Count > 0)
//            {
//                foreach (var portCheck in healthCheckSettings.PortHealthChecks)
//                {
//                    services.AddHealthChecks().AddCheck(portCheck.Name, 
//                        new PortHealthCheck(portCheck.Host, portCheck.Port, portCheck.TimeoutMilliseconds));
//                }
//            }

//            if (healthCheckSettings.SqlServerHealthChecks != null && healthCheckSettings.SqlServerHealthChecks.Count > 0)
//            {
//                foreach (var sqlServerCheck in healthCheckSettings.SqlServerHealthChecks)
//                {
//                    services.AddHealthChecks().AddSqlServer(configuration[sqlServerCheck.ConnectionString], name: sqlServerCheck.Name);
//                }
//            }
//        }
//    }

//    public class HttpHealthCheck : IHealthCheck
//    {
//        private readonly string _url;
//        private readonly int _timeoutMilliseconds;

//        public HttpHealthCheck(string url, int timeoutMilliseconds)
//        {
//            _url = url;
//            _timeoutMilliseconds = timeoutMilliseconds;
//        }

//        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                using var client = new HttpClient
//                {
//                    Timeout = TimeSpan.FromMilliseconds(_timeoutMilliseconds)
//                };

//                var response = await client.GetAsync(new Uri(_url), HttpCompletionOption.ResponseHeadersRead, cancellationToken);

//                if (response == null || !response.IsSuccessStatusCode)
//                {
//                    return HealthCheckResult.Unhealthy();
//                }

//                return HealthCheckResult.Healthy();
//            }
//            catch
//            {
//                return HealthCheckResult.Unhealthy();
//            }
//        }
//    }

//    public class PingHealthCheck : IHealthCheck
//    {
//        private readonly string _host;
//        private readonly int _timeoutMilliseconds;

//        public PingHealthCheck(string host, int timeoutMilliseconds)
//        {
//            _host = host;
//            _timeoutMilliseconds = timeoutMilliseconds;
//        }

//        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                using var ping = new Ping();
//                var reply = await ping.SendPingAsync(_host, _timeoutMilliseconds);

//                if (reply.Status != IPStatus.Success)
//                {

//                    return HealthCheckResult.Unhealthy();
//                }

//                if (reply.RoundtripTime >= _timeoutMilliseconds)
//                {
//                    return HealthCheckResult.Degraded();
//                }

//                return HealthCheckResult.Healthy();
//            }
//            catch
//            {
//                return HealthCheckResult.Unhealthy();
//            }
//        }
//    }

//    public class PortHealthCheck : IHealthCheck
//    {
//        private readonly string _host;
//        private readonly int _port;
//        private readonly int _timeoutMilliseconds;

//        public PortHealthCheck(string host, int port, int timeoutMilliseconds)
//        {
//            _host = host;
//            _port = port;
//            _timeoutMilliseconds = timeoutMilliseconds;
//        }

//#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
//        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
//#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
//        {
//            try
//            {
//                if (!IsPortOpen(_host, _port, _timeoutMilliseconds))
//                {
//                    return HealthCheckResult.Unhealthy();
//                }

//                return HealthCheckResult.Healthy();
//            }
//            catch
//            {
//                return HealthCheckResult.Unhealthy();
//            }
//        }

//        static bool IsPortOpen(string host, int port, int timeout)
//        {
//            try
//            {
//                using var client = new TcpClient();
//                var result = client.BeginConnect(host, port, null, null);
//                var success = result.AsyncWaitHandle.WaitOne(timeout);
//                client.EndConnect(result);
//                return success;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}
