using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskForQuipu.View;
using TaskForQuipu.ViewModel;

namespace TaskForQuipu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainPage window = new MainPage();
            ClientViewModel VM = new ClientViewModel();
            window.DataContext = VM;
            window.Show();
        }
    }
}
