namespace EntityWorld.Instructions
{
    public class CopyInstruction : InstructionBase
    {
        public CopyInstruction() : base(InstructionTypes.Copy, 3)
        {
        }

        public override void Execute(EntityState state, byte[] parameters)
        {
            byte sourceAddress = parameters[0];
            byte targetAddress = parameters[1];

            byte sourceValue = state.Memory.Get(sourceAddress);
            
            state.Memory.Set(targetAddress, sourceValue);

            state.InstructionIndex += Size;
        }
    }
}