using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowVM
{
    [Verb("list", isDefault: true, HelpText = "Display current visible windows titles")]
    internal class ListWindowsCommandOptions : BaseCommandOptions
    {
        public override int Execute()
        {
            _windowManager.ListWindows();
            return 0;
        }

        [Usage(ApplicationAlias = "ShowVM.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Enumerate VirtualBox windows titles", new ListWindowsCommandOptions());
            }
        }
    }
}
