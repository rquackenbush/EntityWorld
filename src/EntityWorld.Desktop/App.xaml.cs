using Avalonia;
using Avalonia.Markup.Xaml;

namespace EntityWorld.Desktop
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
   }
}