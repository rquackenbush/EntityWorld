using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace EntityWorld
{
    public class World
    {
        public World(WorldInfo worldInfo, Entity[] entities)
        {
            WorldInfo = worldInfo;
            Entities = entities ?? throw new ArgumentNullException(nameof(entities));
        }

        public WorldInfo WorldInfo { get; }

        public Entity[] Entities { get; }

        /// <summary>
        /// Cycle the world. Once.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task CycleAsync(CancellationToken token = default)
        {
            foreach (var entity in Entities)
            {
                entity.Cycle();
            }

            return Task.CompletedTask;
        }
    }
}