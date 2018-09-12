using OpenGTSMobil.ViewModels;
using OpenGTSMobil.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OpenGTSMobil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginView()) {
                BarBackgroundColor = (!string.IsNullOrEmpty(Login.colorNavBar)) ? Color.FromHex(Login.colorNavBar) : Color.Default,
                BarTextColor = (!string.IsNullOrEmpty(Login.colorTextNavBar))? Color.FromHex(Login.colorTextNavBar) : Color.Default };
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
