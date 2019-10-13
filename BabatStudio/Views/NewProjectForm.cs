using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BabatStudio.Interfaces;
using BabatStudio.Services;

namespace BabatStudio.Views
{
    public partial class New_ProjectForm : Form, INew_Project
    {
        public New_ProjectForm()
        {
            InitializeComponent();
        }
        
        private void New_Project_Load(object sender, EventArgs e)
        {
            btnCancel.DialogResult = DialogResult.Cancel;
            btnOK.DialogResult = DialogResult.OK;
        }

        
        public event EventHandler CreateSubdirEvent;
        public event EventHandler OkEvent;
        public event EventHandler CancelEvent;
        public event EventHandler ChooseFolderEvent;
        public event EventHandler TextChangedEvent;

        public void BtnChoosefolder_Click(object sender, EventArgs e)
        {
            ChooseFolderEvent.Invoke(txtFolder, e);
        }

        public void ChkCreateSubdir_CheckedChanged(object sender, EventArgs e)
        {
            CreateSubdirEvent.Invoke(sender, e);
        }

        public void BtnCancel_Click(object sender, EventArgs e)
        {
            CancelEvent.Invoke(sender, e);
        }

        public void BtnOK_Click(object sender, EventArgs e)
        {
            OkEvent.Invoke(sender,e);
        }

      

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            TextChangedEvent.Invoke(sender, e);
        }

        bool IView.ShowDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public ProjectInfo GetProjectInfo()
        {
            var projectInfo = IoC.Reference.Resolve<ProjectInfo>();
            projectInfo.CreateSubdirCheck = chkCreateSubdir.Checked;
            projectInfo.ProjectName = txtName.Text;
            projectInfo.ProjectPath = txtFolder.Text;
            return projectInfo;
        }
    }
}
