using System;
using System.Drawing;

namespace EntityWorld
{
    public class WorldCreationContext
    {
        public WorldCreationContext(int numberOfEntities, int numberOfInstructions, Size worldSize, int maxFood, Rectangle food)
        {
            NumberOfEntities = numberOfEntities;
            NumberOfInstructions = numberOfInstructions;
            WorldSize = worldSize;
            MaxFood = maxFood;
            Food = food;
        }

        /// <summary>
        /// The random number generator used for this run
        /// </summary>
        public Random Random { get; } = new Random();

        public int NumberOfEntities { get; }

        public int NumberOfInstructions { get; }

        public Size WorldSize { get; }

        public int MaxFood { get; }

        public Rectangle Food { get; }
    }
}