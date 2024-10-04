using ElementSpy.Interfaces;
using ElementSpy.Structs;

namespace ElementSpy
{
    public class LowLevelMouseMoveEventArgs : EventArgs, IMouseMoveEventArgs
    {
        public IScreenPosition Position { get; }

        public LowLevelMouseMoveEventArgs(POINT point)
        {
            Position = new ScreenPosition(point.x, point.y);
        }
    }
}
