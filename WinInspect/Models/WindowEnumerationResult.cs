namespace WinInspect.Models
{
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
}