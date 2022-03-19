using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShowVM
{
    /// <summary>
    /// Manage Windows
    /// </summary>
    internal class WindowManager
    {
        private readonly List<Window> _windows = new List<Window>();

        /// <summary>
        /// Discover Windows
        /// </summary>
        private bool AcquireWindows()
        {
            return NativeMethods.EnumDesktopWindows(IntPtr.Zero, FilterCallback, IntPtr.Zero);
        }

        /// <summary>
        /// Display Windows
        /// </summary>
        internal void ListWindows()
        {
            if (AcquireWindows())
            {
                Console.WriteLine();
                
                if (_windows.Count == 0)
                {
                    Console.WriteLine("No VirtualBox windows found.");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"  {"[Handle]",-12}  Title");
                Console.WriteLine($"  {"--------",-12}  -----");
                _windows.ForEach(window =>
                {
                    Console.WriteLine($"  {$"[{window.WindowHandle}]",-12}  {window}");
                });
                Console.WriteLine();
            }
        }

        internal void ShowWindows(ShowWindowsCommandOptions options)
        {
            if (AcquireWindows())
            {
                if (options.Verbose && options.Monitor == 0) Console.WriteLine($"Monitor: Restore all VM monitors");
                
                var matching_windows = _windows
                    .Where(window =>
                    {
                        var result = Regex.IsMatch(window.Title, options.Name, RegexOptions.IgnoreCase);
                        if (options.Verbose)
                        {
                            Console.WriteLine($"VM Window {window.Title} matching name {options.Name}: {result}");
                        }

                        if (options.Monitor != 0)
                        {
                            if (options.Verbose) Console.WriteLine($"Monitor: Restore only VM monitor {options.Monitor}");
                            var pattern = $"{options.Monitor}$";
                            result = Regex.IsMatch(window.Title, pattern, RegexOptions.IgnoreCase);
                        }
                        return result;
                    }).ToList();

                    matching_windows.All(window =>
                        {
                            if (options.Verbose) Console.WriteLine($"Restore {window.Title}");
                            RestoreWindow(window.WindowHandle, options);
                            return true;
                        });
            }
        }

        private void RestoreWindow(IntPtr hWnd, ShowWindowsCommandOptions options)
        {
            var isIcon = NativeMethods.IsIconic(hWnd);
            if (options.Verbose) Console.WriteLine($"Is Iconic {isIcon}");

            var isMax = NativeMethods.IsZoomed(hWnd);
            if (options.Verbose) Console.WriteLine($"Is Maximized: {isMax}");

            var ret = NativeMethods.ShowWindowAsync(hWnd, options.Display);
            if (options.Verbose) Console.WriteLine($"Show as {options.Display}: {ret}");

            if (options.Front)
            {
                ret = NativeMethods.BringWindowToTop(hWnd);
                if (options.Verbose) Console.WriteLine($"Bring To Front: {ret}");

                ret = NativeMethods.SetForegroundWindow(hWnd);
                if (options.Verbose) Console.WriteLine($"Set ForeGround: {ret}");
            }
        }

        /// <summary>
        /// Callback to collect matching VM windows. Collect only matching "Oracle VM VirtualBox" windows.
        /// </summary>
        private bool FilterCallback(IntPtr hWnd, int lParam)
        {
            if (NativeMethods.IsWindowVisible(hWnd))
            {
                // Get the window's title.
                StringBuilder sb_title = new StringBuilder(1024);
                _ = NativeMethods.GetWindowText(hWnd, sb_title, sb_title.Capacity);

                // Filter only "Oracle VM VirtualBox" window's title
                if (Regex.IsMatch(sb_title.ToString(), "Oracle VM VirtualBox", RegexOptions.IgnoreCase))
                {
                    _windows.Add(
                        new Window()
                        {
                            WindowHandle = hWnd,
                            Title = sb_title.ToString()
                        }
                    );
                }
            }
            // Return true to indicate that we
            // should continue enumerating windows.
            return true;
        }



    }
}
