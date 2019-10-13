using System;

namespace BabatStudio.Views
{
    public interface INew_Project
    {
        event EventHandler CancelEvent;
        event EventHandler ChooseFolderEvent;
        event EventHandler CreateSubdirEvent;
        event EventHandler OkEvent;


        void ShowDialogOfView();
        void BtnCancel_Click(object sender, EventArgs e);
        void BtnChoosefolder_Click(object sender, EventArgs e);
        void BtnOK_Click(object sender, EventArgs e);
        void ChkCreateSubdir_CheckedChanged(object sender, EventArgs e);
    }
}