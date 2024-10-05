using System.Windows.Automation;

namespace ElementSpy.Interfaces
{
    public interface IElementFinder
    {
        IUIElement GetElementAtPosition(int x, int y);
    }
}
