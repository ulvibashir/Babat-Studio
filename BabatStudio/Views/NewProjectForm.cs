﻿using System;
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
    public partial class New_ProjectForm : Form, INewProjectView
    {
        public string ProjectName {get => txtName.Text; }
        public string ProjectPath { get => txtFolder.Text; set => txtFolder.Text = value; }
        public bool HasSubdir { get => chkCreateSubdir.Checked; }
        

        public New_ProjectForm()
        {
            InitializeComponent();
        }
      
        private void New_Project_Load(object sender, EventArgs e)
        {
            btnCancel.DialogResult = DialogResult.Cancel;
            btnOK.DialogResult = DialogResult.OK;
        }

        public event EventHandler ChooseFolderEvent;

        public void BtnChoosefolder_Click(object sender, EventArgs e)
        {
            ChooseFolderEvent.Invoke(txtFolder, e);
        }

        
        bool IView.ShowDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }
        
    }
}
