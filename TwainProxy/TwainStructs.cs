using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TwainProxy
{
    class TwainStructs
    {
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Fix32
        {
            public short Whole;
            public ushort Frac;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
        public struct Version
        {
            public ushort MajorNum;
            public ushort MinorNum;
            public ushort Language;
            public ushort Country;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
            public string Info;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
        public struct Identity
        {
            public uint Id;
            public TwainStructs.Version Version;
            public ushort ProtocolMajor;
            public ushort ProtocolMinor;
            public uint SupportedGroups;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
            public string Manufacturer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
            public string ProductFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
            public string ProductName;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ImageInfo
        {
            public Fix32 XResolution;
            public Fix32 YResolution;
            public int ImageWidth;
            public int ImageLength;
            public short SamplesPerPixel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public short[] BitsPerSample;
            public short BitsPerPixel;
            public ushort Planar;
            public PixelType PixelType;
            public ushort Compression;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct UserInterface
        {
            public ushort ShowUI;
            public ushort ModalUI;
            public IntPtr hParent;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Event
        {
            public IntPtr pEvent;
            public ushort TWMessage;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct OneValue
        {
            public DataTypes ItemType;
            public uint Item;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Array
        {
            public DataTypes ItemType;
            public uint NumItems;
            // array of values follows.
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Enumeration
        {
            public DataTypes ItemType;
            public uint NumItems;
            public uint CurrentIndex;
            public uint DefaultIndex;
            // array of values follows.
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Capability
        {
            public ushort Cap;
            public ContainerType ConType;
            public IntPtr hContainer;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 2)]
        public struct PendingXfersUnion
        {
            [FieldOffset(0)]
            public uint EOJ;
            [FieldOffset(0)]
            public uint Reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct PendingXfers
        {
            public ushort Count;
            public PendingXfersUnion union;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 2)]
        public struct StatusUnion
        {
            [FieldOffset(0)]
            public ushort Data; // output (TWAIN 2.1 and newer)
            [FieldOffset(0)]
            public ushort Reserved; // output (TWAIN 2.0 and older)
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Status
        {
            public ConditionCode ConditionCode;
            public StatusUnion union;
        }

    }
}
