using System.Diagnostics;
using Module02Exercise01.View;
using Microsoft.Maui.Networking;
using Module02Exercise01.Services;

namespace Module02Exercise01
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Set the MainPage to EmployeePage directly
            MainPage = new NavigationPage(new EmployeePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}