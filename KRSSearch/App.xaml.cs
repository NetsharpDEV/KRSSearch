using KRSSearch.DataAccessLayer;
using KRSSearch.Logic;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KRSSearch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;
       

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();

            Current.MainWindow = this.container.Get<LoadingPanel>();
            Current.MainWindow.Show();
            AssociationRepository repo = new AssociationRepository(container.Get<DataService>());
            repo.UpdateDatabaseFromAPI();
            Current.MainWindow.Hide();
            ComponeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            this.container = new StandardKernel();
            container.Bind<IAssociationRepository>().To<AssociationRepository>();
            container.Bind<DataService>().ToSelf().InSingletonScope();
        }
        private void ComponeObjects()
        {
            Current.MainWindow = this.container.Get<MainWindow>();
            Current.MainWindow.Title = "KRS - search tool";
        }

    }
}
