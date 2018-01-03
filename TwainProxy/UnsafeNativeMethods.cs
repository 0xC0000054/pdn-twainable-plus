////////////////////////////////////////////////////////////////////////
//
// This file is part of pdn-twainable-plus, an Effect plugin for
// Paint.NET that imports images from TWAIN devices.
//
// Copyright (c) 2014, 2017, 2018 Nicholas Hayes
//
// This file is licensed under the MIT License.
// See LICENSE.txt for complete licensing and attribution information.
//
////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Runtime.InteropServices;

namespace TwainProxy
{
    [System.Security.SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        internal static extern SafeLibraryHandle LoadLibraryW(string lpFileName);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false)]
        internal static extern IntPtr GetProcAddress([In()] SafeLibraryHandle hModule, [In(), MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int SHGetFolderPathW(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder pszPath);
    }
}
