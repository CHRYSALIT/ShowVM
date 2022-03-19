using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowVM
{
    /// <summary>
    /// Represent a Window
    /// </summary>
    internal class Window
    {
        internal IntPtr WindowHandle { get; set; }
        internal string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
