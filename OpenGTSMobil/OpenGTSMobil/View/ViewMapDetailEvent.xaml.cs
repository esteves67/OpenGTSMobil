using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenGTSMobil.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewMapDetailEvent : ContentPage
	{
        public ViewMapDetailEvent(string Lat, string Long)
        {
            InitializeComponent();
            webView.Source = "https://maps.google.com/?q=" + Lat + "," + Long;
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
        }
    }
}