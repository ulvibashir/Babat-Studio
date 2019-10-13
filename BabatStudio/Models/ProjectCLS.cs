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
        public List<string> ProjectFiles { get; set; }

        public ProjectCLS()
        {
            ProjectFiles = new List<string>();
        }

        

        public void Writer(bool subdirCheck)
        {
            if (subdirCheck)
            {
                Directory.CreateDirectory($@"{Path}/{ProjectName}");
                Path += $@"/{ProjectName}";
            }
            
            AddFile("Program.cs");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectCLS));
            using (TextWriter textWriter = new StreamWriter($@"{Path}/{ProjectName}.bsln"))
                xmlSerializer.Serialize(textWriter, this);

            WriteFiles();

        }

        public void WriteFiles()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(String));
            foreach (var item in ProjectFiles)
            {
                using (TextWriter textWriter = new StreamWriter($@"{Path}/{item}"))
                    xmlSerializer.Serialize(textWriter, item);

            }

        }

        public void AddFile(string name)
        {
            ProjectFiles.Add(name);
        }
        public bool IsExist(string name)
        {
            bool check = false;
            foreach (var item in ProjectFiles)
            {
                if (item == name)
                {
                    check = true;
                }
            }
            return check;
        }
    }
}
