using System.Drawing;
using EntityWorld.Instructions;
using Shouldly;
using Xunit;

namespace EntityWorld.Tests
{
    public class EntityTests
    {
        //Create the world
        private readonly WorldCreationParameters _parameters = new WorldCreationParameters();
        private readonly WorldInfo _worldInfo = new WorldInfo(new Size(200, 200), new Rectangle(50, 50, 10, 10));
        private readonly Point StartingLocation = new Point(100, 100);

        [Fact]
        public void SetTest()
        {
            var metadata = new EntityMetadata
            {
                Instructions = new byte[]
                {
                    InstructionTypes.Set,
                    0,
                    42
                },
                
                Memory = new byte[]
                {
                  0
                }
            };
            
            var entity = new Entity(_worldInfo, _parameters, metadata, StartingLocation);
            
            entity.State.Memory.Get(0).ShouldBe((byte)0);
            
            entity.Cycle();
            
            entity.State.Memory.Get(0).ShouldBe((byte)42);
        }

        [Fact]
        public void CopyTest()
        {
            var metadata = new EntityMetadata
            {
                Instructions = new byte[]
                {
                    InstructionTypes.Set, 
                    0,
                    42,
                    InstructionTypes.Copy,
                    0,
                    1
                },
                
                Memory = new byte[]
                {
                    0, 
                    0
                }
            };
            
            var entity = new Entity(_worldInfo, _parameters, metadata, StartingLocation);
            
            entity.State.Memory.Get(0).ShouldBe((byte)0);
            entity.State.Memory.Get(1).ShouldBe((byte)0);
            
            entity.Cycle();
            
            entity.State.Memory.Get(0).ShouldBe((byte)42);
            entity.State.Memory.Get(1).ShouldBe((byte)0);
            
            entity.Cycle();
            
            entity.State.Memory.Get(1).ShouldBe((byte)42);
        }
    }
}