using System.Drawing;

namespace EntityWorld
{
    public static class RectangleExtensions
    {
        public static bool IsRectangleBelow(this Rectangle rect, Point position)
        {
            return position.Y < rect.Top;
        }

        public static bool IsRectangleAbove(this Rectangle rect, Point position)
        {
            return position.Y >= rect.Bottom;
        }

        public static bool IsRectangleLeft(this Rectangle rect, Point position)
        {
            return position.X >= rect.Right;
        }

        public static bool IsRectangleRight(this Rectangle rect, Point position)
        {
            return  position.X < rect.Left;
        }
    }
}