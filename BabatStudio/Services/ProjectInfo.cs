using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabatStudio.Services
{
    public class ProjectInfo : EventArgs
    {
        public string ProjectPath { get; set; }
        public bool CreateSubdirCheck { get; set; }
        public string ProjectName { get; set; }
    }
}
