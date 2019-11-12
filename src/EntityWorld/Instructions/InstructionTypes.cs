namespace EntityWorld.Instructions
{
    public static class InstructionTypes
    {
        /// <summary>
        /// Copies one piece of memory to another.
        /// </summary>
        public const byte Copy = 1;

        /// <summary>
        /// Negate a value (in place)
        /// </summary>
        public const byte Not = 2;
        
        /// <summary>
        /// Jump to a specified address.
        /// </summary>
        public const byte Jump = 3;

        /// <summary>
        /// Jump if the values at the memory locations are equal
        /// </summary>
        public const byte JumpEqual = 4;

        /// <summary>
        /// Jump if the 2nd value is greater than the 1st value.
        /// </summary>
        public const byte JumpGreaterThan = 5;

        /// <summary>
        /// Jump if the 2nd value is less than the 1st value.
        /// </summary>
        public const byte JumpLessThan = 6;

        /// <summary>
        /// Sets the memory at the specified location to the provided value
        /// </summary>
        public const byte Set = 10;
    }
}