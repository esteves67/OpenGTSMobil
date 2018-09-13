using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGTSMobil.Models;
using OpenGTSMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static OpenGTSMobil.Models.ResultAPI;

namespace OpenGTSMobil.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        public string msgError { get; set; }

        public LoginView ()
		{
			InitializeComponent();
            aplicar();                              //mostrar los iconos correspondientes
        }

        void aplicar()
        {
            NavigationPage.SetHasNavigationBar(this, Login.showNavBar);
            if (!string.IsNullOrEmpty(Login.BackgroundImage))
            {
                this.BackgroundImage = Login.BackgroundImage;
            }
            if (!string.IsNullOrEmpty(Login.ButtonLogin))
            {
                ButtonLogin.Image = Login.ButtonLogin;
            }
            if (!string.IsNullOrEmpty(Login.IconAccount))
            {
                IconAccount.Source = Login.IconAccount;
                IconAccount.IsVisible = true;
            }
            if (!string.IsNullOrEmpty(Login.IconMail))
            {
                IconMail.Source = Login.IconMail;
                IconMail.IsVisible = true;
            }
            if (!string.IsNullOrEmpty(Login.IconPassword))
            {
                IconPassword.Source = Login.IconPassword;
                IconPassword.IsVisible = true;
            }
            if (!string.IsNullOrEmpty(Login.ImageLogo))
            {
                ImageLogo.Source = Login.ImageLogo;
            }
            else
            {
                ImageLogo.Source = "Default.png";
            }

            if (Login.AccountOrMail == 0)
            {
                EntryAccount.IsVisible = true;
                EntryMail.IsVisible = true;
            }
            else if (Login.AccountOrMail == 1)
            {
                EntryMail.IsVisible = true;
            }
            else
            {
                EntryAccount.IsVisible = true;
            }
            LabelCopyright.Text = Login.labelCopyright;
            LabelCopyright.IsVisible = Login.showCopyright;
        }

        private void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            bool state = Validar();
            if (state)
            {
                if(Global.isConected)
                {
                    Device.BeginInvokeOnMainThread(async() => {
                        RestClient cliente = new RestClient();
                        var result = await cliente.Login<AccountData>(EntryAccount.Text, EntryMail.Text, EntryPassword.Text);
                        if(result != null){
                            Global.deviceList = result.DeviceList;
                            LoginModel.AccountID = EntryAccount.Text;
                            LoginModel.Mail = EntryMail.Text;
                            LoginModel.Password = EntryPassword.Text;
                            if(Login.autoLogin){
                                LoginModel.autoLogin = true;
                            }
                            Navigation.InsertPageBefore(new ShowMapView(), this);
                            await Navigation.PopAsync(true);
                        }
                    });
                }
                else
                {
                    DisplayAlert("Error", Global.failNetwork, "Entendido.");
                }
            }
            else
            {
                DisplayAlert("Error",msgError,"Entendido");
            }
        }

        bool Validar()
        {
            if (Login.AccountOrMail == 0)
            {
                if (string.IsNullOrEmpty(EntryAccount.Text))
                {
                    msgError = Login.EntryAccountEmpty;
                    return false;
                }
                if (string.IsNullOrEmpty(EntryMail.Text))
                {
                    msgError = Login.EntryMailEmpty;
                    return false;
                }
            }
            else if (Login.AccountOrMail == 1)
            {
                if (Login.ValidarEntry && !EntryMail.Text.Contains("@"))
                {
                    msgError = Login.EntryMailNotValid;
                    return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(EntryMail.Text))
                    {
                        msgError = Login.EntryMailEmpty;
                        return false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(EntryAccount.Text))
                {
                    msgError = Login.EntryAccountEmpty;
                    return false;
                }
            }
            
            if (string.IsNullOrEmpty(EntryPassword.Text))
            {
                msgError = Login.EntryPasswordEmpty;
                return false;
            }

            msgError = "";
            return true;
        }
    }
}