using System.Drawing;
using Xunit;
using Shouldly;

namespace EntityWorld.Tests
{
    public class RectangleExtensionsTests
    {
        private readonly Rectangle _rectangle = new Rectangle(100, 100, 10, 10);

        [Theory]
        [InlineData(0, 0)]
        [InlineData(20, 20)]
        [InlineData(200, 99)]
        public void IsAbovePositiveTests(int x, int y)
        {
            _rectangle
                .IsRectangleAbove(new Point(x, y))
                .ShouldBeTrue();
        }

        [Theory]
        [InlineData(200, 200)]
        [InlineData(0, 200)]
        public void IsAboveNegativeTests(int x, int y)
        {
            _rectangle
                .IsRectangleAbove(new Point(x, y))
                .ShouldBeFalse();
        }

        [Theory]
        [InlineData(200, 200)]
        [InlineData(0, 200)]
        [InlineData(100, 200)]
        public void IsBelowPositiveTests(int x, int y)
        {
            _rectangle
                .IsRectangleBelow(new Point(x, y))
                .ShouldBeTrue();
        }
    }
}