using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowVM
{
    internal interface ICommandOptions
    {
        [Option('v', "verbose", HelpText = "Set output to verbose messages.")]
        bool Verbose { get; set; }

        int Execute();
    }
}
