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
