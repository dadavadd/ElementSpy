using ElementSpy.Interfaces;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;

namespace ElementSpy.AutomationElementFinder
{
    public class AutomationElementFinder : IElementFinder
    {
        public IUIElement GetElementAtPosition(IScreenPosition position)
        {
            var automationElement = AutomationElement.FromPoint(new(position.X, position.Y));

            if (automationElement == null) return null;

            var elementWrapper = new AutomationElementWrapper(automationElement);

            if (elementWrapper.ProcessId == Process.GetCurrentProcess().Id) return null;

            return elementWrapper;
        }
    }
}
