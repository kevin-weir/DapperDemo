//// The Serilog logging service makes use of the following Nuget packages:
//// Serilog
//// Serilog.AspNetCore
//// Serilog.Enrichers.Environment
//// Serilog.Enrichers.Process
//// Serilog.Enrichers.Thread
//// Serilog.Settings.Configuration
//// Serilog.Sinks.Console
//// Serilog.Sinks.File
//// Serilog.Sinks.Seq
//// Serilog.Sinks.Async
//// Serilog.Formatting.Compact  See: https://github.com/serilog/serilog-formatting-compact
//// Serilog.Sinks.ApplicationInsights  See: https://github.com/serilog/serilog-sinks-applicationinsights

//// Refer to: 
//// https://www.youtube.com/watch?v=_iryZxv8Rxw
//// https://github.com/serilog
//// https://datalust.co/seq/

//using Microsoft.AspNetCore.Builder;
//using Serilog;

//namespace ToDo.API.ApplicationBuilders
//{
//    public static class SerilogBuilder
//    {
//        public static void UseSerilogBuilder(this IApplicationBuilder app)
//        {
//            app.UseSerilogRequestLogging();
//        }
//    }
//}
