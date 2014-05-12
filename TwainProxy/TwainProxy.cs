using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TwainProxy
{
    internal sealed class TwainProxy : IDisposable
    {
        private bool disposed;
        private TwainState state;
        private SafeLibraryHandle hModule;
        private TwainStructs.Identity appId;
        private TwainStructs.Identity deviceId;
        private TwainStructs.UserInterface ui;
        private TwainStructs.Event twEvent;

        private static readonly string WindowsDirectory = GetWindowsDirectory();
        private const string TwainDllName = "Twain_32.dll";
        private const string TwainEntryPoint = "DSM_Entry";

        private List<TwainStructs.Identity> availableSources;
        private int defaultSourceIndex;
        private IntPtr latestDIB;

        private IntPtr parentWindowHandle;
        private ITwainApplication hostApp;

        #region Delegates
        private static TwainDelegates.TwainEvent twainEvent;
        private static TwainDelegates.TwianParent twainParent;
        private static TwainDelegates.TwainIdentity twainIdentity;
        private static TwainDelegates.TwainUI twainUI;
        private static TwainDelegates.TwainCapability twainCapability;
        private static TwainDelegates.TwianNativeTransfer twainNativeTransfer;
        private static TwainDelegates.TwianPendingXfer twainPendingXfers;
        private static TwainDelegates.TwainStatus twainStatus;
        #endregion

        private static string GetWindowsDirectory()
        {
            StringBuilder builder = new StringBuilder(260);

            int result = UnsafeNativeMethods.SHGetFolderPathW(IntPtr.Zero, NativeConstants.CSIDL_WINDOWS, IntPtr.Zero, 0, builder);

            if (result >= 0)
            {
                return builder.ToString();
            }

            return null;
        }

        public TwainProxy(ITwainApplication app)
        {
            this.disposed = false;
            this.state = TwainState.PreSession; 
            
            this.availableSources = new List<TwainStructs.Identity>();
            this.defaultSourceIndex = -1;
            this.latestDIB = IntPtr.Zero;

            this.appId = new TwainStructs.Identity();
            appId.Id = 0U;
            appId.Version.MajorNum = 1;
            appId.Version.MinorNum = 0;
            appId.Version.Country = TwainConstants.TWCY_USA;
            appId.Version.Language = TwainConstants.TWLG_ENGLISH_USA;
            appId.Version.Info = string.Format("TwainProxy v{0}", typeof(TwainProxy).Assembly.GetName().Version.ToString(4));
            appId.Manufacturer = "null54";
            appId.ProductFamily = "TwainablePlus";
            appId.ProductName = "TwainProxy";
            appId.ProtocolMajor = TwainConstants.ProtocolMajor;
            appId.ProtocolMinor = TwainConstants.ProtocolMinor;
            appId.SupportedGroups = (uint)(DataGroup.Control | DataGroup.Image);

            this.deviceId = new TwainStructs.Identity();
            this.parentWindowHandle = app.WindowHandle;
            this.hostApp = app;
        }

        private bool LoadSourceManager()
        {
            if (state >= TwainState.SourceManagerLoaded)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(WindowsDirectory))
            {
                string path = Path.Combine(WindowsDirectory, TwainDllName);

                if (File.Exists(path))
                {
                    this.hModule = UnsafeNativeMethods.LoadLibraryW(path);
                    if (!hModule.IsInvalid)
                    {
                        IntPtr entry = UnsafeNativeMethods.GetProcAddress(hModule, TwainEntryPoint);
                        if (entry != IntPtr.Zero)
                        {
                            twainEvent = (TwainDelegates.TwainEvent)Marshal.GetDelegateForFunctionPointer(entry, typeof(TwainDelegates.TwainEvent));
                            twainIdentity = (TwainDelegates.TwainIdentity)Marshal.GetDelegateForFunctionPointer(entry, typeof(TwainDelegates.TwainIdentity));
                            twainParent = (TwainDelegates.TwianParent)Marshal.GetDelegateForFunctionPointer(entry, typeof(TwainDelegates.TwianParent));
                            twainUI = (TwainDelegates.TwainUI)Marshal.GetDelegateForFunctionPointer(entry, typeof(TwainDelegates.TwainUI));
                            twainCapability = (TwainDelegates.TwainCapability)Marshal.GetDelegateForFunctionPointer(entry, typeof(TwainDelegates.TwainCapability));
                            twainNativeTransfer = (TwainDelegates.TwianNativeTransfer)Marshal.GetDelegateForFunctionPointer(entry, typeof(TwainDelegates.TwianNativeTransfer));
                            twainPendingXfers = (TwainDelegates.TwianPendingXfer)Marshal.GetDelegateForFunctionPointer(entry, typeof(TwainDelegates.TwianPendingXfer));
                            twainStatus = (TwainDelegates.TwainStatus)Marshal.GetDelegateForFunctionPointer(entry, typeof(TwainDelegates.TwainStatus));

                            this.state = TwainState.SourceManagerLoaded;
                            return true;
                        }
                    }

                }
            }

            return false;
        }

        private bool OpenSourceManager()
        {
            if (state < TwainState.SourceManagerOpen)
            {
                if (LoadSourceManager())
                {
                    ResultCode rc = twainParent(ref appId, IntPtr.Zero, DataGroup.Control, DataArgumentType.Parent, TwainMessages.OpenDSM, ref parentWindowHandle);

                    if (rc == ResultCode.Success)
                    {
                        this.state = TwainState.SourceManagerOpen;
                    }
                }
            }

            return (this.state >= TwainState.SourceManagerOpen);
        }

        private bool OpenSource()
        {
            if (state == TwainState.SourceManagerOpen)
            {
                ResultCode rc = DSMIdentity(DataGroup.Control, TwainMessages.OpenDS, ref deviceId);

                if (rc == ResultCode.Success)
                {
                    this.state = TwainState.SourceOpen;
                }
            }

            return (this.state >= TwainState.SourceOpen);
        }

        private bool EnableSource()
        {
            if (state == TwainState.SourceOpen)
            {
                ui.ShowUI = 1;
                ui.hParent = parentWindowHandle;


                ResultCode rc = twainUI(ref appId, ref deviceId, DataGroup.Control, DataArgumentType.USERINTERFACE, TwainMessages.EnableDS, ref ui);

                if (rc == ResultCode.Success)
                {
                    this.state = TwainState.SourceEnabled;                
                }
            }

            return (this.state >= TwainState.SourceEnabled);
        }

        private void DisableSource()
        {
            if (state == TwainState.SourceEnabled)
            {
                ResultCode rc = twainUI(ref appId, ref deviceId, DataGroup.Control, DataArgumentType.USERINTERFACE, TwainMessages.DisableDS, ref ui);
                if (rc == ResultCode.Success)
                {
                    this.state = TwainState.SourceOpen;
                }
            }
        }

        private void CloseSource()
        {
            DisableSource();

            if (state == TwainState.SourceOpen)
            {
                if (DSMIdentity(DataGroup.Control, TwainMessages.CloseDS, ref deviceId) == ResultCode.Success)
                {
                    state = TwainState.SourceManagerOpen;
                }
            }
        }

        private void CloseSourceManager()
        {
            CloseSource();
            if (state == TwainState.SourceManagerOpen)
            {
                ResultCode rc = twainParent(ref appId, IntPtr.Zero, DataGroup.Control, DataArgumentType.Parent, TwainMessages.CloseDSM, ref parentWindowHandle);

                if (rc == ResultCode.Success)
                {
                    this.state = TwainState.SourceManagerLoaded;
                }
            }
        }

        private bool FilterMessage(NativeStructs.tagMSG msg)
        {
            if (twEvent.pEvent == IntPtr.Zero)
            {
                twEvent.pEvent = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeStructs.tagMSG)));
            }

            Marshal.StructureToPtr(msg, twEvent.pEvent, false);

            ResultCode rc = twainEvent(ref appId, ref deviceId, DataGroup.Control, DataArgumentType.Event, TwainMessages.ProcessEvent, ref twEvent);

            if (rc != ResultCode.DSEvent && rc != ResultCode.NotDSEvent)
            {
                return false;
            }

            switch (twEvent.TWMessage)
            {
                case TwainMessages.XferReady:
                    this.state = TwainState.TransferReady;
                    this.latestDIB = DoOneTransfer();
                    break;
                case TwainMessages.CloseDSReq:
                    DisableSource();
                    break;
                default:
                    break;
            }

            return (rc == ResultCode.DSEvent);
        }

        private IntPtr DoOneTransfer()
        {
            IntPtr hDib = DoNativeXfer();

            this.breakModalLoop = true;

            EndXfer();

            return hDib;
        }

        private void EndXfer()
        {
            TwainStructs.PendingXfers pending = new TwainStructs.PendingXfers();
            ResultCode rc = ResultCode.Failure;

            if (state == TwainState.Transfering)
            {
                rc = twainPendingXfers(ref appId, ref deviceId, DataGroup.Control, DataArgumentType.PendingXfers, TwainMessages.EndXfer, ref pending);

                if (rc == ResultCode.Success)
                {
                    if (pending.Count > 0)
                    {
                        this.state = TwainState.TransferReady;
                    }
                    else
                    {
                        this.state = TwainState.SourceEnabled;
                    }
                }
            }

            if (state == TwainState.TransferReady)
            {
                twainPendingXfers(ref appId, ref deviceId, DataGroup.Control, DataArgumentType.PendingXfers, TwainMessages.Reset, ref pending);
            }
        }

        private IntPtr DoNativeXfer()
        {                
            IntPtr hDib = IntPtr.Zero;

            if (state == TwainState.TransferReady)
            {
                ResultCode rc = twainNativeTransfer(ref appId, ref deviceId, DataGroup.Image, DataArgumentType.ImageNativeXfer, TwainMessages.Get, ref hDib);

                switch (rc)
                {
                    case ResultCode.Cancel:
                    case ResultCode.TransferDone:
                        this.state = TwainState.Transfering;
                        break;
                    case ResultCode.Failure:
                    default:
                        this.state = TwainState.TransferReady;
                        hDib = IntPtr.Zero;
                        break;
                }
            }

            return hDib;
        }

        private ResultCode DSMIdentity(DataGroup group, ushort msg, ref TwainStructs.Identity ident)
        {
            ResultCode rc = twainIdentity(ref appId, IntPtr.Zero, group, DataArgumentType.Identity, msg, ref ident);

            return rc;
        }

        private ResultCode DSMStatus(ref TwainStructs.Status status)
        {
            return twainStatus(ref appId, IntPtr.Zero, DataGroup.Control, DataArgumentType.Status, TwainMessages.Get, ref status);
        }

        private bool SetTransferMech(TransferMech mech)
        {
            return SetCapOneValue(TwainCapabilities.IXferMech, DataTypes.UInt16, (uint)mech);
        }

        private bool SetTransferCount(int count)
        {
            return SetCapOneValue(TwainCapabilities.XferCount, DataTypes.Int16, (uint)count);
        }

#if false
        private bool GetUIControllable()
        {
            bool result = false;

            object value;

            if (GetCapOneValue(TwainCapabilities.UIControllable, DataTypes.Bool, out value))
            {
                result = (bool)value;
            }

            return result;
        }

        private bool GetCustomDataSupported()
        {
            bool result = false;

            object value;

            if (GetCapOneValue(TwainCapabilities.CustomDSData, DataTypes.Bool, out value))
            {
                result = (bool)value;
            }

            return result;
        }

        private unsafe bool GetCapOneValue(ushort capability, DataTypes type, out object value)
        {
            value = null;
            ResultCode rc = ResultCode.Failure;

            if (state >= TwainState.SourceOpen)
            {
                TwainStructs.Capability cap = new TwainStructs.Capability();
                cap.Cap = capability;
                cap.ConType = ContainerType.OneValue;
                cap.hContainer = IntPtr.Zero;

                rc = twainCapability(ref appId, ref deviceId, DataGroup.Control, DataArgumentType.Capability, TwainMessages.GetCurrent, ref cap);

                try
                {
                    if (cap.hContainer != IntPtr.Zero)
                    {
                        IntPtr ptr = LockMemory(cap.hContainer);

                        if (cap.ConType == ContainerType.OneValue)
                        {
                            TwainStructs.OneValue* pcon = (TwainStructs.OneValue*)ptr.ToPointer();

                            switch (pcon->ItemType)
                            {
                                case DataTypes.Bool:
                                    value = pcon->Item != 0U;
                                    break;
                                case DataTypes.Int16:
                                    value = (short)pcon->Item;
                                    break;
                                case DataTypes.UInt16:
                                    value = (ushort)pcon->Item;
                                    break;
                                case DataTypes.Int32:
                                    value = (int)value;
                                    break;
                                case DataTypes.UInt32:
                                    value = pcon->Item;
                                    break;
                            }
                        }

                        UnlockMemory(cap.hContainer);


                        if (rc == ResultCode.Failure)
                        {
                            TwainStructs.Status status = new TwainStructs.Status();
                            ResultCode src = DSMStatus(ref status);

                            System.Diagnostics.Debug.Assert(src == ResultCode.Success);
                        }
                    }
                }
                finally
                {
                    if (cap.hContainer != IntPtr.Zero)
                    {
                        FreeMemory(cap.hContainer);
                        cap.hContainer = IntPtr.Zero;
                    }
                }
            }

            return (rc == ResultCode.Success);
        } 
#endif

        private unsafe bool SetCapOneValue(ushort capability, DataTypes type, uint value)
        {
            ResultCode rc = ResultCode.Failure;

            if (state >= TwainState.SourceOpen)
            {
                TwainStructs.Capability cap = new TwainStructs.Capability();
                cap.Cap = capability;
                cap.ConType = ContainerType.OneValue;

                cap.hContainer = AllocateMemory(Marshal.SizeOf(typeof(TwainStructs.OneValue)));
                try
                {
                    if (cap.hContainer != IntPtr.Zero)
                    {
                        IntPtr ptr = LockMemory(cap.hContainer);

                        TwainStructs.OneValue* data = (TwainStructs.OneValue*)ptr.ToPointer();
                        data->ItemType = type;
                        data->Item = value;

                        UnlockMemory(cap.hContainer);

                        rc = twainCapability(ref appId, ref deviceId, DataGroup.Control, DataArgumentType.Capability, TwainMessages.Set, ref cap);

                        if (rc == ResultCode.Failure)
                        {
                            TwainStructs.Status status = new TwainStructs.Status();
                            ResultCode src = DSMStatus(ref status);

                            System.Diagnostics.Debug.Assert(src == ResultCode.Success);
                        }
                    }
                }
                finally
                {
                    if (cap.hContainer != IntPtr.Zero)
                    {
                        FreeMemory(cap.hContainer);
                        cap.hContainer = IntPtr.Zero;
                    }
                }
            }

            return (rc == ResultCode.Success);
        }

        private void EnumAvailableSources()
        {
            if (OpenSourceManager())
            {
                TwainStructs.Identity defaultSource = new TwainStructs.Identity();

                ResultCode rc = DSMIdentity(DataGroup.Control, TwainMessages.GetDefault, ref defaultSource);

                if (rc == ResultCode.Success)
                {
                    TwainStructs.Identity source = new TwainStructs.Identity();

                    rc = DSMIdentity(DataGroup.Control, TwainMessages.GetFirst, ref source);

                    if (rc == ResultCode.Success)
                    {
                        do
                        {
                            this.availableSources.Add(source);
                            rc = DSMIdentity(DataGroup.Control, TwainMessages.GetNext, ref source);

                        } while (rc == ResultCode.Success);

                        this.availableSources.Sort(new TwainIdentityComparer());
                        this.defaultSourceIndex = this.availableSources.FindIndex(i => i.ProductName == defaultSource.ProductName);
                    }
                }
            }
        }

        private IntPtr AllocateMemory(int size)
        {
            return SafeNativeMethods.GlobalAlloc(NativeConstants.GHND, new UIntPtr((ulong)size));
        }

        private void FreeMemory(IntPtr hMem)
        {
            SafeNativeMethods.GlobalFree(hMem);
        }

        private IntPtr LockMemory(IntPtr hMem)
        {
            return SafeNativeMethods.GlobalLock(hMem);
        }

        private void UnlockMemory(IntPtr hMem)
        {
            SafeNativeMethods.GlobalUnlock(hMem);
        }

        public ReadOnlyCollection<string> GetSourceList()
        {
            if (availableSources.Count == 0)
            {
                EnumAvailableSources();
            }

            List<string> list = new List<string>(availableSources.Count);

            for (int i = 0; i < availableSources.Count; i++)
            {
                list.Add(availableSources[i].ProductName);
            }

            return list.AsReadOnly();
        }

        public int DefaultSourceIndex
        {
            get
            {
                return defaultSourceIndex;
            }
        }

        public void SetSelectedSource(int index)
        {
            if (state == TwainState.SourceManagerOpen)
            {
                this.deviceId = this.availableSources[index];

                if (index != defaultSourceIndex)
                {
                    ResultCode rc = DSMIdentity(DataGroup.Control, TwainMessages.Set, ref deviceId);

#if DEBUG
                    if (rc == ResultCode.Failure)
                    {
                        TwainStructs.Status status = new TwainStructs.Status();
                        DSMStatus(ref status);

                        System.Diagnostics.Debug.WriteLine(status.ConditionCode);
                    }
#endif
                }
            }
        }

        public static bool IsAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(WindowsDirectory))
                {
                    string path = Path.Combine(WindowsDirectory, TwainDllName);

                    if (File.Exists(path))
                    {
                        using (SafeLibraryHandle hMod = UnsafeNativeMethods.LoadLibraryW(path))
                        {
                            if (UnsafeNativeMethods.GetProcAddress(hMod, TwainEntryPoint) != IntPtr.Zero)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
        }

        public bool AcquireToClipboard(IntPtr clipOwnerHWnd)
        {
            bool result = false;

            this.hostApp.DisableApplicationWindow();
            try
            {
                if (OpenSourceManager())
                {
                    if (OpenSource() && SetTransferCount(1))
                    {
                        this.hostApp.BringToForeground();
                        if (EnableSource())
                        {
                            IntPtr hDib = WaitForNativeXfer();

                            if (hDib != IntPtr.Zero)
                            {
                                if (SafeNativeMethods.OpenClipboard(clipOwnerHWnd))
                                {
                                    if (SafeNativeMethods.EmptyClipboard())
                                    {
                                        if (SafeNativeMethods.SetClipboardData(NativeConstants.CF_DIB, hDib) != IntPtr.Zero)
                                        {
                                            hDib = IntPtr.Zero; // The system now owns the DIB.
                                            result = true;
                                        }
                                    }
                                    SafeNativeMethods.CloseClipboard();
                                }

                                if (hDib != IntPtr.Zero)
                                {
                                    SafeNativeMethods.GlobalFree(hDib);
                                    hDib = IntPtr.Zero;
                                }
                            }
                        }
                    }
                    CloseSourceManager();
                }
            }
            finally
            {
                this.hostApp.EnableApplicationWindow();
            }

            return result;
        }

        private bool breakModalLoop;

        private void ModalMessageLoop()
        {
            this.breakModalLoop = false;

            NativeStructs.tagMSG msg;

            while (!breakModalLoop)
            {
                if (SafeNativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0) == 0)
                {
                    break; // WM_QUIT
                }

                if (!FilterMessage(msg))
                {
                    SafeNativeMethods.TranslateMessage(ref msg);
                    SafeNativeMethods.DispatchMessage(ref msg);
                }
            }

        }
        
        private IntPtr WaitForNativeXfer()
        {
            IntPtr hDib = IntPtr.Zero;

            if (state >= TwainState.SourceOpen)
            {
                if (SetTransferMech(TransferMech.Native))
                {
                    if (state == TwainState.TransferReady)
                    {
                        hDib = DoOneTransfer();
                    }
                    else
                    {
                        ModalMessageLoop();

                        hDib = this.latestDIB;
                    }
                }
            }

            return hDib;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~TwainProxy()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                CloseSourceManager();
                if (disposing)
                {
                    if (hModule != null)
                    {
                        hModule.Dispose();
                        hModule = null;
                    }

                }

                if (twEvent.pEvent != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(twEvent.pEvent);
                    twEvent.pEvent = IntPtr.Zero;
                }
            }
        }


    }
}
