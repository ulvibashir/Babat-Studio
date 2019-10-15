using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BabatStudio
{
    public class ProjectCLS
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
        public string FilePath { get; set; }
        public string Data { get; set; }
    }
}
