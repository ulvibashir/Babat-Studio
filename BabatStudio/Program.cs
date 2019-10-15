using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BabatStudio.Interfaces;
using BabatStudio.Presenters;
using BabatStudio.Services;
using BabatStudio.Views;


namespace BabatStudio
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            IoC.Reference.Register<BabatStudioForm, IBabatStudioView>()
                .Register<New_ProjectForm, INewProjectView>()
                .Register<MainService, IMainService>()
                .Register<NewProjectPresenter>()
                .Register<BabatStudioPresenter>()
                .Register<ProjectCLS>()
                .Build();



            BabatStudioPresenter babatStudioPresenter = IoC.Reference.Resolve<BabatStudioPresenter>();

            Application.Run(babatStudioPresenter.babatStudioForm as BabatStudioForm);
        }
    }
}
