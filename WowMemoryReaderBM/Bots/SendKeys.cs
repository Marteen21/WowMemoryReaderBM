using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WowMemoryReaderBM.Bots {
    public class SendKeys {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const Int32 WM_KEYDOWN = 0x0100;
        private const Int32 WM_KEYUP = 0x0101;

        public static void Send(Constants.Const.WindowsVirtualKey Key) {
            IntPtr Handle = FindWindow(null, "World of Warcraft");
            PostMessage(Handle, WM_KEYDOWN,(int)Key, 0);
            PostMessage(Handle, WM_KEYUP, (int)Key, 0);
        }

        
    }
}
