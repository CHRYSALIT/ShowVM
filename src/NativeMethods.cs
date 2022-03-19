using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShowVM
{

    internal delegate bool EnumDelegate(IntPtr hWnd, int lParam);

    internal static class NativeMethods
    {
        /// <summary>
        /// Enables the specified process to set the foreground window using the SetForegroundWindow function.<br />
        /// The calling process must already be able to set the foreground window.
        /// </summary>
        /// <param name="dwProcessId">The identifier of the process that will be enabled to set the foreground window.<br />
        /// If this parameter is ASFW_ANY (int: -1), all processes will be enabled to set the foreground window.</param>
        /// <remarks>See <see cref="SetForegroundWindow(IntPtr)"/> for more information.</remarks>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool AllowSetForegroundWindow([In] uint dwProcessId);


        /// <summary>
        /// Brings the specified window to the top of the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window to bring to the top of the Z order.</param>
        [DllImport("user32.dll")]
        internal static extern bool BringWindowToTop([In] IntPtr hWnd);

        /// <summary>
        /// Enumerates all top-level windows associated with the specified desktop.<br />
        /// It passes the handle to each window, in turn, to an application-defined callback function.<br />
        /// You must ensure that the callback function sets <see cref="SetLastErr"/>SetLastError if it fails.
        /// </summary>
        /// <param name="hDesktop">A handle to the desktop whose top-level windows are to be enumerated. If this parameter is NULL, the current desktop is used.</param>
        /// <param name="lpEnumFunc">A pointer to an application-defined EnumWindowsProc callback function.</param>
        /// <param name="lParam">An application-defined value to be passed to the callback function.</param>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool EnumDesktopWindows([Optional][In] IntPtr hDesktop, [In] EnumDelegate lpEnumFunc, [In] IntPtr lParam);

        /// <summary>
        /// Enumerates all top-level windows on the screen by passing the handle to each window, in turn, to an application-defined callback function.<br />
        /// EnumWindows continues until the last top-level window is enumerated or the callback function returns FALSE.
        /// </summary>
        /// <param name="lpEnumFunc">A pointer to an application-defined callback function.</param>
        /// <param name="lParam">An application-defined value to be passed to the callback function.</param>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool EnumWindows([In] EnumDelegate lpEnumFunc, [In] IntPtr lParam);

        /// <summary>
        /// Retrieves a handle to the desktop window. <br />
        /// The desktop window covers the entire screen. The desktop window is the area on top of which other windows are painted.
        /// </summary>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).<br />
        /// The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
        /// </summary>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Examines the Z order of the child windows associated with the specified parent window and retrieves a handle to the child window at the top of the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the parent window whose child windows are to be examined.<br />
        /// If this parameter is NULL, the function returns a handle to the window at the top of the Z order.</param>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetTopWindow([Optional][In] IntPtr hWnd);


        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one) into a buffer.<br />
        /// If the specified window is a control, the text of the control is copied.<br />
        /// However, GetWindowText cannot retrieve the text of a control in another application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control containing the text.</param>
        /// <param name="lpWindowText">The buffer that will receive the text.<br/>
        /// If the string is as long or longer than the buffer, the string is truncated and terminated with a null character.</param>
        /// <param name="nMaxCount">The maximum number of characters to copy to the buffer, including the null character.<br />
        /// If the text exceeds this limit, it is truncated.</param>
        /// <returns>If the function succeeds, the return value is the length, in characters, of the copied string, not including the terminating null character.</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowText", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint GetWindowText([In] IntPtr hWnd, [Out] StringBuilder lpWindowText, [In] int nMaxCount);


        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.
        /// </summary>
        /// <param name="lpdwProcessId">A pointer to a variable that receives the process identifier.<br />
        /// If this parameter is not NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it does not.</param>
        /// <returns>The return value is the identifier of the thread that created the window.</returns>
        [DllImport("user32.dll")]
        internal static extern uint GetWindowThreadProcessId([In] IntPtr hWnd, [Optional][Out] out uint lpdwProcessId);

        /// <inheritdoc cref="GetWindowThreadProcessId"/>
        [DllImport("user32.dll")]
        internal static extern uint GetWindowThreadProcessId([In] IntPtr hWnd, [Optional][Out] IntPtr lpdwProcessId);

        /// <summary>
        /// Determines whether the specified window is minimized (iconic).
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        [DllImport("user32.dll")]
        internal static extern bool IsIconic([In] IntPtr hWnd);

        /// <summary>
        /// Determines whether the specified window handle identifies an existing window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool IsWindow([Optional][In] IntPtr hWnd);

        /// <summary>
        /// Determines the visibility state of the specified window.<br />
        /// Any drawing to a window with the WS_VISIBLE style will not be displayed if the window is obscured by other windows or is clipped by its parent window.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        [DllImport("user32.dll")]
        internal static extern bool IsWindowVisible([In] IntPtr hWnd);


        /// <summary>
        /// Determines whether a window is maximized.<br/>
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        [DllImport("user32.dll")]
        internal static extern bool IsZoomed([In] IntPtr hWnd);


        /// <summary>
        /// Restores a minimized (iconic) window to its previous size and position; it then activates the window.<br />
        /// </summary>
        /// <param name="hWnd">A handle to the window to be restored and activated.</param>
        [DllImport("user32.dll")]
        internal static extern bool OpenIcon([In] IntPtr hWnd);


        /// <summary>
        /// Activates a window.<br />
        /// The window must be attached to the calling thread's message queue.
        /// </summary>
        /// <param name="hWnd">A handle to the top-level window to be activated</param>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetActiveWindow([In] IntPtr hWnd);


        /// <summary>
        /// Sets the keyboard focus to the specified window.<br />
        /// The window must be attached to the calling thread's message queue.
        /// </summary>
        /// <param name="hWnd">A handle to the window that will receive the keyboard input.<br />
        /// If this parameter is NULL, keystrokes are ignored.</param>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetFocus([Optional][In] IntPtr hWnd);

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window.<br />
        /// Keyboard input is directed to the window, and various visual cues are changed for the user.<br />
        /// The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <remarks>
        /// The system restricts which processes can set the foreground window. A process can set the foreground window only if one of the following conditions is true:
        /// <list type="bullet">
        /// <item>The process is the foreground process.</item>
        /// <item>The process was started by the foreground process.</item>
        /// <item>The process received the last input event.</item>
        /// <item>There is no foreground process.</item>
        /// <item>The process is being debugged.</item>
        /// <item>The foreground process is not a Modern Application or the Start Screen.</item>
        /// <item>The foreground is not locked (see LockSetForegroundWindow).</item>
        /// <item>The foreground lock time-out has expired(see SPI_GETFOREGROUNDLOCKTIMEOUT in SystemParametersInfo).</item>
        /// <item>No menus are active.</item>
        /// </list>
        /// An application cannot force a window to the foreground while the user is working with another window. <br /> 
        /// Instead, Windows flashes the taskbar button of the window to notify the user.
        /// </remarks>
        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow([In]IntPtr hWnd);




        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nCmdShow">Controls how the window is to be shown.</param>
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow([In] IntPtr hWnd, [In] EnumShow nCmdShow);

        /// <summary>
        /// Sets the show state of a window without waiting for the operation to complete.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nCmdShow">ontrols how the window is to be shown.</param>
        [DllImport("user32.dll")]
        internal static extern bool ShowWindowAsync([In] IntPtr hWnd, [In] EnumShow nCmdShow);


        internal enum EnumSPIF : uint
        {
            /// <summary>
            /// Default
            /// </summary>
            NONE = 0,

            /// <summary>
            /// Writes the new system-wide parameter setting to the user profile.
            /// </summary>
            SPIF_UPDATEINIFILE = 1,

            /// <summary>
            /// Broadcasts the WM_SETTINGCHANGE message after updating the user profile.
            /// </summary>
            SPIF_SENDCHANGE = SPIF_SENDWININICHANGE,

            /// <summary>
            /// Same as <see cref="SPIF_SENDCHANGE"/>.
            /// </summary>
            SPIF_SENDWININICHANGE = 2
        }

        #region SystemParametersInfo
        /// <summary>
        /// Retrieves or sets the value of one of the system-wide parameters.<br />
        /// This function can also update the user profile while setting a parameter.
        /// </summary>
        /// <param name="uiAction">The system-wide parameter to be retrieved or set.</param>
        /// <param name="uiParam">A parameter whose usage and format depends on the system parameter being queried or set.<br /> 
        /// For more information about system-wide parameters, see the uiAction parameter.<br />
        /// If not otherwise indicated, you must specify zero for this parameter.</param>
        /// <param name="pvParam">A parameter whose usage and format depends on the system parameter being queried or set.<br />
        /// For more information about system-wide parameters, see the uiAction parameter.<br />
        /// If not otherwise indicated, you must specify NULL for this parameter.</param>
        /// <param name="fWinIni">See <see cref="EnumSPIF"/></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SystemParametersInfo([In] uint uiAction, [In] uint uiParam, [In][Out] IntPtr pvParam, [In] uint fWinIni);

        /// <inheritdoc cref="SystemParametersInfo"/>
        /// <remarks>Used to get <see cref="uint"/> information.</remarks>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SystemParametersInfo([In] uint uiAction, [In] uint uiParam, [In][Out] ref uint pvParam, [In] uint fWinIni);

        /// <inheritdoc cref="SystemParametersInfo"/>
        /// <remarks>Used to set <see cref="uint"/> information.</remarks>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SystemParametersInfo([In] uint uiAction, [In] uint uiParam, [In][Out] uint pvParam, [In] uint fWinIni);
        #endregion




        #region Window Thread
        /// <summary>
        /// Attaches or detaches the input processing mechanism of one thread to that of another thread.
        /// </summary>
        /// <param name="idAttach">The identifier of the thread to be attached to another thread. The thread to be attached cannot be a system thread.</param>
        /// <param name="idAttachTo">The identifier of the thread to which idAttach will be attached. This thread cannot be a system thread.</param>
        /// <param name="fAttach">If this parameter is TRUE, the two threads are attached.<br />
        /// If the parameter is FALSE, the threads are detached.</param>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool AttachThreadInput([In] uint idAttach, [In] uint idAttachTo, [In] bool fAttach);

        /// <summary>
        /// Retrieves the thread identifier of the calling thread.
        /// </summary>
        [DllImport("kernel32.dll")]
        internal static extern uint GetCurrentThreadId();
        #endregion

        /// <summary>
        /// <list type="bullet">
        /// <item><see cref="https://www.codeproject.com/Articles/2286/Window-Hiding-with-C?fid=3881&df=90&mpp=25&sort=Position&spc=Relaxed&prof=True&view=Normal&fr=26#xx0xx"/></item>
        /// <item><see cref="https://www.codeproject.com/Articles/2286/Window-Hiding-with-C?fid=3881&df=90&mpp=25&sort=Position&spc=Relaxed&prof=True&view=Normal&fr=26#xx0xx"/></item>
        /// <item><see cref="http://pinvoke.net/default.aspx/user32/SetForegroundWindow.html"/></item>
        /// </list>
        /// </summary>
        /// <param name="hWnd"></param>
        internal static void SetForegroundWindowInternal(IntPtr hWnd)
        {
            if (!IsWindow(hWnd)) return;

            int lockout = 0;
            var hCurrWnd = GetForegroundWindow();
            //System.Threading.Thread.CurrentThread.ManagedThreadId
            var thisTID = GetCurrentThreadId();
            var ForTID = GetWindowThreadProcessId(hCurrWnd, IntPtr.Zero);
            var WindowTID = GetWindowThreadProcessId(hWnd, IntPtr.Zero);

            int ASFW_ANY = -1;

            IntPtr plockout = Marshal.AllocHGlobal(sizeof(int));
            Marshal.StructureToPtr(lockout, plockout, true);

            if (thisTID != WindowTID)
            {
                /*
                IntPtr pv = Marshal.AllocHGlobal(sizeof(int));
                Marshal.StructureToPtr(0, pv, true);

                AttachThreadInput(WindowTID, thisTID, true);
                SystemParametersInfo(0x2000, 0, plockout, 0);
                
                lockout = Marshal.ReadInt32(plockout);

                SystemParametersInfo(0x2001, 0, pv, (uint)(SPIF.SPIF_SENDWININICHANGE | SPIF.SPIF_UPDATEINIFILE));
                */
                AllowSetForegroundWindow((uint)ASFW_ANY);
            }

            SetForegroundWindow(hWnd);
            ShowWindow(hWnd, EnumShow.SW_SHOW);
            BringWindowToTop(hWnd);

            if (thisTID != WindowTID)
            {
                SystemParametersInfo(0x2001, 0, plockout, (uint)(EnumSPIF.SPIF_SENDWININICHANGE | EnumSPIF.SPIF_UPDATEINIFILE));
                AttachThreadInput(WindowTID, thisTID, false);
            }

            Marshal.FreeHGlobal(plockout);

        }
    }
}
