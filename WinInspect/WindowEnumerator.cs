using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using WinInspect.Models;

namespace WinInspect {

public static class WindowEnumerator
{
    public static WindowEnumerationResult GetTopLevelWindows(int maxCount = 5)
    {
        var windows = new List<WindowInfo>(maxCount);

        bool stoppedEarly = false;

        Native.EnumWindowsProc callback = (hWnd, lParam) =>
        {
            if (!Native.IsWindowVisible(hWnd))
            {
                return true;
            }

            int length = Native.GetWindowTextLengthW(hWnd);
            if (length == 0)
            {
                return true;
            }

            var title = new StringBuilder(length + 1);
            int copied = Native.GetWindowTextW(hWnd, title, title.Capacity);

            if (copied == 0)
            {
                return true;
            }

            windows.Add(new WindowInfo(hWnd, title.ToString()));

            if (windows.Count >= maxCount)
            {
                stoppedEarly = true;
                return false;
            }

            return true;
        };

        bool success = Native.EnumWindows(callback, IntPtr.Zero);
        int errorCode = Marshal.GetLastWin32Error();

        GC.KeepAlive(callback);

        if (!success && !stoppedEarly)
        {
            string message = new Win32Exception(errorCode).Message;
            return WindowEnumerationResult.Fail(errorCode, message);
        }

        return WindowEnumerationResult.Ok(windows);
    }
}
}

