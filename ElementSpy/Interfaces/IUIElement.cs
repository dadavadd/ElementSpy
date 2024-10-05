namespace ElementSpy.Interfaces
{
    public interface IUIElement
    {
        int ProcessId { get; }
        string GetFieldIdentifier();
    }
}
