namespace EntityWorld
{
    public class EntityState
    {
        public EntityState(byte[] instructions, byte[] memory)
        {
            Instructions = new Memory<byte>(instructions);
            Memory = new Memory<byte>(memory);
        }
        
        public Memory<byte> Memory { get; }
        
        public Memory<byte> Instructions { get; }
        
        public int InstructionIndex { get; set; }
    }
}