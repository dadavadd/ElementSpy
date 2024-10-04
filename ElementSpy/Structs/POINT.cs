using System.Runtime.InteropServices;

namespace ElementSpy.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}
