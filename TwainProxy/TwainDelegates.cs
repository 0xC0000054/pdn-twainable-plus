using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwainProxy
{
    internal static class TwainDelegates
    {
        internal delegate ResultCode TwainEvent(ref TwainStructs.Identity appId, ref TwainStructs.Identity deviceId, DataGroup group, ushort argType, ushort msg, ref TwainStructs.Event item);
        internal delegate ResultCode TwianParent(ref TwainStructs.Identity appId, IntPtr deviceId, DataGroup group, ushort argType, ushort msg, ref IntPtr parentHWnd);
        internal delegate ResultCode TwainIdentity(ref TwainStructs.Identity appId, IntPtr deviceId, DataGroup group, ushort argType, ushort msg, ref TwainStructs.Identity currentDevice);
        internal delegate ResultCode TwainUI(ref TwainStructs.Identity appId, ref TwainStructs.Identity deviceId, DataGroup group, ushort argType, ushort msg, ref TwainStructs.UserInterface item);
        internal delegate ResultCode TwainCapability(ref TwainStructs.Identity appId, ref TwainStructs.Identity deviceId, DataGroup group, ushort argType, ushort msg, ref TwainStructs.Capability item);
        internal delegate ResultCode TwianNativeTransfer(ref TwainStructs.Identity appId, ref TwainStructs.Identity deviceId, DataGroup group, ushort argType, ushort msg, ref IntPtr hDib);
        internal delegate ResultCode TwianPendingXfer(ref TwainStructs.Identity appId, ref TwainStructs.Identity deviceId, DataGroup group, ushort argType, ushort msg, ref TwainStructs.PendingXfers item);
        internal delegate ResultCode TwainStatus(ref TwainStructs.Identity appId, IntPtr deviceId, DataGroup group, ushort argType, ushort msg, ref TwainStructs.Status status);

    }
}
