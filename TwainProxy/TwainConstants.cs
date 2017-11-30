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

namespace TwainProxy
{
    internal static class DataArgumentType
    {
        public const ushort Null = 0;
        public const ushort CUSTOMBASE = 0x8000;

        // Arguments for the Control Data Group

        public const ushort Capability = 1;
        public const ushort Event = 2;
        public const ushort Identity = 3;
        public const ushort Parent = 4;
        public const ushort PendingXfers = 5;
        public const ushort SetupMemXfer = 6;
        public const ushort SetupFileXfer = 7;
        public const ushort Status = 8;
        public const ushort USERINTERFACE = 9;
        public const ushort XFERGROUP = 10;
        public const ushort CUSTOMDSDATA = 12;
        public const ushort DEVICEEVENT = 13;
        public const ushort FILESYSTEM = 14;
        public const ushort PASSTHRU = 15;
        public const ushort CALLBACK = 16;
        public const ushort STATUSUTF8 = 17;
        public const ushort CALLBACK2 = 18;

        // Arguments for the Image Data Group

        public const ushort ImageInfo = 257;
        public const ushort ImageLayout = 258;
        public const ushort ImageMemXfer = 259;
        public const ushort ImageNativeXfer = 260;
        public const ushort ImageFileXfer = 261;
        public const ushort CIEColor = 262;
        public const ushort GrayResponse = 263;
        public const ushort RGBResponse = 264;
        public const ushort JPEGCompression = 265;
        public const ushort Palette8 = 266;
        public const ushort ExtImageInfo = 267;
        public const ushort Filter = 268;

        // Arguments for the Audio Data Group

        public const ushort AudioFileXfer = 513;
        public const ushort AudioInfo = 514;
        public const ushort AudioNativeXfer = 515;

        // Misc Arguments

        public const ushort ICCProfile = 1025;
        public const ushort ImageMemFileXfer = 1026;
        public const ushort Entrypoint = 1027;
    }


    internal static class TwainMessages
    {
        public const ushort Null = 0;
        public const ushort CustomBase = 32768;

        // Generic messages may be used with any of several DATs.
        public const ushort Get = 1;
        public const ushort GetCurrent = 2;
        public const ushort GetDefault = 3;
        public const ushort GetFirst = 4;
        public const ushort GetNext = 5;
        public const ushort Set = 6;
        public const ushort Reset = 7;
        public const ushort QuerySupport = 8;
        public const ushort GetHelp = 9;
        public const ushort GetLabel = 10;
        public const ushort GetLabelNum = 11;
        public const ushort SetConstraint = 12;

        // Messages used with DAT_NULL
        public const ushort XferReady = 257;
        public const ushort CloseDSReq = 258;
        public const ushort CloseDSOk = 259;
        public const ushort DeviceEvent = 260;

        // Messages used with a pointer to DAT_PARENT data
        public const ushort OpenDSM = 769;
        public const ushort CloseDSM = 770;

        // Messages used with a pointer to a DAT_IDENTITY structure
        public const ushort OpenDS = 1025;
        public const ushort CloseDS = 1026;
        public const ushort UserSelect = 1027;

        // Messages used with a pointer to a DAT_USERINTERFACE structure
        public const ushort DisableDS = 1281;
        public const ushort EnableDS = 1282;
        public const ushort EnableDSUIOnly = 1283;

        // Messages used with a pointer to a DAT_EVENT structure
        public const ushort ProcessEvent = 1537;

        // Messages used with a pointer to a DAT_PENDINGXFERS structure
        public const ushort EndXfer = 1793;
        public const ushort StopFeeder = 1794;

        // Messages used with a pointer to a DAT_FILESYSTEM structure
        public const ushort ChangeDirectory = 2049;
        public const ushort CreateDirectory = 2050;
        public const ushort Delete = 2051;
        public const ushort FormatMedia = 2052;
        public const ushort GetClose = 2053;
        public const ushort GetFirstFile = 2054;
        public const ushort GetInfo = 2055;
        public const ushort GetNextFile = 2056;
        public const ushort Rename = 2057;
        public const ushort Copy = 2058;
        public const ushort AutomaticCaptureDirectory = 2059;

        // Messages used with a pointer to a DAT_PASSTHRU structure
        public const ushort PassThru = 2305;

        // used with DAT_CALLBACK
        public const ushort RegisterCallback = 2306;

        // used with DAT_CAPABILITY
        public const ushort ResetAll = 2561;
    }


    internal static class TwainCapabilities
    {
        public const ushort CustomBase = 32768;

        // all data sources are REQUIRED to support these caps
        public const ushort XferCount = 1;

        // image data sources are REQUIRED to support these caps
        public const ushort ICompression = 256;
        public const ushort IPixelType = 257;
        public const ushort IUnits = 258;
        public const ushort IXferMech = 259;

        // all data sources MAY support these caps
        public const ushort Author = 4096;
        public const ushort Caption = 4097;
        public const ushort FeederEnabled = 4098;
        public const ushort FeederLoaded = 4099;
        public const ushort TimeDate = 4100;
        public const ushort SupportedCaps = 4101;
        public const ushort ExtendedCaps = 4102;
        public const ushort AutoFeed = 4103;
        public const ushort ClearPage = 4104;
        public const ushort FeedPage = 4105;
        public const ushort RewindPage = 4106;
        public const ushort Indicators = 4107;
        public const ushort SupportedCapsExt = 4108;
        public const ushort PaperDetectable = 4109;
        public const ushort UIControllable = 4110;
        public const ushort DeviceOnline = 4111;
        public const ushort AutoScan = 4112;
        public const ushort ThumbnailsEnabled = 4113;
        public const ushort Duplex = 4114;
        public const ushort DuplexEnabled = 4115;
        public const ushort EnableDSUIOnly = 4116;
        public const ushort CustomDSData = 4117;
        public const ushort Endorser = 4118;
        public const ushort JobControl = 4119;

        // image data sources MAY support these caps
        public const ushort AutoBright = 4352;
        public const ushort Brightness = 4353;
        public const ushort Contrast = 4355;
        public const ushort CustHalftone = 4356;
        public const ushort ExposureTime = 4357;
        public const ushort Filter = 4358;
        public const ushort FlashUsed = 4359;
        public const ushort Gamma = 4360;
        public const ushort Halftones = 4361;
        public const ushort Highlight = 4362;
        public const ushort ImageFileFormat = 4364;
        public const ushort LampState = 4365;
        public const ushort LightSource = 4366;
        public const ushort Orientation = 4368;
        public const ushort PhysicalWidth = 4369;
        public const ushort PhysicalHeight = 4370;
        public const ushort Shadow = 4371;
        public const ushort Frames = 4372;
        public const ushort XNativeResolution = 4374;
        public const ushort YNativeResolution = 4375;
        public const ushort XResolution = 4376;
        public const ushort YResolution = 4377;
        public const ushort MaxFrames = 4378;
        public const ushort Tiles = 4379;
        public const ushort BitOrder = 4380;
        public const ushort CCITTKFACTOR = 4381;
        public const ushort LightPath = 4382;
        public const ushort PixelFlavor = 4383;
        public const ushort PlanarChunky = 4384;
        public const ushort Rotation = 4385;
        public const ushort SupportedSizes = 4386;
        public const ushort Threshold = 4387;
        public const ushort XScaling = 4388;
        public const ushort YScaling = 4389;
        public const ushort BitOrderCodes = 4390;
        public const ushort PixelFlavorCodes = 4391;
        public const ushort JPEGPixelType = 4392;
        public const ushort TimeFill = 4394;
        public const ushort BitDepth = 4395;
        public const ushort BitDepthReduction = 4396;
        public const ushort UndefinedImageSize = 4397;
        public const ushort ImageDataSet = 4398;
        public const ushort ExtImageInfo = 4399;
        public const ushort MinimumHeight = 4400;
        public const ushort MinimumWidth = 4401;
    }


    internal static class TwainConstants
    {
        public const ushort ProtocolMajor = 1;
        public const ushort ProtocolMinor = 9;

        public const ushort TWLG_ENGLISH_USA = 13;
        public const ushort TWCY_USA = 1;
    }
}
