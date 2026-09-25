using System.ComponentModel;
using System.Runtime.InteropServices;
using WinInspect.Models;

namespace WinInspect
{

    public static class ProcessInspector
    {
        public static ProcessInspectionResult Inspect(uint processId)
        {
            using SafeProcessHandle handle = Native.OpenProcess(Native.PROCESS_QUERY_LIMITED_INFORMATION, false, processId);

            if (handle.IsInvalid)
            {
                int errorCode = Marshal.GetLastWin32Error();
                string errorMessage = new Win32Exception(errorCode).Message;

                return ProcessInspectionResult.Fail(processId, errorCode, errorMessage);
            }

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

            bool is64Bit = Environment.Is64BitOperatingSystem && !isWow64;

            return ProcessInspectionResult.Ok(
                processId,
                is64Bit,
                counters.WorkingSetSize,
                counters.PeakWorkingSetSize);
        }
    }
}
