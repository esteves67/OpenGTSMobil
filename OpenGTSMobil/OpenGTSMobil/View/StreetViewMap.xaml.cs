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
	public partial class StreetViewMap : ContentPage
	{
        public StreetViewMap(string Lat, string Long)
        {
            InitializeComponent();
            string HTTPS = "https://www.google.com/maps/@?api=1&map_action=pano&viewpoint=" + Lat + "," + Long + "&heading=0&pitch=38&fov=80";
            webView.Source = HTTPS;

        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
        }
    }
}