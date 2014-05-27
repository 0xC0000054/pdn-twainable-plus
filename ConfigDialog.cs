using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using PaintDotNet.Effects;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Runtime.InteropServices;
using TwainablePlus.Properties;

namespace TwainablePlus
{
    internal partial class ConfigDialog : EffectConfigDialog
    {
        private Process process;
        private static readonly uint ProxyDataMessage = SafeNativeMethods.RegisterWindowMessageW("TwainProxyData");
        private IntPtr proxyHWnd;
        private bool scanRunning;

        public ConfigDialog()
        {
            InitializeComponent();
            this.scanRunning = false;
        }

        protected override void InitialInitToken()
        {
            base.theEffectToken = new ConfigToken();
        }

        protected override void InitDialogFromToken(PaintDotNet.Effects.EffectConfigToken effectTokenCopy)
        {
        }

        protected override void InitTokenFromDialog()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string path = Path.Combine(Path.GetDirectoryName(typeof(ConfigDialog).Assembly.Location), "TwainProxy.exe");
            if (File.Exists(path))
            {
                string hWnd = this.Handle.ToString();
                process = Process.Start(path, hWnd);
            }
            else
            {
                if (MessageBox.Show(this, Resources.TwainProxyNotFound, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.OK)
                {
                    base.Close();
                }
            }

            this.Cursor = Cursors.AppStarting;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SafeNativeMethods.PostMessageW(proxyHWnd, NativeConstants.WM_CLOSE, UIntPtr.Zero, IntPtr.Zero);

            base.OnFormClosing(e);
        }

        private void LoadSourcesCompleted(IntPtr data)
        {
            string[] sources = null;
            int defaultIndex = -1;

            int count = Marshal.ReadInt32(data);

            if (count > 0)
            {
                sources = new string[count];
                int offset = 4;

                const string DefaultItemPrefix = "Default:";

                for (int i = 0; i < count; i++)
                {
                    int length = Marshal.ReadInt32(data, offset);
                    offset += 4;

                    string item = Marshal.PtrToStringUni(new IntPtr(data.ToInt64() + (long)offset), length / 2).TrimEnd('\0'); // 2 bytes per char
                    offset += length;

                    if ((defaultIndex == -1) && item.StartsWith(DefaultItemPrefix, StringComparison.Ordinal))
                    {
                        item = item.Remove(0, DefaultItemPrefix.Length);
                        defaultIndex = i;
                    }

                    sources[i] = item;
                }

            }

            if (sources != null)
            {
                this.selectSourceCbo.Items.AddRange(sources);
                this.selectSourceCbo.SelectedIndex = defaultIndex;

                // Disable the source selection combo box unless there is more than one source available.
                this.selectSourceCbo.Enabled = this.selectSourceCbo.Items.Count > 1;
                this.acquireBtn.Enabled = true;
                this.autoCloseCb.Enabled = true;
            }

            this.Cursor = Cursors.Default;
        }

        private void ScanCompleted(IntPtr result)
        {
            this.scanRunning = false;
            this.Cursor = Cursors.Default;

            if (result != IntPtr.Zero)
            {
                base.DialogResult = System.Windows.Forms.DialogResult.OK;

                if (autoCloseCb.Checked)
                {
                    base.Close();
                }
            }

        }

        private void ScanControlsEnabled(bool enabled)
        {
            this.selectSourceCbo.Enabled = enabled ? this.selectSourceCbo.Items.Count > 1 : false;
            this.acquireBtn.Enabled = enabled;
            this.autoCloseCb.Enabled = enabled;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void acquireBtn_Click(object sender, System.EventArgs e)
        {
            if (!scanRunning)
            {
                this.scanRunning = true;
                base.Cursor = Cursors.WaitCursor;

                IntPtr source = new IntPtr(selectSourceCbo.SelectedIndex);

                SafeNativeMethods.PostMessageW(proxyHWnd, ProxyDataMessage, new UIntPtr(TwainProxy.ProxyDataMessages.StartScan), source);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == ProxyDataMessage)
            {
                int action = m.WParam.ToInt32();

                if (action == TwainProxy.ProxyDataMessages.GetProxyWindowHandle)
                {
                    this.proxyHWnd = m.LParam;
                }
                else if (action == TwainProxy.ProxyDataMessages.ScanEnableWindow)
                {
                    ScanControlsEnabled(m.LParam != IntPtr.Zero);
                }
                else if (action == TwainProxy.ProxyDataMessages.ScanCompletedCallback)
                {
                    ScanCompleted(m.LParam);
                }

            }
            else if (m.Msg == NativeConstants.WM_COPYDATA)
            {
                if (m.WParam == proxyHWnd)
                {
                    NativeStructs.COPYDATASTRUCT cds = (NativeStructs.COPYDATASTRUCT)m.GetLParam(typeof(NativeStructs.COPYDATASTRUCT));

                    LoadSourcesCompleted(cds.lpData);
                    m.Result = new IntPtr(1);
                }
            }

            base.WndProc(ref m);
        }

    }
}
