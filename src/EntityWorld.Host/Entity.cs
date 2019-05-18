using System;
using System.Drawing;

namespace EntityWorld.Host
{
    public class Entity
    {
        private int _instructionIndex;
        private readonly int _maxFood;

        public Entity(World world, WorldCreationContext context, EntityMetadata metadata)
        {
            //Start out with a full stomach
            FoodLevel = context.MaxFood;

            //Save a reference to the world
            World = world;
            Metadata = metadata;

            //Create the position of this entity
            var x = context.Random.Next(0, context.WorldSize.Width);
            var y = context.Random.Next(0, context.WorldSize.Height);

            //Get the start position
            StartPosition = Position = new Point(x, y);

            _maxFood = context.MaxFood;
        }

        public World World { get; }

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

                    if (World.IsFoodDown(Position))
                    {
                        _instructionIndex++;
                    }

                    break;

                case Instruction.SkipIfFoodLeft:

                    if (World.IsFoodLeft(Position))
                    {
                        _instructionIndex++;
                    }

                    break;

                case Instruction.SkipIfFoodRight:

                    if (World.IsFoodRight(Position))
                    {
                        _instructionIndex++;
                    }

                    break;

                case Instruction.SkipIfFoodUp:

                    if (World.IsFoodUp(Position))
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
            var isInFood = World.Food.Contains(Position);

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