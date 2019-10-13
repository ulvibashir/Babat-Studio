using BabatStudio.Interfaces;
using BabatStudio.Services;
using System;

namespace BabatStudio.Views
{
    public interface INew_Project : IView
    {
        event EventHandler CancelEvent;
        event EventHandler ChooseFolderEvent;
        event EventHandler CreateSubdirEvent;
        event EventHandler OkEvent;
        event EventHandler TextChangedEvent;


      
        void BtnCancel_Click(object sender, EventArgs e);
        void BtnChoosefolder_Click(object sender, EventArgs e);
        void BtnOK_Click(object sender, EventArgs e);
        void ChkCreateSubdir_CheckedChanged(object sender, EventArgs e);
        ProjectInfo GetProjectInfo();
    }
}