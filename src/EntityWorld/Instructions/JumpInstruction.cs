namespace EntityWorld.Instructions
{
    public class JumpInstruction : InstructionBase
    {
        public JumpInstruction() : base(InstructionTypes.Jump, 2)
        {
        }

        public override void Execute(EntityState state, byte[] parameters)
        {
            state.InstructionIndex = parameters[0];
        }
    }
}