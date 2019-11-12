namespace EntityWorld.Instructions
{
    public class SetInstruction : InstructionBase
    {
        public SetInstruction() : base(InstructionTypes.Set, 3)
        {
        }

        public override void Execute(EntityState state, byte[] parameters)
        {
            byte address = parameters[0];
            byte value = parameters[1];
            
            state.Memory.Set(address, value);
            
            state.InstructionIndex += Size;
        }
    }
}