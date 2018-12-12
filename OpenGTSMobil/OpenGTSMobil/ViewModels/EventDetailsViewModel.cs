using OpenGTSMobil.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using static OpenGTSMobil.Models.ResultAPI;

namespace OpenGTSMobil.ViewModels
{
    class EventDetailsViewModel : INotifyPropertyChanged
    {
        public string SizeIndex { get; set; }
        public string SizeState { get; set; }
        public string SizeTime { get; set; }
        public string SizeSpeed { get; set; }
        //propiedad de property changed
        public event PropertyChangedEventHandler PropertyChanged;

        // funcion que ejecuata el cambio de propiedades
        protected void OnPropertyChange([CallerMemberName]  string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<EventData> _ListEventDetail;

        public ObservableCollection<EventData> ListEventDetail
        {
            get { return _ListEventDetail; }
            set { _ListEventDetail = value; OnPropertyChange(); }
        }

        public EventDetailsViewModel()
        {
            if (Device.RuntimePlatform.Equals("iOS"))
            {
                if (Global.vehicleSelect != null)
                {
                    if (Global.vehicleSelect.Equals("TODOS"))
                    {
                        SizeIndex = "9";
                        SizeState = "9";
                        SizeTime = "9";
                        SizeSpeed = "9";
                    }
                    else
                    {
                        SizeIndex = "9";
                        SizeState = "9";
                        SizeTime = "9";
                        SizeSpeed = "9";
                    }
                }
            }
            else
            {
                if (Global.vehicleSelect != null)
                {
                    if (Global.vehicleSelect.Equals("TODOS"))
                    {
                        SizeIndex = "9";
                        SizeState = "9";
                        SizeTime = "9";
                        SizeSpeed = "9";
                    }
                    else
                    {
                        SizeIndex = "9";
                        SizeState = "9";
                        SizeTime = "9";
                        SizeSpeed = "9";
                    }
                }
            }
            ListEventDetail = new ObservableCollection<EventData>();

            ListEventDetail = EventDetails.EDM;
        }
    }
}
