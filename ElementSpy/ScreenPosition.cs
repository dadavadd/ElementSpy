using ElementSpy.Interfaces;

namespace ElementSpy
{
    internal class ScreenPosition : IScreenPosition
    {
        public int X { get; }

        public int Y { get; }

        public ScreenPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
