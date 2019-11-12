namespace EntityWorld.Instructions
{
    public class NotInstruction : InstructionBase
    {
        public NotInstruction() : base(InstructionTypes.Not, 3)
        {
        }

        public override void Execute(EntityState state, byte[] parameters)
        {
            byte address = parameters[0];
            
            state.Memory.Set(address, (byte)~state.Memory.Get(address));
            
            state.InstructionIndex += Size;
        }
    }
}