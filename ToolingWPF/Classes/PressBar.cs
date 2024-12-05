using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolingWPF.Classes
{
    internal class PressBar
    {
        public int PressID { get; set; }
        public int Toolbar { get; set; }

        public PressBar(int PressID, int Toolbar)
        {
            this.PressID = PressID;
            this.Toolbar = Toolbar;
        }
    }
}