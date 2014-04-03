using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TwainProxy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
		    System.Diagnostics.Debugger.Launch();
#endif
            Application.Run(new Form1(args));
        }
    }
}
