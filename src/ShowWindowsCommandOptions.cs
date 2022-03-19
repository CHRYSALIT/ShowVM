using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowVM
{

    [Verb("show", HelpText = "Show VM window(s) with options specified")]
    internal class ShowWindowsCommandOptions : BaseCommandOptions
    {
        public override int Execute()
        {
            _windowManager.ShowWindows(this);
            return 0;
        }

        [Usage(ApplicationAlias = "ShowVM.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Restore windows for VM MyVM", new ShowWindowsCommandOptions() { Name = "MyVM" });
                yield return new Example("Restore windows for VM MyVM and force foreground", new ShowWindowsCommandOptions() { Name = "MyVM", Front = true });
                yield return new Example("Maximize windows for VM MyVM", new ShowWindowsCommandOptions() { Name = "MyVM", Display = EnumShow.SW_SHOWMAXIMIZED });
                yield return new Example("Maximize window for monitor 1 of VM MyVM", new ShowWindowsCommandOptions() { Name = "MyVM", Display = EnumShow.SW_SHOWMAXIMIZED, Monitor = 1});
            }
        }

        [Option('n', "name", Required = true, HelpText = "Partial or full VM window title or Regex pattern")]
        public string Name { get; set; }

        private int _monitor = 0;
        [Option('m', "monitor", Default = 0, Required = false, HelpText = "0 represent all VM virtual monitors, [1-8] to specify a VM monitor.")]
        public int Monitor
        {
            get { return _monitor; }
            set
            {
                if (value > -1 && value < 8)
                {
                    _monitor = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("monitor", "1 to 8");
                }
            }
        }

        [Option('d', "display", Default = EnumShow.SW_RESTORE, Required = false, HelpText = "Window display mode\n", Separator =',')]
        public EnumShow Display { get; set; } = EnumShow.SW_RESTORE;

        [Option('f', "front", Default = false, Required = false, HelpText = "Set VM window(s) foreground.")]
        public bool Front { get; set; } = false;

        /*
        [Option('s', "sort", Default = false, Required = false, HelpText = "Order VM taskbar icons.")]
        public bool Sort { get; set; } = false;
        */
    }
}
