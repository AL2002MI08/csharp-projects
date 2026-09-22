using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WinInspect;

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

public static class ProcessInspector
{
    public static ProcessInspectionResult Inspect(uint processId)
    {
        IntPtr handle = Native.OpenProcess(Native.PROCESS_QUERY_LIMITED_INFORMATION, false, processId);

        if (handle == IntPtr.Zero)
        {
            int errorCode = Marshal.GetLastWin32Error();
            string errorMessage = new Win32Exception(errorCode).Message;

            return ProcessInspectionResult.Fail(processId, errorCode, errorMessage);
        }

        try
        {
            if (!Native.IsWow64Process(handle, out bool isWow64))
            {
                int errorCode = Marshal.GetLastWin32Error();
                return ProcessInspectionResult.Fail(processId, errorCode, new Win32Exception(errorCode).Message);
            }

            if (!Native.GetProcessMemoryInfo(
                    handle,
                    out Native.PROCESS_MEMORY_COUNTERS counters,
                    (uint)Marshal.SizeOf<Native.PROCESS_MEMORY_COUNTERS>()))
            {
                int errorCode = Marshal.GetLastWin32Error();
                return ProcessInspectionResult.Fail(processId, errorCode, new Win32Exception(errorCode).Message);
            }

            bool is64Bit = !isWow64;

            return ProcessInspectionResult.Ok(
                processId,
                is64Bit,
                counters.WorkingSetSize,
                counters.PeakWorkingSetSize);
        }
        finally
        {
            Native.CloseHandle(handle);
        }
    }
}
