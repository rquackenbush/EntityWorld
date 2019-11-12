using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityWorld.Instructions
{
    public static class InstructionFactory
    {
        private static readonly IDictionary<byte, InstructionBase> _instructions = new Dictionary<byte, InstructionBase>();
        
        static InstructionFactory()
        {
            var instructionBaseType = typeof(InstructionBase);

            //Get the instruction types
            var instructionTypes = typeof(InstructionFactory).Assembly.GetTypes()
                .Where(t => instructionBaseType.IsAssignableFrom(t) && !t.IsAbstract);

            //Consider each instruction type
            foreach (var instructionType in instructionTypes)
            {
                //Create an instance (should be an empty constructor)
                var instruction = Activator.CreateInstance(instructionType);

                //Let's see if it's actually the correct type
                var typedInstruction = instruction as InstructionBase;
                
                if (typedInstruction == null)
                    throw new InvalidOperationException($"The type '{instructionType.FullName}' does not derive from '{nameof(InstructionBase)}'");
                
                _instructions.Add(typedInstruction.InstructionType, typedInstruction);
            }
        }

        /// <summary>
        /// Gets the instruction given its type.
        /// </summary>
        /// <param name="instructionType"></param>
        /// <returns></returns>
        public static InstructionBase Get(byte instructionType)
        {
            if (_instructions.TryGetValue(instructionType, out var instruction))
                return instruction;

            return null;
        }
    }
}