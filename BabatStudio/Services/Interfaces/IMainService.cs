using System.CodeDom.Compiler;

namespace BabatStudio.Services
{
    public interface IMainService
    {
        ProjectCLS MainProject { get; set; }

        void AddFile(Files file);
        
        void WriteFiles();
        void WriteNewProject(bool subdirCheck);
        void AddFile();
        bool LoadProject();
        void SaveFile();
        void Build();
        void Run();

        CompilerResults compilerResults(string[] sources, string output, params string[] references);
      
    }
}