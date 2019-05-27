using System;
using System.Windows.Input;
using ReactiveUI;

namespace EntityWorld.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private WorldViewModel _world;

        public MainWindowViewModel()
        {
            var randomNumberGenerator = new RandomNumberGenerator();

            var parameters = new WorldCreationParameters
            {
                NumberOfEntities = 10
            };

            var factory = new WorldFactory(randomNumberGenerator);

            var world = factory.Create(parameters);

            World = new WorldViewModel(world);

            RunCommand = ReactiveCommand.Create(Run);
        }

        public ICommand RunCommand { get; }

        private async void Run()
        {
            for (int iteration = 0; iteration < 10; iteration++)
            {
                await World.World.CycleAsync();
            }

            foreach (var entity in World.Entities)
            {
                entity.UpdateLocation();
            }
        }

        public WorldViewModel World
        {
            get => _world;
            set
            {
                _world = value;
                this.RaisePropertyChanged();
            }
        }

//        DoTheThing = ReactiveCommand.Create(RunTheThing);
//    }
//
//    public ReactiveCommand<Unit, Unit> DoTheThing { get; }
//
//    void RunTheThing()
//    {
//    // Code for executing the command here.
//    }
    }
}
