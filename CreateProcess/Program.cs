using System;
using System.Runtime.InteropServices;

namespace CreateProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize
            string desktop_name = @"InstallationDesktop";
            
            //Create new Desktop for Installation
            IntPtr newDesktop = IntPtr.Zero;
            newDesktop = CreateDesktop(desktop_name, null, null, 0, ACCESS_MASK.GENERIC_ALL, IntPtr.Zero);

// SwitchDesktop(newDesktop);
            //Prep API structs
            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
            STARTUPINFO si = new STARTUPINFO();
            si.lpDesktop = desktop_name;
            
            //Create new Process
            CreateProcess(args[0], '"' + args[0] + '"' + " " + args[1], IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref si, out pi);
        }

        [DllImport("user32.dll", EntryPoint = "SwitchDesktop", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SwitchDesktop(IntPtr hDesktop);

        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
                      bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
                      string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("user32.dll", EntryPoint = "CreateDesktop", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateDesktop(
                [MarshalAs(UnmanagedType.LPWStr)] string desktopName,
                [MarshalAs(UnmanagedType.LPWStr)] string device, // must be null.
                [MarshalAs(UnmanagedType.LPWStr)] string deviceMode, // must be null,
                [MarshalAs(UnmanagedType.U4)] int flags,  // use 0
                [MarshalAs(UnmanagedType.U4)] ACCESS_MASK accessMask,
                IntPtr attributes);
    }
}

public struct PROCESS_INFORMATION {
    public IntPtr hProcess;
    public IntPtr hThread;
    public uint dwProcessId;
    public uint dwThreadId;
}

public struct STARTUPINFO {
    public uint cb;
    public string lpReserved;
    public string lpDesktop;
    public string lpTitle;
    public uint dwX;
    public uint dwY;
    public uint dwXSize;
    public uint dwYSize;
    public uint dwXCountChars;
    public uint dwYCountChars;
    public uint dwFillAttribute;
    public uint dwFlags;
    public short wShowWindow;
    public short cbReserved2;
    public IntPtr lpReserved2;
    public IntPtr hStdInput;
    public IntPtr hStdOutput;
    public IntPtr hStdError;
}

public struct SECURITY_ATTRIBUTES {
    public int length;
    public IntPtr lpSecurityDescriptor;
    public bool bInheritHandle;
}
[Flags]
public enum ACCESS_MASK : uint
{
    DELETE = 0x00010000,
    READ_CONTROL = 0x00020000,
    WRITE_DAC = 0x00040000,
    WRITE_OWNER = 0x00080000,
    SYNCHRONIZE = 0x00100000,

    STANDARD_RIGHTS_REQUIRED = 0x000F0000,

    STANDARD_RIGHTS_READ = 0x00020000,
    STANDARD_RIGHTS_WRITE = 0x00020000,
    STANDARD_RIGHTS_EXECUTE = 0x00020000,

    STANDARD_RIGHTS_ALL = 0x001F0000,

    SPECIFIC_RIGHTS_ALL = 0x0000FFFF,

    ACCESS_SYSTEM_SECURITY = 0x01000000,

    MAXIMUM_ALLOWED = 0x02000000,

    GENERIC_READ = 0x80000000,
    GENERIC_WRITE = 0x40000000,
    GENERIC_EXECUTE = 0x20000000,
    GENERIC_ALL = 0x10000000,

    DESKTOP_READOBJECTS = 0x00000001,
    DESKTOP_CREATEWINDOW = 0x00000002,
    DESKTOP_CREATEMENU = 0x00000004,
    DESKTOP_HOOKCONTROL = 0x00000008,
    DESKTOP_JOURNALRECORD = 0x00000010,
    DESKTOP_JOURNALPLAYBACK = 0x00000020,
    DESKTOP_ENUMERATE = 0x00000040,
    DESKTOP_WRITEOBJECTS = 0x00000080,
    DESKTOP_SWITCHDESKTOP = 0x00000100,

    WINSTA_ENUMDESKTOPS = 0x00000001,
    WINSTA_READATTRIBUTES = 0x00000002,
    WINSTA_ACCESSCLIPBOARD = 0x00000004,
    WINSTA_CREATEDESKTOP = 0x00000008,
    WINSTA_WRITEATTRIBUTES = 0x00000010,
    WINSTA_ACCESSGLOBALATOMS = 0x00000020,
    WINSTA_EXITWINDOWS = 0x00000040,
    WINSTA_ENUMERATE = 0x00000100,
    WINSTA_READSCREEN = 0x00000200,

    WINSTA_ALL_ACCESS = 0x0000037F
}