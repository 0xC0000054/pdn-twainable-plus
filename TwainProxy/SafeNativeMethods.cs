using System;
using System.Runtime.InteropServices;

namespace TwainProxy
{
    [System.Security.SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenClipboard([In()] IntPtr hWndNewOwner);

        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EmptyClipboard();

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern IntPtr SetClipboardData([In()] uint uFormat, [In()] IntPtr hMem);

        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseClipboard();

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock([In()] System.IntPtr hMem);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GlobalUnlock([In()] System.IntPtr hMem);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree([In()] IntPtr hMem);

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern uint RegisterWindowMessageW([In(), MarshalAs(UnmanagedType.LPWStr)] string lpString);

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern IntPtr SendMessageW([In()] IntPtr hWnd, [In()] uint Msg, [In()] UIntPtr wParam, [In()] IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageW", ExactSpelling = true)]
        internal static extern IntPtr SendCopyData([In()] IntPtr hWnd, [In()] uint Msg, [In()] IntPtr wParam, [In()] ref NativeStructs.COPYDATASTRUCT cds);

        [DllImport("user32.dll", EntryPoint = "GetMessageW")]
        internal static extern int GetMessage([Out()] out NativeStructs.tagMSG lpMsg, [In()] System.IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll", EntryPoint = "TranslateMessage")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool TranslateMessage([In()] ref NativeStructs.tagMSG lpMsg);

        [DllImport("user32.dll", EntryPoint = "DispatchMessageW")]
        internal static extern IntPtr DispatchMessage([In()] ref NativeStructs.tagMSG lpMsg);
    }
}
