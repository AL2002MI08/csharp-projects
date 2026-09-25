namespace WinInspect.Models
{
    public sealed class ProcessInspectionResult
    {
        public bool Success { get; }
        public uint ProcessId { get; }
        public bool Is64Bit { get; }
        public ulong WorkingSetSize { get; }
        public ulong PeakWorkingSetSize { get; }
        public int Win32ErrorCode { get; }
        public string? Win32ErrorMessage { get; }

        private ProcessInspectionResult(
            bool success,
            uint processId,
            bool is64Bit,
            ulong workingSetSize,
            ulong peakWorkingSetSize,
            int win32ErrorCode,
            string? win32ErrorMessage)
        {
            Success = success;
            ProcessId = processId;
            Is64Bit = is64Bit;
            WorkingSetSize = workingSetSize;
            PeakWorkingSetSize = peakWorkingSetSize;
            Win32ErrorCode = win32ErrorCode;
            Win32ErrorMessage = win32ErrorMessage;
        }

        public static ProcessInspectionResult Ok(uint processId, bool is64Bit, ulong workingSetSize, ulong peakWorkingSetSize) =>
            new(true, processId, is64Bit, workingSetSize, peakWorkingSetSize, 0, null);

        public static ProcessInspectionResult Fail(uint processId, int win32ErrorCode, string win32ErrorMessage) =>
            new(false, processId, false, 0, 0, win32ErrorCode, win32ErrorMessage);
    }
}