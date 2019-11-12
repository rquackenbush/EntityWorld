using System;
using System.Collections.Generic;
using System.Drawing;
using EntityWorld.Instructions;
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
            var worldState = new WorldInfo(parameters.WorldSize, food);

            //Create the entities
            var entities = CreateEntities(parameters, worldState);

            //Create the world! Mwah hah hah!
            return new World(worldState, entities);
        }

        private Point GenerateEntityLocation(WorldCreationParameters parameters)
        {
            //Create the position of this entity
            var x = _randomNumberGenerator.Next(0, parameters.WorldSize.Width);
            var y = _randomNumberGenerator.Next(0, parameters.WorldSize.Height);

            return new Point(x, y);
        }

        private Entity[] CreateEntities(WorldCreationParameters parameters, WorldInfo worldInfo)
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
                    var entity = new Entity(worldInfo, parameters, existingEntity, GenerateEntityLocation(parameters));

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
                var metadata = GenerateEntityMetadata(parameters, 0);
                
                //Create the entity
                var entity = new Entity(worldInfo, parameters, metadata, GenerateEntityLocation(parameters));

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

        private EntityMetadata GenerateEntityMetadata(WorldCreationParameters parameters, int generation)
        {
            //var instructions = new Instruction[parameters.NumberOfInstructions];

            //Create the instructions
//            for (int instructionIndex = 0; instructionIndex < parameters.NumberOfInstructions; instructionIndex++)
//            {
//                //Create the instruction
//                instructions[instructionIndex] = (Instruction) _randomNumberGenerator.Next(0, (int) Instruction.DoIfFoodRight + 1);
//            }
//
//            return instructions;

            return new EntityMetadata
            {
                Instructions = new byte[]
                {
                    // Initialize
                    InstructionTypes.Set, 4, 0,
                    InstructionTypes.Set, 5, 0,
                    InstructionTypes.Set, 6, 0,
                    InstructionTypes.Set, 7, 0,
                    
                    //Up
                    
                    // Down
                    
                    // Left
                    
                    // Right
                    
                    
                },
                
                Memory = new byte[]
                {
                    0, //[0] Is food up?
                    0, //[1] Is food down
                    0, //[2] Is food left?
                    0, //[3] Is food right?
                    0, //[4] Move up
                    0, //[5] Move down
                    0, //[6] Move left
                    0  //[7] Move right
                }
            };

//            return new byte[]
//            {
//                //InstructionTypes.Copy
//            };

//              return new []
//              {
//                  Instruction.DoIfFoodDown,
//                  Instruction.GoDown,
//
//                  Instruction.DoIfFoodUp,
//                  Instruction.GoUp,
//
//                  Instruction.DoIfFoodLeft,
//                  Instruction.GoLeft,
//
//                  Instruction.DoIfFoodRight,
//                  Instruction.GoRight
//              };
        }
    }
}