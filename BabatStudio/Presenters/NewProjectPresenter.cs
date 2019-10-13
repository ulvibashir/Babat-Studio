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

        
        public NewProjectPresenter(INew_Project new_Project)
        {
            _new_Project = new_Project;

            _new_Project.CancelEvent += CancelPR;
            _new_Project.OkEvent += OkPR;
            _new_Project.CreateSubdirEvent += CreateSubdirPR;
            _new_Project.ChooseFolderEvent += ChooseFolderPR;
        }

        public void ChooseFolderPR(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                   MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                }
            }
        }

        public void CreateSubdirPR(object sender, EventArgs e)
        {

        }

        public void CancelPR(object sender, EventArgs e)
        {
           
        }

        public void OkPR(object sender, EventArgs e)
        {
           
        }
        
    }
}
