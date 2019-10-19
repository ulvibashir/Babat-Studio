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
        private string DataClass { get; set; }
        public ProjectCLS MainProject { get => Projectcls; set => Projectcls = value; }
        public CompilerResults result { get; set; }
        public MainService()
        {
            Projectcls = new ProjectCLS();
            GetCsData();
        }
        public CompilerResults compilerResults(string[] sources, string output, params string[] references)
        {
            var parameters = new CompilerParameters(references, output);
            parameters.GenerateExecutable = true;
            using (var provider = new CSharpCodeProvider())
                return provider.CompileAssemblyFromSource(parameters, sources);

        }
        private void GetCsData()
        {
            using (TextReader textReader = new StreamReader($@"..\..\..\DataFiles\DataFiles.cs"))
                DataCs = textReader.ReadToEnd();

            using (TextReader textReader = new StreamReader($@"..\..\..\DataFiles\DataFilesClass.cs"))
                DataClass = textReader.ReadToEnd();
        }





        public void WriteNewProject(bool subdirCheck)
        {
            MainProject.ProjectFiles.Clear();
            if (subdirCheck)
            {
                Directory.CreateDirectory($@"{MainProject.Path}\\{MainProject.ProjectName}");
                MainProject.Path += $@"\{MainProject.ProjectName}";
            }

            AddFile(new Files() {FileName = "Program.cs", Data = DataCs});

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
                    using (TextWriter textWriter = new StreamWriter($@"{MainProject.Path}\\{item.FileName}"))
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
                    file.FileName = Path.GetFileNameWithoutExtension(svd.FileName);
                    file.FileName += ".cs";
                    file.Data = DataClass;
                    FileInfo fileInfo = new FileInfo(svd.FileName);
                    if (fileInfo.DirectoryName == Projectcls.Path)
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
                    LoadData();
                    return true;
                }
            }
            return false;
        }
        public void LoadData()
        {
            if (MainProject.Path != null)
            {
                foreach (var item in MainProject.ProjectFiles)
                {
                    using (TextReader textReader = new StreamReader($@"{MainProject.Path}\\{item.FileName}"))
                       item.Data =  textReader.ReadToEnd();

                }
            }
        }
        public void SaveFile()
        {
            //UpdateBsln();
            WriteFiles();
        }
        public void Build()
        {
            if (MainProject.ProjectName != null && MainProject.ProjectName != string.Empty) {
                string[] sources = new string[Projectcls.ProjectFiles.Count];
            int i = 0;
            foreach (var item in Projectcls.ProjectFiles)
            {

                sources[i] = item.Data;
                i++;
            }
            result = compilerResults(sources, "compile.exe");
            }

        }
        public void Run()
        {
            if (MainProject.ProjectName != null && MainProject.ProjectName != string.Empty)
            {
                Build();
                if (result.Errors.Count != 0)
                    MessageBox.Show("Please Check Code!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    Process.Start("compile.exe");
            }
        }

    }
}
