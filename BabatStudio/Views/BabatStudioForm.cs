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
using FastColoredTextBoxNS;
using System.CodeDom.Compiler;

namespace BabatStudio
{
    public partial class BabatStudioForm : Form, IBabatStudioView
    {
        private ProjectCLS Project;
        ImageList imageList;

        public bool ErrorCollapseCheck { get; set; }
        public bool projectCollapseCheck { get; set; }

        public CompilerResults result { get; set; }

        public event EventHandler NewProjectEvent;
        public event EventHandler OpenProjectEvent;
        public event EventHandler NewFileEvent;
        public event EventHandler OpenFileEvent;
        public event EventHandler<ProjectCLS> SaveEvent;
        public event EventHandler<ProjectCLS> SaveAllEvent;
        public event EventHandler CutEvent;
        public event EventHandler CopyEvent;
        public event EventHandler PasteEvent;
        public event EventHandler BuildEvent;
        public event EventHandler RunEvent;
        public event EventHandler CommentEvent;
        public event EventHandler ExitEvent;
        public event EventHandler CloseProjectEvent;
        public event Action ProjectCollapseEvent;
        public event Action TreeCollapseEvent;
        public event EventHandler<TreeNodeMouseClickEventArgs> TreeViewDoubleClickEvent;

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

        #region ToolStrip Buttons

        public void NewProjectTL_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Clear();
            NewProjectEvent.Invoke(sender, e);
            LoadTreeView();
        }
        public void OpenProjectTL_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Clear();
            OpenProjectEvent.Invoke(sender, e);
            LoadTreeView();

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
            CheckUppdateTextOne();
            SaveEvent.Invoke(sender, Project);
        }
        public void SaveSaveAll_TL_Click(object sender, EventArgs e)
        {
            CheckUppdateTextAll();
            SaveAllEvent.Invoke(sender, Project);
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
            OpenProjectEvent.Invoke(sender, e);
        }

        private void NewFileMN_Click(object sender, EventArgs e)
        {
            NewFileEvent.Invoke(sender, e);
        }
        private void OpenFileMN_Click(object sender, EventArgs e)
        {
            OpenFileEvent.Invoke(sender, e);
        }
        private void SaveMN_Click(object sender, EventArgs e)
        {
            CheckUppdateTextOne();

            SaveEvent.Invoke(sender, Project);
        }
        private void SaveAll_Click(object sender, EventArgs e)
        {
            CheckUppdateTextAll();

            SaveAllEvent.Invoke(sender, Project);
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
            ProjectCollapseEvent.Invoke();
        }
        private void ErrorsTreeCollapseMN_Click(object sender, EventArgs e)
        {
            TreeCollapseEvent.Invoke();
        }



        public void ProjectCollapseDo()
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


        public void TreeCollapseDo()
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
            treeView1.Nodes.Clear();
            CloseProjectEvent.Invoke(sender, e);
        }

        #endregion

        bool IView.ShowDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }
        public void TreeViewDoubleClickDo(object sender, TreeNodeMouseClickEventArgs e)
        {
            bool check = false;
            foreach (var item in Project.ProjectFiles)
            {
                if(e.Node.Text == item.FileName)
                {

                    foreach (var itemTab in tabControl2.TabPages)
                    {
                        if((itemTab as TabPage).Text == item.FileName)
                        {
                            tabControl2.SelectedTab = (itemTab as TabPage);
                            check = true;
                        } 
                    }


                    if (!check)
                        CreateTab(item);
                }
            }
        }
        private void FastColoredText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Project.ProjectName != null)
            {
                CheckUppdateTextAll();
                //SaveAllEvent.Invoke(sender, Project);
            }
        }
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewDoubleClickEvent.Invoke(sender, e);
        }
        public void CreateTab(Files file)
        {
            TabPage tabPage = new TabPage();
            tabPage.ContextMenuStrip = contextMenuStrip1;
            FastColoredTextBox fastColoredText = new FastColoredTextBox();
            fastColoredText.TextChanged += FastColoredText_TextChanged;
            fastColoredText.Dock = DockStyle.Fill;
            fastColoredText.Language = Language.CSharp;
            fastColoredText.Text = file.Data;
            tabPage.Text = file.FileName;
            tabPage.BorderStyle = BorderStyle.FixedSingle;
            tabPage.Controls.Add(fastColoredText);
            tabControl2.TabPages.Add(tabPage);
            tabControl2.SelectedTab = tabControl2.TabPages[tabControl2.TabPages.Count - 1];
            

        }
        private void LoadTreeView()
        {
            if (Project.ProjectName != null)
            {
                treeView1.Nodes.Clear();
                TreeNode rootnode = new TreeNode(Project.ProjectName);

                treeView1.Nodes.Add(rootnode);
                rootnode.ImageIndex = 1;
                rootnode.SelectedImageIndex = 1;

                foreach (var item in Project.ProjectFiles)
                {
                    rootnode.Nodes.Add(item.FileName);
                    rootnode.ImageIndex = 0;
                    rootnode.SelectedImageIndex = 0;

                }
                treeView1.ExpandAll();
            }
        }
        public void CloseProject()
        {
            tabControl2.TabPages.Clear();
            treeView1.Nodes.Clear();
            Project.ProjectName = string.Empty;

        }
        private void CheckUppdateTextOne()
        {
            foreach (var item in Project.ProjectFiles)
            {
                if(tabControl2.SelectedTab.Text == item.FileName)
                {
                    item.Data = tabControl2.SelectedTab.Controls[0].Text;
                }
            }
        }
        private void CheckUppdateTextAll()
        {
            foreach (var item in tabControl2.TabPages)
            {
                foreach (var itemFile in Project.ProjectFiles)
                {
                    if((item as TabPage).Text == itemFile.FileName)
                    {
                        itemFile.Data = (item as TabPage).Controls[0].Text;
                    }
                }
            }
        }
        public void CutDo()
        {
            (tabControl2.SelectedTab.Controls[0] as FastColoredTextBox).Cut();
        }
        public void CopyDo()
        {
            (tabControl2.SelectedTab.Controls[0] as FastColoredTextBox).Copy();
        }
        public void PasteDo()
        {
            (tabControl2.SelectedTab.Controls[0] as FastColoredTextBox).Paste();
        }
        public void CommentDo()
        {
            (tabControl2.SelectedTab.Controls[0] as FastColoredTextBox).CommentSelected();
        }
        public void GetData(ProjectCLS _projectCLS)
        {
            Project = _projectCLS;
            LoadTreeView();
        }
        public void GetResult(CompilerResults compilerResults)
        {
            result = compilerResults;
        }
        public void LoadImages()
        {
            imageList.Images.Add("Parent", Image.FromFile(@"..\..\..\Images\Parent.png"));
            imageList.Images.Add("Child", Image.FromFile(@"..\..\..\Images\Child.png"));
            treeView1.ImageList = imageList;
            treeView1.ImageIndex = 1;
            treeView1.SelectedImageIndex = 1;

        }
      

        public void LoadResultPage()
        {
            if (result.Errors.Count == 0 && result != null)
            {
                #region Errors
                TableLayoutPanel panel = new TableLayoutPanel();
                panel.Dock = DockStyle.Fill;
                panel.AutoScroll = true;
                panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                panel.ColumnCount = 4;
                panel.RowCount = 1;
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                panel.Controls.Add(new Label() { Text = "Error Code" }, 0, 0);
                panel.Controls.Add(new Label() { Text = "Message" }, 1, 0);
                panel.Controls.Add(new Label() { Text = "Line" }, 2, 0);
                panel.Controls.Add(new Label() { Text = "File" }, 3, 0);
                panel.RowCount = panel.RowCount + 1;
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

                tabControl1.TabPages[0].Controls.Clear();
                tabControl1.TabPages[0].Controls.Add(panel);

                #endregion

                #region Warning
                TableLayoutPanel warningpanel = new TableLayoutPanel();
                warningpanel.Dock = DockStyle.Fill;
                warningpanel.AutoScroll = true;
                warningpanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                warningpanel.ColumnCount = 4;
                warningpanel.RowCount = 1;
                warningpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                warningpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                warningpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                warningpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

                warningpanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                warningpanel.Controls.Add(new Label() { Text = "Warning Code" }, 0, 0);
                warningpanel.Controls.Add(new Label() { Text = "Message" }, 1, 0);
                warningpanel.Controls.Add(new Label() { Text = "Line" }, 2, 0);
                warningpanel.Controls.Add(new Label() { Text = "File" }, 3, 0);
                warningpanel.RowCount = warningpanel.RowCount + 1;
                warningpanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

                tabControl1.TabPages[1].Controls.Clear();
                tabControl1.TabPages[1].Controls.Add(warningpanel);

                #endregion

                #region Output
                string tmp = string.Empty;
                tabControl1.SelectedTab = tabControl1.TabPages[2];
                tmp = @"1>------ Build started: Project: BabatStudio, Configuration: Debug Any CPU ------
1 > BabatStudio->D:\Visual Studio files\Babat - Studio\BabatStudio\bin\Debug\BabatStudio.exe
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========
";
                tabControl1.TabPages[2].Controls[0].Text = tmp;
                #endregion
            }
            else
            {
                #region Warning
                TableLayoutPanel warningpanel = new TableLayoutPanel();
                warningpanel.Dock = DockStyle.Fill;
                warningpanel.AutoScroll = true;
                warningpanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                warningpanel.ColumnCount = 4;
                warningpanel.RowCount = 1;
                warningpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                warningpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                warningpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                warningpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

                warningpanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                warningpanel.Controls.Add(new Label() { Text = "Warning Code" }, 0, 0);
                warningpanel.Controls.Add(new Label() { Text = "Message" }, 1, 0);
                warningpanel.Controls.Add(new Label() { Text = "Line" }, 2, 0);
                warningpanel.Controls.Add(new Label() { Text = "File" }, 3, 0);
                warningpanel.RowCount = warningpanel.RowCount + 1;
                warningpanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

                tabControl1.TabPages[1].Controls.Clear();
                tabControl1.TabPages[1].Controls.Add(warningpanel);

                #endregion

                TableLayoutPanel panel = new TableLayoutPanel();
                panel.Dock = DockStyle.Fill;
                panel.AutoScroll = true;
                panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                panel.ColumnCount = 4;
                panel.RowCount = 1;
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                panel.Controls.Add(new Label() { Text = "Error Code" }, 0, 0);
                panel.Controls.Add(new Label() { Text = "Message" }, 1, 0);
                panel.Controls.Add(new Label() { Text = "Line" }, 2, 0);
                panel.Controls.Add(new Label() { Text = "File" }, 3, 0);


                foreach (CompilerError err in result.Errors)
                {
                    panel.RowCount = panel.RowCount + 1;
                    panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
                    panel.Controls.Add(new Label() { Text = err.ErrorNumber }, 0, panel.RowCount - 1);
                    panel.Controls.Add(new Label() { Text = err.ErrorText }, 1, panel.RowCount - 1);
                    panel.Controls.Add(new Label() { Text = err.Line.ToString() }, 2, panel.RowCount - 1) ;
                    panel.Controls.Add(new Label() { Text = err.FileName }, 3, panel.RowCount - 1);
                }
                panel.RowCount = panel.RowCount + 1;
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

                tabControl1.TabPages[0].Controls.Clear();
                tabControl1.TabPages[0].Controls.Add(panel);
                tabControl1.SelectedTab = tabControl1.TabPages[0];


            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutEvent.Invoke(sender, e);
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyEvent.Invoke(sender, e);
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteEvent.Invoke(sender, e);
        }

        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUppdateTextOne();

            SaveEvent.Invoke(sender, Project);
        }

        

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Remove(tabControl2.SelectedTab);
        }

        private void ClosePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Remove(tabControl2.SelectedTab);
        }
    }
}
