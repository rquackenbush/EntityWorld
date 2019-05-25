using System;
using EntityWorld.Interfaces;

namespace EntityWorld
{
    /// <summary>
    /// This uses the built in psuedo random number generator.
    /// Should switch this out to use a cryptographic generator eventually.
    /// </summary>
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random = new Random();

        public int Next(int minimum, int maximum)
        {
            return _random.Next(minimum, maximum);
        }
    }
}