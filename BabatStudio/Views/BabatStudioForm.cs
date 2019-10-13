using BabatStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;



namespace BabatStudio
{
    public partial class BabatStudioForm : Form, IBabatStudioForm
    {
        public event EventHandler NewProjectEvent;
        public event EventHandler OpenProjectEvent;
               
        public event EventHandler NewFileEvent;
        public event EventHandler OpenFileEvent;
        public event EventHandler SaveEvent;
        public event EventHandler SaveAllEvent;
               
        public event EventHandler CutEvent;
        public event EventHandler CopyEvent;
        public event EventHandler PasteEvent;
               
        public event EventHandler BuildEvent;
        public event EventHandler RunEvent;
        public event EventHandler CommentEvent;



        public BabatStudioForm()
        {
            InitializeComponent();
        }
        public void BabatStudio_Load(object sender, EventArgs e)
        {
            
        }

        public void NewProjectTL_Click(object sender, EventArgs e)
        {
            NewProjectEvent.Invoke(sender, e);
        }
        public void OpenProjectTL_Click(object sender, EventArgs e)
        {
            OpenProjectEvent.Invoke(sender, e);
        }

        public void NewFileTL_Click(object sender, EventArgs e)
        {
            NewFileEvent.Invoke(sender, e);
        }
        public void OpenFileTL_Click(object sender, EventArgs e)
        {
            OpenFileEvent.Invoke(sender, e);
        }
        public void SaveTL_Click(object sender, EventArgs e)
        {
            SaveEvent.Invoke(sender, e);
        }
        public void SaveSaveAll_TL_Click(object sender, EventArgs e)
        {
            SaveAllEvent.Invoke(sender, e);
        }

        public void CutTL_Click(object sender, EventArgs e)
        {
            CutEvent.Invoke(sender, e);
        }
        public void CopyTL_Click(object sender, EventArgs e)
        {
            CopyEvent.Invoke(sender, e);
        }
        public void PasteTL_Click(object sender, EventArgs e)
        {
            PasteEvent.Invoke(sender, e);
        }

        public void BuildTL_Click(object sender, EventArgs e)
        {
            BuildEvent.Invoke(sender, e);
        }
        public void RunTL_Click(object sender, EventArgs e)
        {
            RunEvent.Invoke(sender, e);
        }
        public void CommentTL_Click(object sender, EventArgs e)
        {
            CommentEvent.Invoke(sender, e);
        }

       
    }
}
