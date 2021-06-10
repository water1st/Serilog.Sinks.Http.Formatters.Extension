# Serilog.Sinks.Http.Formatters.Extension

### Getting started
To use the cluster information formatter, first install the [NuGet package](https://www.nuget.org/packages/Serilog.Sinks.Http.Formatters.Extension/):.

```powershell
Install-Package Serilog.Sinks.Http.Formatters.Extension
``` 

Next, config the formatter
```csharp
services.AddLogging(logBuilder => {
    var logger = new LoggerConfiguration()
        .WriteTo.Http("http://localhost:8080", batchPostingLimit: 1, batchFormatter: Serilog.Sinks.Http.BatchFormatters.SingleLogBatchFormatter.Instance)
        .CreateLogger();

    logBuilder.ClearProviders();
    logBuilder.AddSerilog(logger);
});
```
or config in appsettings.json
```json
{
  "Serilog": {
    "MinimumLevel": "Verbose",
    "WriteTo": [
      {
        "Name": "Http",
        "Args": {
          "requestUri": "http://localhost:8080",
          "batchPostingLimit": "1",
          "batchFormatter": "Serilog.Sinks.Http.BatchFormatters.SingleLogBatchFormatter::Instance, Serilog.Sinks.Http.Formatters.Extension"
        }
      }
    ]
  }
}
```
