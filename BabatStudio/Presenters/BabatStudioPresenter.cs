using BabatStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabatStudio.Services;
using System.Windows.Forms;

namespace BabatStudio.Presenters
{
    class BabatStudioPresenter
    {
        public IBabatStudioView babatStudioForm { get; set; }
        public IMainService mainService { get; set; }



        public BabatStudioPresenter(IBabatStudioView _babatStudioForm, IMainService _mainService)
        {
            babatStudioForm = _babatStudioForm;
            mainService = _mainService;
            Subscribe();
        }

        private void Subscribe()
        {
            babatStudioForm.NewProjectEvent += _babatStudioForm_NewProjectEvent;
            babatStudioForm.OpenProjectEvent += _babatStudioForm_OpenProjectEvent;
            babatStudioForm.NewFileEvent += _babatStudioForm_NewFileEvent; 
            babatStudioForm.OpenFileEvent += _babatStudioForm_OpenFileEvent; 
            babatStudioForm.SaveEvent += _babatStudioForm_SaveEvent; 
            babatStudioForm.SaveAllEvent += _babatStudioForm_SaveAllEvent; 
            babatStudioForm.CutEvent += _babatStudioForm_CutEvent; 
            babatStudioForm.CopyEvent += _babatStudioForm_CopyEvent; 
            babatStudioForm.PasteEvent += _babatStudioForm_PasteEvent; 
            babatStudioForm.BuildEvent += _babatStudioForm_BuildEvent; 
            babatStudioForm.RunEvent += _babatStudioForm_RunEvent;
            babatStudioForm.CommentEvent += _babatStudioForm_CommentEvent;

            babatStudioForm.ExitEvent += _babatStudioForm_ExitEvent;
            babatStudioForm.CloseProjectEvent += _babatStudioForm_CloseProjectEvent;

            babatStudioForm.TreeCollapseEvent += BabatStudioForm_TreeCollapse;
            babatStudioForm.ProjectCollapseEvent += BabatStudioForm_ProjectCollapse;
            babatStudioForm.TreeViewDoubleClickEvent += BabatStudioForm_TreeViewDoubleClickEvent;
                                
        }
        private void Send()
        {
            babatStudioForm.GetData(mainService.MainProject);
        }
        private void SendResult()
        {
            babatStudioForm.GetResult(mainService.result);
        }

        private void BabatStudioForm_TreeViewDoubleClickEvent(object sender, TreeNodeMouseClickEventArgs e)
        {
            babatStudioForm.TreeViewDoubleClickDo(sender,e);
        }
        private void _babatStudioForm_NewProjectEvent(object sender, EventArgs e)
        {
            var newProjectPresenter = IoC.Reference.Resolve<NewProjectPresenter>();

            if (newProjectPresenter._newProjectForm.ShowDialog())
            {
                mainService.MainProject.ProjectName = newProjectPresenter._newProjectForm.ProjectName;
                mainService.MainProject.Path = newProjectPresenter._newProjectForm.ProjectPath;
                mainService.WriteNewProject(newProjectPresenter._newProjectForm.IsSubdir);
                Send();
                babatStudioForm.CreateTab(mainService.MainProject.ProjectFiles[0]);
            }
            
        }
        private void _babatStudioForm_OpenProjectEvent(object sender, EventArgs e)
        {
            if (mainService.LoadProject())
            {
                babatStudioForm.CreateTab(mainService.MainProject.ProjectFiles[0]);
                Send();
            }
        }
        private void _babatStudioForm_NewFileEvent(object sender, EventArgs e)
        {
            int tmp = mainService.MainProject.ProjectFiles.Count;
            mainService.AddFile();
            if(tmp != mainService.MainProject.ProjectFiles.Count)
            babatStudioForm.CreateTab(mainService.MainProject.ProjectFiles[mainService.MainProject.ProjectFiles.Count - 1]);
           
        }
        private void _babatStudioForm_OpenFileEvent(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }
        private void _babatStudioForm_SaveEvent(object sender, ProjectCLS e)
        {
            if (mainService.MainProject.ProjectName != null)
            {
                mainService.MainProject = e;
                mainService.SaveFile();
            }
        }
        private void _babatStudioForm_SaveAllEvent(object sender, ProjectCLS e)
        {
            if (mainService.MainProject.ProjectName != null)
            {
                mainService.MainProject = e;
                mainService.SaveFile();
            }
        }
        private void _babatStudioForm_CutEvent(object sender, EventArgs e)
        {
            babatStudioForm.CutDo();
        }
        private void _babatStudioForm_CopyEvent(object sender, EventArgs e)
        {
            babatStudioForm.CopyDo();
        }
        private void _babatStudioForm_PasteEvent(object sender, EventArgs e)
        {
            babatStudioForm.PasteDo();
        }
        private void _babatStudioForm_BuildEvent(object sender, EventArgs e)
        {
            mainService.Build();
        }
        private void _babatStudioForm_RunEvent(object sender, EventArgs e)
        {
            mainService.Run();
            SendResult();
            if (mainService.result != null)
                babatStudioForm.LoadResultPage();
        }
        private void _babatStudioForm_CommentEvent(object sender, EventArgs e)
        {
            babatStudioForm.CommentDo();
        }
        private void _babatStudioForm_CloseProjectEvent(object sender, EventArgs e)
        {
            mainService.MainProject.ProjectName = string.Empty;
            babatStudioForm.CloseProject();
        }
        private void BabatStudioForm_ProjectCollapse()
        {
            babatStudioForm.ProjectCollapseDo();
        }
        private void BabatStudioForm_TreeCollapse()
        {
            babatStudioForm.TreeCollapseDo();
        }
        private void _babatStudioForm_ExitEvent(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        


    }
}
