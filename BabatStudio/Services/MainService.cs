using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabatStudio.Services
{
    public class MainService : IMainService
    {
        private ProjectCLS ProjectCLS;
        public ProjectCLS MainProject { get => ProjectCLS; set => ProjectCLS = value; }
        public MainService()
        {
            ProjectCLS = new ProjectCLS();
        }
        public void Writer(bool subdirCheck)
        {
            if (subdirCheck)
            {
                Directory.CreateDirectory($@"{MainProject.Path}/{MainProject.ProjectName}");
                MainProject.Path += $@"/{MainProject.ProjectName}";
            }

            AddFile(new Files() {FileName = "Program.cs", FilePath = MainProject.Path});

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectCLS));
            using (TextWriter textWriter = new StreamWriter($@"{MainProject.Path}/{MainProject.ProjectName}.bsln"))
                xmlSerializer.Serialize(textWriter, ProjectCLS);

            WriteFiles();

        }

        public void WriteFiles()
        {
            foreach (var item in MainProject.ProjectFiles)
            {
                using (TextWriter textWriter = new StreamWriter($@"{MainProject.Path}/{item.FileName}"))
                    textWriter.WriteLine(item.Data);

            }

        }

        public void AddFile(Files file)
        {
            MainProject.ProjectFiles.Add(file);
        }
        public bool IsExist(string name)
        {
            bool check = false;
            foreach (var item in MainProject.ProjectFiles)
            {
                if (item.FileName == name)
                {
                    check = true;
                }
            }
            return check;
        }
    }
}
