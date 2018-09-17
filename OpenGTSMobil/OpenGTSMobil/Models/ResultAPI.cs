using System;
using System.Collections.Generic;

namespace OpenGTSMobil.Models
{
    public class ResultAPI
    {
        public class EventData
        {
            public string Device { get; set; }
            public string Timestamp { get; set; }
            public string Timestamp_date { get; set; }
            public string Timestamp_time { get; set; }
            public string StatusCode { get; set; }
            public string StatusCode_hex { get; set; }
            public string StatusCode_desc { get; set; }
            public string GPSPoint { get; set; }
            public string GPSPoint_lat { get; set; }
            public string GPSPoint_lon { get; set; }
            public string Speed_kph { get; set; }
            public string Speed { get; set; }
            public string Speed_units { get; set; }
            public string Altitude_meters { get; set; }
            public string Altitude { get; set; }
            public string Altitude_units { get; set; }
            public string Odometer_km { get; set; }
            public string Odometer { get; set; }
            public string Odometer_units { get; set; }
            public string Geozone { get; set; }
            public string Geozone_index { get; set; }
            public string Address { get; set; }
            public string Index { get; set; }
        }

        public class DeviceList
        {
            public string Device { get; set; }
            public string Device_desc { get; set; }
            public List<EventData> EventData { get; set; }
        }

        public class AccountData
        {
            public string Account { get; set; }
            public string Account_desc { get; set; }
            public string TimeZone { get; set; }
            public List<DeviceList> DeviceList { get; set; }
            public string Error { get; set; }
        }
    }
}
