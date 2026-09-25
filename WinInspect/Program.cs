using System.Runtime.Versioning;
using WinInspect.Models;

[assembly: SupportedOSPlatform("windows")]

namespace WinInspect
{

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("WinInspect - minimal P/Invoke process inspector");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Inspect a process by PID");
                Console.WriteLine("2. EnumWindows callback demo");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                try
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            InspectProcess();
                            break;
                        case "2":
                            RunEnumWindowsDemo();
                            break;
                        case "3":
                            return;
                        default:
                            Console.WriteLine("Unrecognized option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        private static void InspectProcess()
        {
            Console.Write("Enter a process ID: ");
            if (!uint.TryParse(Console.ReadLine(), out uint pid))
            {
                Console.WriteLine("That is not a valid process ID.");
                return;
            }

            ProcessInspectionResult result = ProcessInspector.Inspect(pid);

            Console.WriteLine();
            if (!result.Success)
            {
                Console.WriteLine($"Could not open process {pid}.");
                Console.WriteLine($"Win32 error {result.Win32ErrorCode}: {result.Win32ErrorMessage}");
                return;
            }

            Console.WriteLine($"Process ID       : {result.ProcessId}");
            Console.WriteLine($"Architecture     : {(result.Is64Bit ? "64-bit" : "32-bit")}");
            Console.WriteLine($"Working Set      : {result.WorkingSetSize / 1024:N0} KB");
            Console.WriteLine($"Peak Working Set : {result.PeakWorkingSetSize / 1024:N0} KB");
        }

        private static void RunEnumWindowsDemo()
        {
            Console.WriteLine();
            Console.WriteLine("Enumerating top-level windows (first 5 visible, titled windows)...");

            WindowEnumerationResult result = WindowEnumerator.GetTopLevelWindows(maxCount: 5);

            if (!result.Success)
            {
                Console.WriteLine($"EnumWindows failed (Win32 error {result.Win32ErrorCode}: {result.Win32ErrorMessage}).");
                return;
            }

            foreach (WindowInfo window in result.Windows)
            {
                Console.WriteLine($"  hWnd=0x{window.Handle:X}  \"{window.Title}\"");
            }
        }
    }
}
