using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.Markup.Xaml;
using Serilog;

namespace EntityWorld.Desktop
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

#if DEBUG
            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                //.WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .WriteTo.Console(outputTemplate: "{Area}: {Message}" + Environment.NewLine)
                .CreateLogger());
#endif
        }


   }
}