////////////////////////////////////////////////////////////////////////
//
// This file is part of pdn-twainable-plus, an Effect plugin for
// Paint.NET that imports images from TWAIN devices.
//
// Copyright (c) 2014, 2017 Nicholas Hayes
//
// This file is licensed under the MIT License.
// See LICENSE.txt for complete licensing and attribution information.
//
////////////////////////////////////////////////////////////////////////

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
