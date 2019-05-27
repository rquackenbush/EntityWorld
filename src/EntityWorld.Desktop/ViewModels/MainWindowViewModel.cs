using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ReactiveUI;

namespace EntityWorld.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _greeting = "Hello World!";

        public MainWindowViewModel()
        {
            RunCommand = ReactiveCommand.Create(Run);
        }

        public ICommand RunCommand { get; }

        private void Run()
        {
            Greeting = "Running";
        }

        public string Greeting
        {
            get => _greeting;
            set
            {
                _greeting = value;
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
