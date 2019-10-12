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
            IoC.Reference.Register<BabatStudioForm, IBabatStudioForm>()
                .Register<New_ProjectForm, INew_Project>()
                .Register<NewProjectPresenter>()
                .Register<BabatStudioPresenter>().Build();

            BabatStudioPresenter babatStudioPresenter = new BabatStudioPresenter(new BabatStudioForm());

            Application.Run(babatStudioPresenter._babatStudioForm as BabatStudioForm);
        }
    }
}
