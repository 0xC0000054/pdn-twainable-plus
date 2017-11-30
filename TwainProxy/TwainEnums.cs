using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwainProxy
{
    [Flags]
    internal enum DataGroup : int
    {
        Control = 1,
        Image = 2,
        Audio = 4
    }

    internal enum ImageMesurementUnits : int
    {
        Inches = 0,
        Centimeters = 1,
        Picas = 2,
        Points = 3,
        Twips = 4,
        Pixels = 5,
        Milimeters = 6
    }

    internal enum DataTypes : ushort
    {
        Int8 = 0,
        Int16,
        Int32,
        UInt8,
        UInt16,
        UInt32,
        Bool,
        Fix32,
        Frame,
        Str32,
        Str64,
        Str128,
        Str255,
        Handle
    }

    internal enum ContainerType : ushort
    {
        Array = 3,
        Enumeration = 4,
        OneValue = 5,
        Range = 6,
        DontCare = ushort.MaxValue
    }

    [Flags]
    internal enum QueryCapability
    {
        None = 0,
        Get = 1,
        Set = 2,
        GetDefault = 4,
        GetCurrent = 8,
        Reset = 16,
        SetConstrant = 32,
        Constrainable = 64,
        GetHelp = 256,
        GetLabel = 512,
        GetLabelNum = 1024
    }

    internal enum ResultCode
    {
        Success = 0,
        Failure,
        CheckStatus,
        Cancel,
        DSEvent,
        NotDSEvent,
        TransferDone,
        EndOfList,
        InfoNotSupported,
        DataNotAvailable,
        Busy,
        ScannerLocked
    }

    internal enum TransferMech : ushort
    {
        Native = 0,
        File = 1,
        Memory = 2
    }

    internal enum ConditionCode : ushort
    {
        CustomBase = 32768,

        Success = 0,
        Bummer = 1,
        LowMemory = 2,
        NoDataSource = 3,
        MaxConnections = 4,
        OperationError = 5,
        BadCap = 6,
        BatProtocol = 9,
        BadValue = 10,
        SeqError = 11,
        BadDest = 12,
        CapUnsupported = 13,
        CapBadOperation = 14,
        CapSeqError = 15,
        Denied = 16,
        FileExists = 17,
        FileNotFound = 18,
        NotEmpty = 19,
        PaperJam = 20,
        PaperDoubleFeed = 21,
        FileWriteError = 22,
        CheckDeviceOnline = 23,
        Interlock = 24,
        DamagedCorner = 25,
        FocusError = 26,
        DocTooLight = 27,
        DocTooDark = 28,
        NoMedia = 29
    }

    internal enum PixelType : short
    {
        /// <summary>
        /// A pixel with a value of zero is black.
        /// </summary>
        Chocolate = 0,
        /// <summary>
        ///A pixel with a value of zero is white.
        /// </summary>
        Vanilla = 1
    }


}
