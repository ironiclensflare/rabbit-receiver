using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace rabbit_receiver.log
{
    public static class Logger
    {
        public static ILog GetLogger()
        {
            var logger = LogManager.GetLogger(typeof(Program));
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var consoleAppender = new ConsoleAppender();
            var hierarchy = (Hierarchy)logRepository;
            hierarchy.Root.AddAppender(consoleAppender);
            var layout = new PatternLayout { ConversionPattern = "%date %-5level %logger - %message%newline" };
            layout.ActivateOptions();
            consoleAppender.Layout = layout;
            hierarchy.Configured = true;
            return logger;
        }
    }
}