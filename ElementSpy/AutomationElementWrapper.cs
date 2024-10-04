using ElementSpy.Interfaces;
using System.Windows.Automation;

namespace ElementSpy
{
    internal class AutomationElementWrapper : IUIElement
    {
        private readonly AutomationElement _element;

        public AutomationElementWrapper(AutomationElement element)
        {
            _element = element;
            ProcessId = _element.Current.ProcessId;
        }

        public string GetFieldIdentifier()
        {
            var automationId = _element.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty) as string;
            return string.IsNullOrEmpty(automationId) ? string.Empty : automationId;
        }

        public int ProcessId { get; }
    }
}
