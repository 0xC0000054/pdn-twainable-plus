using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
