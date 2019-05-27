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

            //Create the random number generator
            var randomNumberGenerator = new RandomNumberGenerator();

            //Create the world factory
            var worldFactory = new WorldFactory(randomNumberGenerator);

            Entity[] survivingEntities = null;

            for (int generationIndex = 0; generationIndex < 100; generationIndex++)
            {
                Console.WriteLine($"=== Generation {generationIndex} ===");

                var parameters = new WorldCreationParameters
                {
                    ExistingEntities = survivingEntities?
                        .Select(e => e.Metadata)
                        .ToArray(),
                    NumberOfEntities = 5000
                };

                //Create the world
                var world = worldFactory.Create(parameters);

                Console.WriteLine($"World created. Food location: ({world.WorldState.Food.X}, {world.WorldState.Food.Y})");

                if (_cts.IsCancellationRequested)
                {
                    Console.WriteLine("Cancelled.");

                    return;
                }

                survivingEntities = await ExecuteGenerationAsync(world, _cts.Token);

                //Print out the survivors
                if (survivingEntities != null)
                {
                    foreach (var aliveEntity in survivingEntities)
                    {
                        var distance = aliveEntity.StartLocation.GetDistance(aliveEntity.Location);

                        Console.WriteLine($"  Entity traveled {distance:0.00}: Gen [{aliveEntity.Metadata.Generation}]");
                    }
                }
            }

        }

        static async Task<Entity[]> ExecuteGenerationAsync(World world, CancellationToken token)
        {
            //Cycle the world
            for (int cycleIndex = 0; cycleIndex < 2000; cycleIndex++)
            {
                //Check to see if we've cancelled
                token.ThrowIfCancellationRequested();

                //Perform a cycle
                await world.CycleAsync(token);
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
            return aliveEntities;
        }

    }
}