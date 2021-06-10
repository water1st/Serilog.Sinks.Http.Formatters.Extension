using Serilog;
using Serilog.Sinks.Http.BatchFormatters;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new LoggerConfiguration().WriteTo.Http("http://192.168.1.123:9600", batchFormatter: SingleLogBatchFormatter.Instance).CreateLogger();
            while (true)
            {
                var s = Console.ReadLine();



                logger.Information(s);
                logger.Debug(s);
                logger.Error(s);
                logger.Verbose(s);
            }
        }
    }
}
