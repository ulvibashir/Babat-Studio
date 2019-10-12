using BabatStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabatStudio.Services;

namespace BabatStudio.Presenters
{
    class BabatStudioPresenter
    {
        public IBabatStudioForm _babatStudioForm { get; set; }
        public ProjectCLS project = new ProjectCLS();
        public BabatStudioPresenter(IBabatStudioForm babatStudioForm)
        {
            _babatStudioForm = babatStudioForm;

            _babatStudioForm.NewProjectEvent += NewProjectPR;
            _babatStudioForm.OpenProjectEvent += OpenProjectPR;
            _babatStudioForm.NewFileEvent += NewFilePR;
            _babatStudioForm.OpenFileEvent += OpenFilePR;
            _babatStudioForm.SaveEvent += SavePR;
            _babatStudioForm.SaveAllEvent += SaveAllPR;
            _babatStudioForm.CutEvent += CutPR;
            _babatStudioForm.CopyEvent += CopyPR;
            _babatStudioForm.PasteEvent += PastePR;
            _babatStudioForm.BuildEvent += BuildPR;
            _babatStudioForm.RunEvent += RunPR;
            _babatStudioForm.CommentEvent += CommentPR;

        }

        public void NewProjectPR(object sender, EventArgs e)
        {
            NewProjectPresenter newProjectPresenter = IoC.Reference.Resolve<NewProjectPresenter>();
           

        }
        public void OpenProjectPR(object sender, EventArgs e)
        {

        }
        public void NewFilePR(object sender, EventArgs e)
        {

        }
        public void OpenFilePR(object sender, EventArgs e)
        {

        }
        public void SavePR(object sender, EventArgs e)
        {

        }
        public void SaveAllPR(object sender, EventArgs e)
        {

        }
        public void CutPR(object sender, EventArgs e)
        {

        }
        public void CopyPR(object sender, EventArgs e)
        {

        }
        public void PastePR(object sender, EventArgs e)
        {

        }
        public void BuildPR(object sender, EventArgs e)
        {

        }
        public void RunPR(object sender, EventArgs e)
        {

        }
        public void CommentPR(object sender, EventArgs e)
        {

        }
    }
}
