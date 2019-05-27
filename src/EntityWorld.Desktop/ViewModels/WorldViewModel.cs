using System;
using System.Drawing;
using System.Linq;
using EntityWorld.Desktop.ViewModels;
using JetBrains.Annotations;

namespace EntityWorld.Desktop.ViewModels
{
    public class WorldViewModel : ViewModelBase
    {
        public World World { get; }

        public WorldViewModel([NotNull] World world)
        {
            World = world ?? throw new ArgumentNullException(nameof(world));

            Entities = world.Entities
                .Select(e => new EntityViewModel(e))
                .ToArray();
        }

        public EntityViewModel[] Entities { get; }

        public Size Size => World.WorldState.Size;

        public Rectangle Food => World.WorldState.Food;
    }
}