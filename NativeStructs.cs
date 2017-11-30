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
