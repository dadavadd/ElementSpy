using ElementSpy.Interfaces;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;

namespace ElementSpy.AutomationElementFinder
{
    public class AutomationElementFinder : IElementFinder
    {
        private IUIElement _lastElement;
        private Point _lastPoint;

        public IUIElement GetElementAtPosition(IScreenPosition position)
        {
            var currentPoint = new Point(position.X, position.Y);

            if (_lastPoint == currentPoint && _lastElement != null)
            {
                return _lastElement;
            }

            _lastPoint = currentPoint;

            var automationElement = AutomationElement.FromPoint(currentPoint);
            if (automationElement == null)
            {
                _lastElement = null;
                return null;
            }

            var elementWrapper = new AutomationElementWrapper(automationElement);
            if (elementWrapper.ProcessId == Process.GetCurrentProcess().Id)
            {
                _lastElement = null;
                return null;
            }

            _lastElement = elementWrapper;
            return elementWrapper;
        }
    }
}
