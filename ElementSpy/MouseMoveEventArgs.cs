using ElementSpy.Interfaces;

namespace ElementSpy
{
    public class MouseMoveEventArgs : EventArgs, IMouseMoveEventArgs
    {
        public IScreenPosition Position { get; }

        public MouseMoveEventArgs(IScreenPosition position)
        {
            Position = position;
        }
    }
}
