using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BabatStudio.Interfaces;
using System.Threading.Tasks;
using BabatStudio.Views;
using System.Windows.Forms;
using System.IO;
using BabatStudio.Services;

namespace BabatStudio.Presenters
{
    class NewProjectPresenter 
    {
        
        public INewProjectView _newProjectForm { get; set; }

        public NewProjectPresenter(INewProjectView new_Project)
        {
            _newProjectForm = new_Project;
            _newProjectForm.ChooseFolderEvent += ChooseFolderPR;
           
        }

        public void ChooseFolderPR(object txtFolder, EventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {

                DialogResult result = fbd.ShowDialog();
               
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _newProjectForm.BtnOKEnabled();
                    _newProjectForm.ProjectPath = fbd.SelectedPath;
                    (txtFolder as TextBox).Text = _newProjectForm.ProjectPath; 
                }
            }
        }

    }
}
