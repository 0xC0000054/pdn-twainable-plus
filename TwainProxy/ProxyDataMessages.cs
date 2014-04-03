using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
