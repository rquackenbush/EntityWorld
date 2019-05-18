using System;
using System.Drawing;

namespace EntityWorld.Host
{
    public static class PointExtensions
    {
        public static double GetDistance(this Point p1, Point p2)
        {
            int deltaX = p1.X - p2.X;
            int deltaY = p1.Y - p2.Y;

            return Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        }
    }
}