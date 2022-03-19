using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShowVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(settings =>
            {
                settings.AutoVersion = false;
                settings.AutoHelp = true;
                settings.EnableDashDash = true;
                settings.HelpWriter = null;
                settings.CaseSensitive = false;
            });

            ParserResult<object> parserResult = null;
            try
            {
                parserResult = parser.ParseArguments<ListWindowsCommandOptions, ShowWindowsCommandOptions>(args);
            }
            catch
            {

            }
            finally {
                parserResult.MapResult(
                    (ListWindowsCommandOptions command) => command.Execute(),
                    (ShowWindowsCommandOptions command) => command.Execute(),
                    errs =>
                    {
                        DisplayHelp(parserResult, errs);
                        return 1;
                    }
                 );
            }




        }

        private static void DisplayHelp(ParserResult<object> parserresult, IEnumerable<Error> errs)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var helptext = HelpText.AutoBuild(
                parserresult,
                onError: h =>
                {
                    h.AutoHelp = true;
                    h.AutoVersion = false;
                    h.AddDashesToOption = true;
                    h.AddEnumValuesToHelpText = true;
                    h.Copyright = "";
                    h.AddNewLineBetweenHelpSections = false;
                    h.AdditionalNewLineAfterOption = false;
                    
                    h.Heading = $"{assembly.GetName().Name} {assembly.GetName().Version}";
                    switch(true)
                    {
                        case bool _ when errs.IsHelp() : { h.AddPreOptionsLine("Help:"); break; }
                        case bool _ when errs.IsVersion() : h.AddPreOptionsLine("Help:"); { break; }
                    }
                    HelpText.DefaultParsingErrorsHandler(parserresult, h);

                    return h;
                },
                onExample: h => h,
                verbsIndex: true
            );

            Console.WriteLine(helptext);
        }
    }
}
