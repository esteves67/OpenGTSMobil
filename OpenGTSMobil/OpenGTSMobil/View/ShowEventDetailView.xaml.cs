using OpenGTSMobil.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static OpenGTSMobil.Models.ResultAPI;

namespace OpenGTSMobil.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowEventDetailView : ContentPage
	{
        public static string Lat { get; set; }
        public static string Long { get; set; }

        public ShowEventDetailView (EventData item)
		{
			InitializeComponent();
            this.Title = item.Index;
            BindingContext = new ShowEventDetailViewModel(item);
            Lat = item.GPSPoint_lat;
            Long = item.GPSPoint_lon;
            string HTTPS = "https://www.google.com/maps/@?api=1&map_action=pano&viewpoint=" + Lat + "," + Long + "&heading=-45&pitch=0&fov=80";
            webView.Source = HTTPS;
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
        }

        private void Map_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewMapDetailEvent(Lat, Long) { Title = "Mapa" });
        }

        private void StreetView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StreetViewMap(Lat, Long) { Title = "Street View" });
        }
    }
}