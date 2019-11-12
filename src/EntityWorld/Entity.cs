using System;
using System.Drawing;
using EntityWorld.Instructions;
using EntityWorld.Interfaces;

namespace EntityWorld
{
    public class Entity
    {
        private readonly int _maxFood;
       
        public Entity(WorldInfo worldInfo, WorldCreationParameters parameters, EntityMetadata metadata, Point location)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            WorldInfo = worldInfo ?? throw new ArgumentNullException(nameof(worldInfo));
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            
            //Start out with a full stomach
            FoodLevel = parameters.MaxFood;

            //Get the start position
            StartLocation = Location = location;

            _maxFood = parameters.MaxFood;
            
            State = new EntityState(metadata.Instructions, metadata.Memory);
        }
        
        public EntityState State { get; }

        public WorldInfo WorldInfo { get; }

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
            if (State.InstructionIndex >= State.Instructions.Length)
            {
                //Go back to the beginning.
                State.InstructionIndex = 0;

                return;
            }

            //Get the instruction type at the current address
            var instructionType = State.Instructions.Get(State.InstructionIndex);

            //Get the instruction from the factory
            var instruction = InstructionFactory.Get(instructionType);

            //Check to see if that was a valid instruction
            if (instruction == null)
            {
               //Not a valid instruction - we either have to move forward or something
               State.InstructionIndex++;

               return;
            }

            //Make sure there is enough room to get the parameters
            if (State.InstructionIndex + instruction.Size > State.Instructions.Length)
            {
                State.InstructionIndex = 0;
                
                return;
            }

            //Get the parameters
            byte[] parameters = State.Instructions.Get(State.InstructionIndex + 1, instruction.Size - 1);
            
            //Execute the instruction
            instruction.Execute(State, parameters);

//            Size? delta = null;
//
//            //Get the current instruction
//            var instruction = Metadata.Instructions[_instructionIndex];
//
//            _instructionIndex++;
//
//            switch (instruction)
//            {
//                case Instruction.GoDown:
//                    delta = new Size(0, 1);
//                    break;
//
//                case Instruction.GoLeft:
//                    delta = new Size(-1, 0);
//                    break;
//
//                case Instruction.GoRight:
//                    delta = new Size(1, 0);
//                    break;
//
//                case Instruction.GoUp:
//                    delta = new Size(0, -1);
//                    break;
//
//                case Instruction.DoIfFoodDown:
//
//                    if (!WorldInfo.Food.IsRectangleBelow(Location))
//                    {
//                        _instructionIndex++;
//                    }
//
//                    break;
//
//                case Instruction.DoIfFoodLeft:
//
//                    if (!WorldInfo.Food.IsRectangleLeft(Location))
//                    {
//                        _instructionIndex++;
//                    }
//
//                    break;
//
//                case Instruction.DoIfFoodRight:
//
//                    if (!WorldInfo.Food.IsRectangleRight(Location))
//                    {
//                        _instructionIndex++;
//                    }
//
//                    break;
//
//                case Instruction.DoIfFoodUp:
//
//                    if (!WorldInfo.Food.IsRectangleAbove(Location))
//                    {
//                        _instructionIndex++;
//                    }
//
//                    break;
//
//                default:
//                    throw new Exception($"Invalid instruction '{instruction}' at {_instructionIndex}.");
//            }
//
//            //Wrap around to the first instruction if we're past program memory
//            if (_instructionIndex >= Metadata.Instructions.Length)
//            {
//                _instructionIndex = 0;
//            }
//
//            if (delta != null)
//            {
//                Location += delta.Value;
//            }
        }

        private void ProcessFood()
        {
            //Determine if we're in the food zone
            var isInFood = WorldInfo.Food.Contains(Location);

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

        public Point StartLocation { get; }

        public Point Location { get; private set; }

        /// <summary>
        /// True if the entity is alive, false otherwise.
        /// </summary>
        public bool IsAlive { get; private set; } = true;
    }
}