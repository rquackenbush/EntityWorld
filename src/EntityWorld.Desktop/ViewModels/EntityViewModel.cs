using System;
using System.Drawing;
using EntityWorld.Desktop.ViewModels;
using JetBrains.Annotations;
using ReactiveUI;

namespace EntityWorld.Desktop.ViewModels
{
    public class EntityViewModel : ViewModelBase
    {
        private readonly Entity _entity;

        public EntityViewModel([NotNull] Entity entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        public void UpdateLocation()
        {
            this.RaisePropertyChanged(nameof(Location));
            this.RaisePropertyChanged(nameof(FoodLevel));
        }

        public Point Location => _entity.Location;

        public int FoodLevel => _entity.FoodLevel;

    }
}