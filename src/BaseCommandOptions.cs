using CommandLine;

namespace ShowVM
{
    internal abstract class BaseCommandOptions : ICommandOptions
    {
        internal protected readonly WindowManager _windowManager;

        internal BaseCommandOptions()
        {
            _windowManager = new WindowManager();
        }

        public bool Verbose { get; set; }

        public abstract int Execute();
    }
}
