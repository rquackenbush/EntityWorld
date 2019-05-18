using System.Drawing;

namespace EntityWorld.Host
{
    public static class WorldExtensions
    {
        public static bool IsFoodDown(this World world, Point position)
        {
            return world.Food.Bottom > position.Y;
        }

        public static bool IsFoodUp(this World world, Point position)
        {
            return world.Food.Top < position.Y;
        }

        public static bool IsFoodLeft(this World world, Point position)
        {
            return world.Food.Right < position.X;
        }

        public static bool IsFoodRight(this World world, Point position)
        {
            return world.Food.Left > position.X;
        }
    }
}