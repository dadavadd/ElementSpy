using ElementSpy.Structs;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

using static Windows.Win32.PInvoke;

namespace ElementSpy
{
    public class LowLevelMouseHook : IDisposable
    {
        private UnhookWindowsHookExSafeHandle _hookHandle;
        private readonly HOOKPROC _hookProc;
        private DateTime _lastMouseMove = DateTime.MinValue;

        public event Action<int, int> MouseMove;

        public LowLevelMouseHook()
        {
            _hookProc = HookCallback;
        }

        public void Start()
            => _hookHandle = SetWindowsHookEx(WINDOWS_HOOK_ID.WH_MOUSE_LL, _hookProc, null, 0u);

        public void Stop()
        {
            if (_hookHandle != null)
                UnhookWindowsHookEx((HHOOK)_hookHandle.DangerousGetHandle());
        }

        private LRESULT HookCallback(int nCode, WPARAM wParam, LPARAM lParam)
        {
            if (nCode >= 0 && DateTime.Now - _lastMouseMove > TimeSpan.FromMilliseconds(500))
            {
                var mouseInfo = Marshal.PtrToStructure<POINT>(lParam);
                _lastMouseMove = DateTime.Now;
                MouseMove?.Invoke(mouseInfo.x, mouseInfo.y);
            }
            return CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }

        public void Dispose() => Stop();
    }
}
