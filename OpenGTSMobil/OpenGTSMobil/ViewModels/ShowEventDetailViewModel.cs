using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using static OpenGTSMobil.Models.ResultAPI;

namespace OpenGTSMobil.ViewModels
{
    class ShowEventDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]  string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string index { get; set; }
        public string Index { get { return index; } set { index = value; OnPropertyChanged(); } }

        private string statusCode_desc { get; set; }
        public string StatusCode_desc { get { return statusCode_desc; } set { statusCode_desc = value; OnPropertyChanged(); } }

        private string gPSPoint { get; set; }
        public string GPSPoint { get { return gPSPoint; } set { gPSPoint = value; OnPropertyChanged(); } }

        private string timesTamp_date { get; set; }
        public string Timestamp_date { get { return timesTamp_date; } set { timesTamp_date = value; OnPropertyChanged(); } }

        private string heading { get; set; }
        public string Heading { get { return heading; } set { heading = value; OnPropertyChanged(); } }

        private string speed { get; set; }
        public string Speed { get { return speed; } set { speed = value; OnPropertyChanged(); } }

        private string vehicleBatteryVolts { get; set; }
        public string VehicleBatteryVolts { get { return vehicleBatteryVolts; } set { vehicleBatteryVolts = value; OnPropertyChanged(); } }

        private string odometer { get; set; }
        public string Odometer { get { return odometer; } set { odometer = value; OnPropertyChanged(); } }

        private string address { get; set; }
        public string Address { get { return address; } set { address = value; OnPropertyChanged(); } }

        public ShowEventDetailViewModel(EventData data)
        {
            Index = data.Index;
            StatusCode_desc = data.StatusCode_desc;
            GPSPoint = data.GPSPoint;
            Timestamp_date = data.Timestamp_time;
            Heading = data.Heading;
            Speed = data.Speed;
            Odometer = data.Odometer;
            Address = data.Address;
        }
    }
}
