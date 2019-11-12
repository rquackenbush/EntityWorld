namespace EntityWorld.Instructions
{
    public abstract class InstructionBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instructionType"></param>
        /// <param name="size">The size of the instruction (including the instructionType)</param>
        protected InstructionBase(byte instructionType, byte size)
        {
            InstructionType = instructionType;
            Size = size;
        }
        
        public byte InstructionType { get; }
        
        public byte Size { get; }

        public abstract void Execute(EntityState state, byte[] parameters);
    }
}