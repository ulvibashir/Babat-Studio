using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BabatStudio.Interfaces;
using System.Threading.Tasks;
using BabatStudio.Views;
using System.Windows.Forms;
using System.IO;

namespace BabatStudio.Presenters
{
    class NewProjectPresenter 
    {
        public INew_Project _new_Project { get; set; }

        public string ProjectPath { get; set; }
        public bool CreateSubdirCheck { get; set; }
        public string ProjectName { get; set; }

        public NewProjectPresenter(INew_Project new_Project)
        {
            _new_Project = new_Project;
            _new_Project.CancelEvent += CancelPR;
            _new_Project.OkEvent += OkPR;
            _new_Project.CreateSubdirEvent += CreateSubdirPR;
            _new_Project.ChooseFolderEvent += ChooseFolderPR;
            _new_Project.TextChangedEvent += TextChangedPR;
        }

        public void ChooseFolderPR(object txtFolder, EventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ProjectPath = fbd.SelectedPath;
                    (txtFolder as TextBox).Text = ProjectPath;
                }
            }
        }

        public void CreateSubdirPR(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
                CreateSubdirCheck = true;
            else
                CreateSubdirCheck = false;
        }

        public void CancelPR(object sender, EventArgs e)
        {
           
        }

        public void OkPR(object sender, EventArgs e)
        {
            
        }

        public void TextChangedPR(object sender, EventArgs e)
        {
            ProjectName = (sender as TextBox).Text;
        }
    }
}
