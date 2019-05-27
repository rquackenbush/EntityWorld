using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace EntityWorld.Desktop.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

//            var presenter = new ItemsControl();
//
//            presenter.ItemsPanel

            //var canvas = new Canvas();

//            this.KeyBindings.Add(new KeyBinding()
//            {
//                Gesture = new KeyGesture()
//            });
        }
    }
}