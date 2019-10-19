using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BabatStudio
{
    public class ProjectCLS : EventArgs
    {
        public string ProjectName { get; set; }
        public string Path { get; set; }
        public List<Files> ProjectFiles { get; set; }

        public ProjectCLS()
        {
            ProjectFiles = new List<Files>();
        }
    }

    public class Files
    {
        public string FileName { get; set; }

        [XmlIgnore]
        public string Data { get; set; }
    }
}
