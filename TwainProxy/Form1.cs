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
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.InteropServices;

namespace TwainProxy
{
    internal partial class Form1 : Form, ITwainApplication
    {
        private TwainProxy twain;
        private static readonly uint ProxyDataMessage = SafeNativeMethods.RegisterWindowMessageW("TwainProxyData");

        private readonly IntPtr ClientHWnd;

        public Form1(string[] args)
        {
            ClientHWnd = new IntPtr(long.Parse(args[0], CultureInfo.InvariantCulture));

            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (twain == null)
            {
                twain = new TwainProxy(this);
            }

            SafeNativeMethods.SendMessageW(ClientHWnd, ProxyDataMessage, new UIntPtr(ProxyDataMessages.GetProxyWindowHandle), base.Handle);
            LoadSources();

            base.OnHandleCreated(e);
        }

        private static int GetBufferLength(string[] items)
        {
            int size = sizeof(int);

            Encoding unicode = Encoding.Unicode;

            for (int i = 0; i < items.Length; i++)
            {
                size += sizeof(int);
                size += unicode.GetByteCount(items[i]);
            }

            return size;
        }

        private void LoadSources()
        {
            var sources = twain.GetSourceList();
            try
            {
                string[] sourceStrings = new string[sources.Count];

                for (int i = 0; i < sourceStrings.Length; i++)
                {
                    if (i == twain.DefaultSourceIndex)
                    {
                        sourceStrings[i] = "Default:" + sources[i] + "\0";
                    }
                    else
                    {
                        sourceStrings[i] = sources[i] + "\0";
                    }
                }

                int bufferSize = GetBufferLength(sourceStrings);

                IntPtr buffer = Marshal.AllocHGlobal(bufferSize);
                try
                {
                    int offset = 0;

                    Marshal.WriteInt32(buffer, sourceStrings.Length);
                    offset += 4;

                    Encoding unicode = Encoding.Unicode;

                    for (int i = 0; i < sourceStrings.Length; i++)
                    {
                        byte[] bytes = unicode.GetBytes(sourceStrings[i]);

                        Marshal.WriteInt32(buffer, offset, bytes.Length);
                        offset += 4;
                        Marshal.Copy(bytes, 0, new IntPtr(buffer.ToInt64() + (long)offset), bytes.Length);
                        offset += bytes.Length;
                    }

                    NativeStructs.COPYDATASTRUCT cds = new NativeStructs.COPYDATASTRUCT();
                    cds.cbData = bufferSize;
                    cds.lpData = buffer;

                    SafeNativeMethods.SendCopyData(ClientHWnd, NativeConstants.WM_COPYDATA, this.Handle, ref cds);
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                    buffer = IntPtr.Zero;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void ScanToClipboard(int sourceIndex)
        {
            twain.SetSelectedSource(sourceIndex);

            IntPtr result = IntPtr.Zero;

            if (twain.AcquireToClipboard(ClientHWnd))
            {
                result = new IntPtr(1);
            }

            SafeNativeMethods.SendMessageW(ClientHWnd, ProxyDataMessage, new UIntPtr(ProxyDataMessages.ScanCompletedCallback), result);
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!IsHandleCreated)
            {
                base.CreateHandle();
            }

            value = false;

            base.SetVisibleCore(value);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == ProxyDataMessage)
            {
                int action = m.WParam.ToInt32();

                if (action == ProxyDataMessages.StartScan)
                {
                    int selectedSource = m.LParam.ToInt32();
                    ScanToClipboard(selectedSource);
                }
            }

            base.WndProc(ref m);
        }

        public IntPtr WindowHandle
        {
            get { return base.Handle; }
        }

        public void DisableApplicationWindow()
        {
            SafeNativeMethods.SendMessageW(ClientHWnd, ProxyDataMessage, new UIntPtr(ProxyDataMessages.ScanEnableWindow), IntPtr.Zero);
        }

        public void EnableApplicationWindow()
        {
            SafeNativeMethods.SendMessageW(ClientHWnd, ProxyDataMessage, new UIntPtr(ProxyDataMessages.ScanEnableWindow), new IntPtr(1));
        }

        public void BringToForeground()
        {
            base.Activate();
        }
    }
}
