using System;

namespace EntityWorld
{
    public class Memory<T> where T : struct
    {
        private readonly T[] _memory;

        public Memory(int size)
        {
            _memory = new T[size];
        }

        public Memory(T[] memory)
        {
            _memory = new T[memory.Length];
            
            Array.Copy(memory, _memory, memory.Length);
        }

        public T Get(int address)
        {
            return _memory[address];
        }

        public T[] Get(int address, int length)
        {
            var result = new T[length];
            
            Array.Copy(_memory, address, result, 0, length);

            return result;
        }

        public void Set(int address, T value)
        {
            _memory[address] = value;
        }

//        public void Set(int address, T[] values)
//        {
//            Array.Copy();
//        }

        /// <summary>
        /// The length of memory
        /// </summary>
        public int Length => _memory.Length;
    }
}