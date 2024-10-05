using ElementSpy.Interfaces;
using System.Windows.Automation;

namespace ElementSpy
{
    internal class AutomationElementWrapper : IUIElement
    {
        private readonly AutomationElement _element;
        public int ProcessId { get; }

        public AutomationElementWrapper(AutomationElement element)
        {
            _element = element;
            ProcessId = _element.Current.ProcessId;
        }

        public string GetFieldIdentifier()
        {
            string id = _element.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty) as string;
            return string.IsNullOrEmpty(id) ? string.Empty : id;
        }
    }
}
