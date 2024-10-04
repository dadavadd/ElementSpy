using System.Windows.Automation;

namespace ElementSpy.Interfaces
{
    public interface IElementFinder
    {
        IUIElement GetElementAtPosition(IScreenPosition position);
    }
}
