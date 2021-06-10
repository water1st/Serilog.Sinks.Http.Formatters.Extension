using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serilog.Sinks.Http.BatchFormatters
{
    public class SingleLogBatchFormatter : DefaultBatchFormatter, IBatchFormatter
    {
        private readonly static IBatchFormatter instance = new SingleLogBatchFormatter();
        public static IBatchFormatter Instance => instance;

        public override void Format(IEnumerable<string> logEvents, TextWriter output)
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
                base.Format(logEvents, output);
            }
        }

    }
}
