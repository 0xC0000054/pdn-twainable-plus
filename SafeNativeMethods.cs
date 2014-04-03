using System;
using System.Runtime.InteropServices;

namespace TwainablePlus
{
    [System.Security.SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern uint RegisterWindowMessageW([In(), MarshalAs(UnmanagedType.LPWStr)] string lpString);

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern IntPtr PostMessageW([In()] IntPtr hWnd, [In()] uint Msg, [In()] UIntPtr wParam, [In()] IntPtr lParam);
    }
}
