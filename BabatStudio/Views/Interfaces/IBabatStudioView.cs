using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BabatStudio.Interfaces;


namespace BabatStudio.Interfaces
{
    public interface IBabatStudioView : IView
    {
        event EventHandler NewProjectEvent;
        event EventHandler OpenProjectEvent;

        event EventHandler NewFileEvent;
        event EventHandler OpenFileEvent;
        event EventHandler<ProjectCLS> SaveEvent;
        event EventHandler<ProjectCLS> SaveAllEvent;

        event EventHandler CutEvent;
        event EventHandler CopyEvent;
        event EventHandler PasteEvent;

        event EventHandler BuildEvent;
        event EventHandler RunEvent;
        event EventHandler CommentEvent;
        event EventHandler ExitEvent;
        event EventHandler CloseProjectEvent;
        event EventHandler<TreeNodeMouseClickEventArgs> TreeViewDoubleClickEvent;

        event Action ProjectCollapseEvent;
        event Action TreeCollapseEvent;











        void NewProjectTL_Click(object sender, EventArgs e);
        void OpenProjectTL_Click(object sender, EventArgs e);

        void NewFileTL_Click(object sender, EventArgs e);
        void OpenFileTL_Click(object sender, EventArgs e);
        void SaveTL_Click(object sender, EventArgs e);
        void SaveSaveAll_TL_Click(object sender, EventArgs e);

        void CutTL_Click(object sender, EventArgs e);
        void CopyTL_Click(object sender, EventArgs e);
        void PasteTL_Click(object sender, EventArgs e);

        void BuildTL_Click(object sender, EventArgs e);
        void RunTL_Click(object sender, EventArgs e);
        void CommentTL_Click(object sender, EventArgs e);
      
        void GetData(ProjectCLS _projectCLS);
        void TreeCollapseDo();
        void ProjectCollapseDo();

        void TreeViewDoubleClickDo(object sender, TreeNodeMouseClickEventArgs e);
        void CreateTab(Files file);
        void CloseProject();
        



    }
}
