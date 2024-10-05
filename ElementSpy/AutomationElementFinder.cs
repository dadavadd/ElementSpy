using ElementSpy.Interfaces;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;

namespace ElementSpy
{
    public class AutomationElementFinder : IElementFinder
    {
        public IUIElement GetElementAtPosition(int x, int y)
        {
            var element = AutomationElement.FromPoint(new Point(x, y));
            if (element == null) return null;

            var wrapper = new AutomationElementWrapper(element);
            return wrapper.ProcessId == Process.GetCurrentProcess().Id ? null : wrapper;
        }
    }

}
