using BabatStudio.Interfaces;
using BabatStudio.Services;
using System;

namespace BabatStudio.Views
{
    public interface INewProjectView : IView
    {
        event EventHandler ChooseFolderEvent;
       

        string ProjectName { get;}
        string ProjectPath { get; set; }
        bool IsSubdir { get;}

        void BtnOKEnabled();
        
        void BtnChoosefolder_Click(object sender, EventArgs e);
    }
}