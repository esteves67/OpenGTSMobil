using OpenGTSMobil.Models;
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
	public partial class EventDetailsView : ContentPage
	{
		public EventDetailsView ()
		{
			InitializeComponent ();
            BindingContext = new EventDetailsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Title = Global.vehicleSelect;
        }

        private void ListViewEvents_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as EventData;
            Navigation.PushAsync(new ShowEventDetailView(item));
        }

        private void ListViewEvents_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListViewEvents.SelectedItem = null;
        }
    }
}