using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace WinInspect;

public record WindowInfo(IntPtr Handle, string Title);

public class WindowEnumerationResult
{
    public bool Success { get; }
    public IReadOnlyList<WindowInfo> Windows { get; }
    public int Win32ErrorCode { get; }
    public string? Win32ErrorMessage { get; }

    private WindowEnumerationResult(
        bool success,
        IReadOnlyList<WindowInfo> windows,
        int win32ErrorCode,
        string? win32ErrorMessage)
    {
        Success = success;
        Windows = windows;
        Win32ErrorCode = win32ErrorCode;
        Win32ErrorMessage = win32ErrorMessage;
    }

    public static WindowEnumerationResult Ok(IReadOnlyList<WindowInfo> windows) =>
        new(true, windows, 0, null);

    public static WindowEnumerationResult Fail(int errorCode, string errorMessage) =>
        new(false, Array.Empty<WindowInfo>(), errorCode, errorMessage);
}

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
