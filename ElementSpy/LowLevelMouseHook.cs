using ElementSpy.Interfaces;
using ElementSpy.Structs;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

using static Windows.Win32.PInvoke;

namespace ElementSpy
{
    public class LowLevelMouseHook : IMouseHook, IDisposable
    {
        private UnhookWindowsHookExSafeHandle _hookHandle;
        private readonly HOOKPROC _hookCallback;
        private DateTime _lastMouseMove;
        private readonly TimeSpan _debounceTime = TimeSpan.FromMilliseconds(500);
        private bool _isDisposed;
        private POINT _lastPosition;

        public bool IsRunning => _hookHandle != null && !_hookHandle.IsInvalid;

        public event EventHandler<IMouseMoveEventArgs> MouseMove;

        public LowLevelMouseHook()
        {
            _hookCallback = HookCallback;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                _hookHandle = SetWindowsHookEx(WINDOWS_HOOK_ID.WH_MOUSE_LL, _hookCallback, null, 0u);
                if (_hookHandle.IsInvalid)
                {
                    throw new InvalidOperationException($"Failed to set mouse hook. Error code: {Marshal.GetLastWin32Error()}");
                }
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                _hookHandle.Dispose();
                _hookHandle = null;
            }
        }

        private LRESULT HookCallback(int nCode, WPARAM wParam, LPARAM lParam)
        {
            if (nCode >= 0)
            {
                DateTime now = DateTime.Now;
                if (now - _lastMouseMove < _debounceTime)
                {
                    return CallNextHookEx(_hookHandle, nCode, wParam, lParam);
                }

                var hookStruct = Marshal.PtrToStructure<MSLLHOOKSTRUCT>((nint)lParam);

                if (_lastPosition.x != hookStruct.Point.x || _lastPosition.y != hookStruct.Point.y)
                {
                    _lastPosition = hookStruct.Point;
                    _lastMouseMove = now;

                    var position = new ScreenPosition(hookStruct.Point.x, hookStruct.Point.y);
                    MouseMove?.Invoke(this, new MouseMoveEventArgs(position));
                }
            }
            return CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    Stop();
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~LowLevelMouseHook()
        {
            Dispose(disposing: false);
        }
    }
}
