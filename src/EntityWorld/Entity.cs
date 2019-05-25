using System;
using System.Drawing;
using EntityWorld.Interfaces;

namespace EntityWorld
{
    public class Entity
    {
        private readonly WorldCreationParameters _parameters;
        private int _instructionIndex;
        private readonly int _maxFood;

        public Entity(IRandomNumberGenerator randomNumberGenerator, WorldState worldState, WorldCreationParameters parameters, EntityMetadata metadata)
        {
            WorldState = worldState ?? throw new ArgumentNullException(nameof(worldState));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));

            //Start out with a full stomach
            FoodLevel = parameters.MaxFood;

            //Create the position of this entity
            var x = randomNumberGenerator.Next(0, parameters.WorldSize.Width);
            var y = randomNumberGenerator.Next(0, parameters.WorldSize.Height);

            //Get the start position
            StartPosition = Position = new Point(x, y);

            _maxFood = parameters.MaxFood;
        }

        public WorldState WorldState { get; }

        public EntityMetadata Metadata { get; }

        /// <summary>
        /// Cycle this entity
        /// </summary>
        public void Cycle()
        {
            if (IsAlive)
            {

                ProcessFood();

                ProcessInstruction();

                //Check to see if we have any gas left in the tank
                if (FoodLevel <= 0)
                {
                    IsAlive = false;
                }
            }
        }

        private void ProcessInstruction()
        {
            Size? delta = null;

            //Get the current instruction
            var instruction = Metadata.Instructions[_instructionIndex];

            _instructionIndex++;

            switch (instruction)
            {
                case Instruction.GoDown:
                    delta = new Size(0, 1);
                    break;

                case Instruction.GoLeft:
                    delta = new Size(-1, 0);
                    break;

                case Instruction.GoRight:
                    delta = new Size(1, 0);
                    break;

                case Instruction.GoUp:
                    delta = new Size(0, -1);
                    break;

                case Instruction.SkipIfFoodDown:

                    if (WorldState.Food.IsPointBelow(Position))
                    {
                        _instructionIndex++;
                    }

                    break;

                case Instruction.SkipIfFoodLeft:

                    if (WorldState.Food.IsPointLeft(Position))
                    {
                        _instructionIndex++;
                    }

                    break;

                case Instruction.SkipIfFoodRight:

                    if (WorldState.Food.IsPointRight(Position))
                    {
                        _instructionIndex++;
                    }

                    break;

                case Instruction.SkipIfFoodUp:

                    if (WorldState.Food.IsPointAbove(Position))
                    {
                        _instructionIndex++;
                    }

                    break;

                default:
                    throw new Exception($"Invalid instruction '{instruction}' at {_instructionIndex}.");
            }

            //Wrap around to the first instruction if we're past program memory
            if (_instructionIndex >= Metadata.Instructions.Length)
            {
                _instructionIndex = 0;
            }

            if (delta != null)
            {
                Position += delta.Value;
            }
        }

        private void ProcessFood()
        {
            //Determine if we're in the food zone
            var isInFood = WorldState.Food.Contains(Position);

            if (isInFood)
            {
                if (FoodLevel < _maxFood)
                {
                    FoodLevel++;
                }
            }
            else
            {
                if (FoodLevel > 0)
                {
                    FoodLevel -= 1;
                }
            }
        }

        public int FoodLevel { get; private set; }

        public Point StartPosition { get; }

        public Point Position { get; private set; }

        /// <summary>
        /// True if the entity is alive, false otherwise.
        /// </summary>
        public bool IsAlive { get; private set; } = true;
    }
}