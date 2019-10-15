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
    public partial class BabatStudioForm : Form, IBabatStudioView
    {
        private ProjectCLS Project;
        ImageList imageList;

        public bool ErrorCollapseCheck { get; set; }
        public bool projectCollapseCheck { get; set; }

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

        

        public event EventHandler ExitEvent;
        public event EventHandler CloseProjectEvent;

        public BabatStudioForm()
        {
            InitializeComponent();
            Project = new ProjectCLS();
            imageList = new ImageList();
            LoadImages();


        }
        public void BabatStudio_Load(object sender, EventArgs e)
        {
            
        }

        bool IView.ShowDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }



        #region ToolStrip Buttons

        public void NewProjectTL_Click(object sender, EventArgs e)
        {
            NewProjectEvent.Invoke(sender, e);
            LoadTreeView();
        }
        public void OpenProjectTL_Click(object sender, EventArgs e)
        {
            OpenProjectEvent.Invoke(sender, e);
        }

        public void NewFileTL_Click(object sender, EventArgs e)
        {
            NewFileEvent.Invoke(sender, e);
            LoadTreeView();


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
        #endregion

        #region MenuStrip File
        private void NewProjectMN_Click(object sender, EventArgs e)
        {
            NewProjectEvent.Invoke(sender, e);
            LoadTreeView();


        }
        private void OpenProjectMN_Click(object sender, EventArgs e)
        {
            OpenFileEvent.Invoke(sender, e);
        }

        private void NewFileMN_Click(object sender, EventArgs e)
        {
            NewFileEvent.Invoke(sender, e);
            LoadTreeView();


        }
        private void OpenFileMN_Click(object sender, EventArgs e)
        {
            OpenFileEvent.Invoke(sender, e);
        }
        private void SaveMN_Click(object sender, EventArgs e)
        {
            SaveEvent.Invoke(sender, e);
        }
        private void SaveAll_Click(object sender, EventArgs e)
        {
            SaveAllEvent.Invoke(sender, e);
        }
        private void ExitMN_Click(object sender, EventArgs e)
        {
            ExitEvent.Invoke(sender, e);
        }
        #endregion

        #region MenuStrip Edit
        private void CutMN_Click(object sender, EventArgs e)
        {
            CutEvent.Invoke(sender, e);
        }
        private void CopyMN_Click(object sender, EventArgs e)
        {
            CopyEvent.Invoke(sender, e);
        }
        private void PasteMN_Click(object sender, EventArgs e)
        {
            PasteEvent.Invoke(sender, e);
        }

        #endregion

        #region MenuStrip View
        private void ProjectTreeCollapseMN_Click(object sender, EventArgs e)
        {
            if (projectCollapseCheck)
            {
                splitContainer2.Panel1Collapsed = false;
                ProjectTreeCollapseMN.Checked = true;
                projectCollapseCheck = false;
            }
            else
            {
                splitContainer2.Panel1Collapsed = true;
                ProjectTreeCollapseMN.Checked = false;
                projectCollapseCheck = true;

            }
        }
        private void ErrorsTreeCollapseMN_Click(object sender, EventArgs e)
        {
            if (ErrorCollapseCheck)
            {
                splitContainer1.Panel2Collapsed = false;
                ErrorsTreeCollapseMN.Checked = true;
                ErrorCollapseCheck = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                ErrorsTreeCollapseMN.Checked = false;
                ErrorCollapseCheck = true;

            }
        }
        #endregion

        #region MenuStrip Project
        private void AddNewFileMN_project_Click(object sender, EventArgs e)
        {
            NewFileEvent.Invoke(sender, e);
            LoadTreeView();


        }
        private void BuildMN_Click(object sender, EventArgs e)
        {
            BuildEvent.Invoke(sender, e);
        }
        private void RunMN_Click(object sender, EventArgs e)
        {
            RunEvent.Invoke(sender, e);
        }
        private void CloseMN_Click(object sender, EventArgs e)
        {
            CloseProjectEvent.Invoke(sender, e);
        }
        #endregion


        public void LoadTreeView()
        {
            
            treeView1.BeginUpdate();

            TreeNode rootnode = new TreeNode(Project.ProjectName);
            rootnode.ImageIndex = 1;
            rootnode.SelectedImageIndex = 1;

            treeView1.Nodes.Add(rootnode);
            


            foreach (var item in Project.ProjectFiles)
            {
                treeView1.Nodes[0].Nodes.Add(item.FileName);
                rootnode.ImageIndex = 0;
                rootnode.SelectedImageIndex = 0;

            }
            treeView1.EndUpdate();
        }
        public void LoadImages()
        {
            imageList.Images.Add("Parent", Image.FromFile(@"..\..\..\Images\Parent.png"));
            imageList.Images.Add("Child", Image.FromFile(@"..\..\..\Images\Child.png"));
            treeView1.ImageList = imageList;
            treeView1.ImageIndex = 1;
            treeView1.SelectedImageIndex = 1;

        }


        public void GetData(ProjectCLS _projectCLS)
        {
            Project = _projectCLS;
        }
       
    }
}
