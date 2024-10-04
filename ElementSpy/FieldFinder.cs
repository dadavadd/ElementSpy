using ElementSpy.Interfaces;

namespace ElementSpy
{
    public class FieldFinder
    {
        private readonly IElementFinder _elementFinder;

        private readonly IMouseHook _mouseHook;

        public event Action<string> FieldFound;

        public FieldFinder(IElementFinder element, IMouseHook mouseHook)
        {
            _elementFinder = element;
            _mouseHook = mouseHook;
            _mouseHook.MouseMove += OnMouseMove;
        }

        private void OnMouseMove(object sender, IMouseMoveEventArgs e)
        {
            IUIElement elementAtPosition = _elementFinder.GetElementAtPosition(e.Position);
            if (elementAtPosition != null)
            {
                OnFieldFound(elementAtPosition.GetFieldIdentifier());
            }
        }

        protected virtual void OnFieldFound(string fieldName)
        {
            FieldFound?.Invoke(fieldName);
        }

        public void StartTracking()
        {
            _mouseHook.Start();
        }

        public void StopTracking()
        {
            _mouseHook.Stop();
        }
    }
}