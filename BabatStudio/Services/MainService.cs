using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BabatStudio.Services
{
    public class MainService : IMainService
    {
        private ProjectCLS Projectcls;
        public ProjectCLS MainProject { get => Projectcls; set => Projectcls = value; }
        public MainService()
        {
            Projectcls = new ProjectCLS();
        }
        public void WriteNewProject(bool subdirCheck)
        {
            MainProject.ProjectFiles.Clear();
            if (subdirCheck)
            {
                Directory.CreateDirectory($@"{MainProject.Path}\\{MainProject.ProjectName}");
                MainProject.Path += $@"\{MainProject.ProjectName}";
            }

            AddFile(new Files() {FileName = "Program.cs", FilePath = MainProject.Path});

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectCLS));
            using (TextWriter textWriter = new StreamWriter($@"{MainProject.Path}\\{MainProject.ProjectName}.bsln"))
                xmlSerializer.Serialize(textWriter, Projectcls);

            

        }

        public void WriteFiles()
        {
            if (MainProject.Path != null)
            {
                foreach (var item in MainProject.ProjectFiles)
                {
                    using (TextWriter textWriter = new StreamWriter($@"{item.FilePath}\\{item.FileName}"))
                        textWriter.WriteLine(item.Data);

                }
            }
        }

        public void AddFile(Files file)
        {
            MainProject.ProjectFiles.Add(file);
            WriteFiles();
        }
        public void AddFile()
        {
            Files file = new Files();

            using (var svd = new SaveFileDialog())
            {
                svd.Filter = "CSharp file|*.cs";

                DialogResult result = svd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    file.FilePath = Path.GetDirectoryName(svd.FileName);
                    file.FileName = Path.GetFileNameWithoutExtension(svd.FileName);
                    file.FileName += ".cs";

                    if (file.FilePath == Projectcls.Path)
                    {
                        AddFile(file);
                        UpdateBsln();
                    }
                }
            }
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


        private void UpdateBsln()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectCLS));
            using (TextWriter textWriter = new StreamWriter($@"{MainProject.Path}\\{MainProject.ProjectName}.bsln"))
                xmlSerializer.Serialize(textWriter, Projectcls);
        }


        public void LoadProject()
        {

            using (var fbd = new OpenFileDialog())
            {
                DialogResult result = fbd.ShowDialog();
                //fbd.Filter = "BSLN file|*.bsln";
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName) && fbd.FileName.Contains(".bsln"))
                {
                    MainProject.Path = Path.GetDirectoryName(fbd.FileName);
                    MainProject.ProjectName = Path.GetFileNameWithoutExtension(fbd.FileName);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectCLS));
                    using (TextReader textReader = new StreamReader($@"{MainProject.Path}\\{MainProject.ProjectName}.bsln"))
                        Projectcls = (xmlSerializer.Deserialize(textReader) as ProjectCLS);


                }
            }



        }

        public void SaveFile()
        {
            UpdateBsln();
        }

        public void SaveAllFile()
        {
            WriteFiles();
        }
    }
}
