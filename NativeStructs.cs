using System;
using System.Runtime.InteropServices;

namespace TwainablePlus
{
    static class NativeStructs
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct COPYDATASTRUCT
        {
            public IntPtr dwData;       // Specifies data to be passed 
            public int cbData;          // Specifies the data size in bytes 
            public IntPtr lpData;       // Pointer to data to be passed 
        } 
    }
}
