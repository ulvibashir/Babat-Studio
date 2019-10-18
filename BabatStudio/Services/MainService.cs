using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BabatStudio.Services
{
    public class MainService : IMainService
    {
        private ProjectCLS Projectcls;
        private string DataCs { get; set; }
        public ProjectCLS MainProject { get => Projectcls; set => Projectcls = value; }
        public MainService()
        {
            Projectcls = new ProjectCLS();
            GetCsData();
        }

        public void WriteNewProject(bool subdirCheck)
        {
            MainProject.ProjectFiles.Clear();
            if (subdirCheck)
            {
                Directory.CreateDirectory($@"{MainProject.Path}\\{MainProject.ProjectName}");
                MainProject.Path += $@"\{MainProject.ProjectName}";
            }

            AddFile(new Files() {FileName = "Program.cs", FilePath = MainProject.Path, Data = DataCs});

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
                    file.Data = DataCs;
                    
                    if (file.FilePath == Projectcls.Path)
                    {
                        AddFile(file);
                        UpdateBsln();
                    }
                }
            }
        }
        private void UpdateBsln()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectCLS));
            using (TextWriter textWriter = new StreamWriter($@"{MainProject.Path}\\{MainProject.ProjectName}.bsln"))
                xmlSerializer.Serialize(textWriter, Projectcls);
        }
        public bool LoadProject()
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

                    return true;
                }
            }
            return false;
        }
        public void SaveFile()
        {
            UpdateBsln();
            WriteFiles();
        }
        private void GetCsData()
        {
            using (TextReader textReader = new StreamReader($@"..\..\..\DataFiles\DataFiles.cs"))
                DataCs = textReader.ReadToEnd();
        }
        public CompilerResults compilerResults(string[] sources, string output, params string[] references)
        {
            var parameters = new CompilerParameters(references, output);
            parameters.GenerateExecutable = true;
            using (var provider = new CSharpCodeProvider())
                return provider.CompileAssemblyFromSource(parameters, sources);

        }

        public void Build()
        {
            string[] sources = new string[Projectcls.ProjectFiles.Count];
            int i = 0;
            foreach (var item in Projectcls.ProjectFiles)
            {

                sources[i] = item.Data;
                i++;
            }
            var result = compilerResults(sources, "compile.exe");
            
        }

        public void Run()
        {
            Build();
            Process.Start("compile.exe");
        }
        
    }
}
