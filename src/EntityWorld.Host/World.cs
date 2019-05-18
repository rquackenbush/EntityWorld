using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace EntityWorld.Host
{
    public class World
    {
        public World(WorldCreationContext context)
        {
            Size = context.WorldSize;
            Food = context.Food;

            //Allocate a place for the entities
            Entities = new Entity[context.NumberOfEntities];

            //Create the entities
            for (int entityIndex = 0; entityIndex < context.NumberOfEntities; entityIndex++)
            {
                var metadata = new EntityMetadata
                {
                    Generation = 0,
                    Instructions = new Instruction[context.NumberOfInstructions]
                };

                //Create the instructions
                for (int instructionIndex = 0; instructionIndex < context.NumberOfInstructions; instructionIndex++)
                {
                    //Create the instruction
                    metadata.Instructions[instructionIndex] = (Instruction) context.Random.Next(0, (int) Instruction.SkipIfFoodRight);
                }

                //Create the entity
                var entity = new Entity(this, context, metadata);

                //Save the entity
                Entities[entityIndex] = entity;
            }
        }

        public Entity[] Entities { get; }

        /// <summary>
        /// The size of the world
        /// </summary>
        public Size Size { get; }

        /// <summary>
        /// The placement and size of the food
        /// </summary>
        public Rectangle Food { get; }

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