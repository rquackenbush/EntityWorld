using System.Drawing;

namespace EntityWorld
{
    /// <summary>
    /// The state of the world
    /// </summary>
    public class WorldInfo
    {
        public WorldInfo(Size size, Rectangle food)
        {
            Size = size;
            Food = food;
        }

        /// <summary>
        /// The area that the food takes up.
        /// </summary>
        public Rectangle Food { get;}

        /// <summary>
        /// The size of the world.
        /// </summary>
        public Size Size { get; }
    }
}