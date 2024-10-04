namespace ElementSpy.Interfaces
{
    public interface IUIElement
    {
        string GetFieldIdentifier();
        int ProcessId { get; }
    }
}
