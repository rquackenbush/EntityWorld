using System;
using System.Collections.Generic;
using System.Drawing;
using EntityWorld.Interfaces;

namespace EntityWorld
{
    public class WorldFactory
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public WorldFactory(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator ?? throw new ArgumentNullException(nameof(randomNumberGenerator));
        }

        public World Create(WorldCreationParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            //Create the food
            var food = CreateFood(parameters);

            //Create the shared world state.
            var worldState = new WorldState(parameters.WorldSize, food);

            //Create the entities
            var entities = CreateEntities(parameters, worldState);

            //Create the world! Mwah hah hah!
            return new World(worldState, entities);
        }

        private Entity[] CreateEntities(WorldCreationParameters parameters, WorldState worldState)
        {
            //Allocate space to store the entities.
            var entities = new List<Entity>(parameters.NumberOfEntities);

            //Check to see if we have any existing entities to import
            if (parameters.ExistingEntities != null)
            {
                //Consider each existing entity
                foreach (var existingEntity in parameters.ExistingEntities)
                {
                    //Increase the generation number so we can keep track of how old this entity is..
                    existingEntity.Generation++;

                    //Create the entity to wrap the metadata
                    var entity = new Entity(_randomNumberGenerator, worldState, parameters, existingEntity);

                    //Add it to the list
                    entities.Add(entity);
                }
            }

            //Determine how many entities we should create
            var numberOfEntitiesToCreate = parameters.NumberOfEntities - entities.Count;

            //Make sure we don't end up with a negative number.
            if (numberOfEntitiesToCreate < 0)
            {
                numberOfEntitiesToCreate = 0;
            }

            //Create the entities
            for (int entityIndex = 0; entityIndex < numberOfEntitiesToCreate; entityIndex++)
            {
                var metadata = new EntityMetadata
                {
                    Generation = 0,
                    Instructions = new Instruction[parameters.NumberOfInstructions]
                };

                //Create the instructions
                for (int instructionIndex = 0; instructionIndex < parameters.NumberOfInstructions; instructionIndex++)
                {
                    //Create the instruction
                    metadata.Instructions[instructionIndex] = (Instruction) _randomNumberGenerator.Next(0, (int) Instruction.SkipIfFoodRight);
                }

                //Create the entity
                var entity = new Entity(_randomNumberGenerator, worldState, parameters, metadata);

                entities.Add(entity);
            }

            return entities.ToArray();
        }

        /// <summary>
        /// We want to put the food in a random location for each iteration of the world. This method handles calculating that random position.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Rectangle CreateFood(WorldCreationParameters parameters)
        {
            //Calculate the valid range for food coordinates (we don't want to have the food out of the world)
            var xMax = parameters.WorldSize.Width - parameters.FoodSize.Width;
            var yMax = parameters.WorldSize.Height - parameters.FoodSize.Height;

            //Create the actual coordinates for the food.
            var foodX = _randomNumberGenerator.Next(0, xMax);
            var foodY = _randomNumberGenerator.Next(0, yMax);

            return new Rectangle(new Point(foodX, foodY), parameters.FoodSize);
        }
    }
}