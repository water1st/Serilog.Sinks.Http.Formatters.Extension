using Serilog.Events;
using Serilog.Formatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serilog.Sinks.Http.BatchFormatters
{
    public class SingleLogBatchFormatter : IBatchFormatter
    {
        private readonly static IBatchFormatter instance = new SingleLogBatchFormatter();
        public static IBatchFormatter Instance => instance;

        public void Format(IEnumerable<LogEvent> logEvents, ITextFormatter formatter, TextWriter output)
        {
            if (logEvents == null) throw new ArgumentNullException(nameof(logEvents));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            IEnumerable<string> formattedLogEvents = logEvents.Select(
                logEvent =>
                {
                    var writer = new StringWriter();
                    formatter.Format(logEvent, writer);
                    return writer.ToString();
                });

            Format(formattedLogEvents, output);
        }

        public void Format(IEnumerable<string> logEvents, TextWriter output)
        {
            if (logEvents == null) throw new ArgumentNullException(nameof(logEvents));
            if (output == null) throw new ArgumentNullException(nameof(output));

            if (logEvents.Count() == 1)
            {
                var logEvent = logEvents.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(logEvent))
                    output.Write(logEvent);
            }
            else
            {
                output.Write("{\"events\":[");
                var delimStart = false;
                foreach (var logEvent in logEvents)
                {
                    if (string.IsNullOrWhiteSpace(logEvent))
                        continue;

                    if (delimStart)
                        output.Write(',');

                    output.Write(logEvent);
                    delimStart = true; ;
                }

                output.Write("]}");
            }
        }
    }
}
