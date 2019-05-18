using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityWorld.Host
{
    class Program
    {
        private static readonly CancellationTokenSource _cts = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;

                _cts.Cancel();

                Console.WriteLine("Cancellation requested...");
            };

            for (int generationIndex = 0; generationIndex < 100; generationIndex++)
            {
                Console.WriteLine($"=== Generation {generationIndex} ===");

                if (_cts.IsCancellationRequested)
                {
                    Console.WriteLine("Cancelled.");

                    return;
                }

                var entities = await ExecuteGenerationAsync(new EntityMetadata[]{}, _cts.Token);
            }
        }

        static async Task<EntityMetadata[]> ExecuteGenerationAsync(EntityMetadata[] entityMetadatas, CancellationToken token)
        {
            //The size of the world
            var worldSize = new Size(800, 600);

            //The position of the food
            var food = new Rectangle(100, 100, 20, 20);

            var context = new WorldCreationContext(1000, 20, worldSize, 1000, food);

            Console.WriteLine("World created");

            //Create the world
            var world = new World(context);

            //Cycle the world
            for (int cycleIndex = 0; cycleIndex < 1000; cycleIndex++)
            {
                //Perform a cycle
                await world.CycleAsync();
            }

            //Get the number of alive entities
            var aliveEntities = world.Entities
                .Where(e => e.IsAlive)
                .ToArray();

            //get the dead count
            var deadCount = world.Entities.Length - aliveEntities.Length;

            //Output the result.
            Console.WriteLine($"Out of {world.Entities.Length} entities, {aliveEntities.Length} are alive and {deadCount} are dead.");

            //Return the alive entities metadata!
            return aliveEntities
                .Select(e => e.Metadata)
                .ToArray();

//            foreach (var aliveEntity in aliveEntities)
//            {
//                var distance = aliveEntity.StartPosition.GetDistance(aliveEntity.Position);
//
//                Console.WriteLine($"  Entity traveled {distance:0.00}");
//            }
        }

    }
}