namespace BabatStudio.Services
{
    public interface IMainService
    {
        ProjectCLS MainProject { get; set; }

        void AddFile(Files file);
        bool IsExist(string name);
        void WriteFiles();
        void WriteNewProject(bool subdirCheck);
        void AddFile();
        void LoadProject();
    }
}