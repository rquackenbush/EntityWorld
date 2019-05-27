using System.Drawing;
using Shouldly;
using Xunit;

namespace EntityWorld.Tests
{
    public class EntityTests
    {
        //Create the world
        private readonly WorldCreationParameters _parameters = new WorldCreationParameters();
        private readonly WorldState _worldState = new WorldState(new Size(200, 200), new Rectangle(50, 50, 10, 10));
        private readonly Point StartingLocation = new Point(100, 100);

        [Theory]
        [InlineData(Instruction.GoUp, 100, 99)]
        [InlineData(Instruction.GoDown, 100, 101)]
        [InlineData(Instruction.GoLeft, 99, 100)]
        [InlineData(Instruction.GoRight, 101, 100)]
        public void MoveTests(Instruction instruction, int expectedX, int expectedY)
        {
            var metadata = new EntityMetadata
            {
                Generation = 0,
                Instructions = new[]
                {
                    instruction,
                }
            };

            var entity = new Entity(_worldState, _parameters, metadata, StartingLocation);

            //Perform a single instruction cycle
            entity.Cycle();

            //Make sure that we have moved correctly
            entity.Location.ShouldBe(new Point(expectedX, expectedY));
        }
    }
}