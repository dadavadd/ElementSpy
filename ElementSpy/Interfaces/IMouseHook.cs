using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementSpy.Interfaces
{
    public interface IMouseHook
    {
        bool IsRunning { get; }
        void Start();
        void Stop();
        event EventHandler<IMouseMoveEventArgs> MouseMove;
    }
}
