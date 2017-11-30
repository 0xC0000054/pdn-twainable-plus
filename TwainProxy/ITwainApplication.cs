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

namespace TwainProxy
{
    interface ITwainApplication
    {
        IntPtr WindowHandle { get; }
        void DisableApplicationWindow();
        void EnableApplicationWindow();
        void BringToForeground();
    }
}
