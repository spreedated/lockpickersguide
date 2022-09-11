using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Serilog;
using Serilog.Events;

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
