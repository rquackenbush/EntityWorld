using System.Drawing;

namespace EntityWorld
{
    public static class RectangleExtensions
    {
        public static bool IsPointBelow(this Rectangle rect, Point position)
        {
            return position.Y > rect.Bottom;
        }

        public static bool IsPointAbove(this Rectangle rect, Point position)
        {
            return position.Y < rect.Top;
        }

        public static bool IsPointLeft(this Rectangle rect, Point position)
        {
            return position.X < rect.Left;
        }

        public static bool IsPointRight(this Rectangle rect, Point position)
        {
            return  position.X > rect.Right;
        }
    }
}