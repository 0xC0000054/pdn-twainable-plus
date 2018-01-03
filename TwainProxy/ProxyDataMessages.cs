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

namespace TwainProxy
{
    internal static class ProxyDataMessages
    {
        internal const int GetProxyWindowHandle = 1;
        internal const int SourcesLoadedCallback = 2;
        internal const int StartScan = 3;
        internal const int ScanEnableWindow = 4;
        internal const int ScanCompletedCallback = 5;
    }
}
