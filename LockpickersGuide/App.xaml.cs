using Serilog;
using Serilog.Events;
using System.Windows;

namespace LockpickersGuide
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
#if DEBUG
        private LogEventLevel level = LogEventLevel.Debug;
#else
        private LogEventLevel level = LogEventLevel.Information;
#endif
        public App() : base()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.Debug(restrictedToMinimumLevel: level)
                .CreateLogger();
        }
    }
}
