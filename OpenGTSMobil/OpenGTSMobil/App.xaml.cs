using OpenGTSMobil.View;
using OpenGTSMobil.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OpenGTSMobil
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Global.isConected = DoIHaveInternet();
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                Global.isConected = args.IsConnected;
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Global.isConected = DoIHaveInternet();
            if (LoginModel.autoLogin)
            {
                MainPage = new NavigationPage(new ShowMapView())
                {
                    BarBackgroundColor = (!string.IsNullOrEmpty(ShowMap.colorNavBar)) ? Color.FromHex(ShowMap.colorNavBar) : Color.Default,
                    BarTextColor = (!string.IsNullOrEmpty(ShowMap.colorTextNavBar)) ? Color.FromHex(ShowMap.colorTextNavBar) : Color.Default
                };
            }
            else
            {
                MainPage = new NavigationPage(new LoginView())
                {
                    BarBackgroundColor = (!string.IsNullOrEmpty(Login.colorNavBar)) ? Color.FromHex(Login.colorNavBar) : Color.Default,
                    BarTextColor = (!string.IsNullOrEmpty(Login.colorTextNavBar)) ? Color.FromHex(Login.colorTextNavBar) : Color.Default
                };
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Global.isConected = DoIHaveInternet();
        }

        public bool DoIHaveInternet()
        {
            if (!CrossConnectivity.IsSupported)
                return true;

            return CrossConnectivity.Current.IsConnected;
        }
    }
}
