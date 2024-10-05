using ElementSpy.Interfaces;

namespace ElementSpy
{
    public class FieldFinder
    {
        private readonly IElementFinder _elementFinder;
        private readonly LowLevelMouseHook _mouseHook;
        public event Action<string> FieldFound;

        public FieldFinder(IElementFinder elementFinder)
        {
            _elementFinder = elementFinder;
            _mouseHook = new LowLevelMouseHook();
            _mouseHook.MouseMove += OnMouseMove;
        }

        private void OnMouseMove(int x, int y)
        {
            var element = _elementFinder.GetElementAtPosition(x, y);

            if (element != null)
                FieldFound?.Invoke(element.GetFieldIdentifier());
        }

        public void StartTracking() => _mouseHook.Start();
        public void StopTracking() => _mouseHook.Stop();
    }
}