using OpenGTSMobil.Models;
using OpenGTSMobil.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using static OpenGTSMobil.Models.ResultAPI;

namespace OpenGTSMobil.View
{
    public partial class ShowMapView : ContentPage
    {
        public string MapaProvider = "";
        public Polyline line = new Polyline();
        public string GoogleMapsSelected = "GoogleMaps";
        public string OSMSelected = "OSM";
        public string TitleErrorAlert = "Error";
        public string GrupoTextString = "Grupo";

        public ShowMapView()
        {
            InitializeComponent();
            aplicar();              //aplica las configuraciones y estilo.
        }

        void aplicar()
        {
            NavigationPage.SetHasNavigationBar(this, ShowMap.showNavBar);
            map.BackgroundColor = Color.FromHex(ShowMap.colorBackgroundMap);
            if (ShowMap.useGoogleMaps)
            {
                map.TileLayers.Clear();
                map.MapType = (ShowMap.typeMap == MapType.None)? MapType.Street : ShowMap.typeMap;
                MapaProvider = GoogleMapsSelected;
                Attr.IsVisible = false;
            }
            else
            {
                map.TileLayers.Clear();
                var tiles = TileLayer.FromTileUri((int x, int y, int z) => new Uri($""+ShowMap.MapProviderServer));
                MapaProvider = OSMSelected;
                Attr.IsVisible = true;
                map.TileLayers.Add(tiles);
            }
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(ShowMap.defaultPosition.Split(',')[0]), double.Parse(ShowMap.defaultPosition.Split(',')[1])), Distance.FromMeters(int.Parse(ShowMap.defaultPosition.Split(',')[2]))));
            map.UiSettings.MyLocationButtonEnabled = ShowMap.showMyLocation;
            Attr.Text = ShowMap.attrMap;
            map.UiSettings.ZoomControlsEnabled = ShowMap.showZoomMap;
            getServerData();
        }

        /*retorna un JSON con los datos*/
        void getServerData()
        {
            if (Global.isConected)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    RestClient cliente = new RestClient();
                    var result = await cliente.Login<AccountData>(LoginModel.AccountID, LoginModel.Mail, LoginModel.Password);
                    if (result != null)
                    {
                        if (Global.deviceList != null && Global.deviceList.Count > 0)
                        {
                            Global.deviceList.Clear();
                        }
                        Global.deviceList = result.DeviceList;
                        chargerData();
                    }
                });
            }
            else
            {
                DisplayAlert(TitleErrorAlert, Global.failNetwork, "Entendido.");
            }
        }

        /* Muestra los puntos en el mapa con su ruta*/
        void RenderMap(bool isFleet = false, string deviceID = "")
        {
            cleanMap();
            var polyline = new Polyline();
            Position posi = new Position();
            polyline.ZIndex = 1;
            polyline.StrokeWidth = ShowMap.anchoLineaMap;
            string rt = TimePicker.Date.ToString("yyyy/MM/dd") + "/23:59:59";
            if (isFleet)    // muestra de eventos por grupo.
            {
                Device.BeginInvokeOnMainThread(async() => {
                    RestClient client = new RestClient();
                    var response = await client.GetEventGroup<AccountData>();
                    if (response != null)
                    {
                        List<DeviceList> ld = response.DeviceList;
                        for (int di = 0; di < ld.Count; di++)
                        {
                            List<EventData> ed = ld[di].EventData;
                            for (int edi = 0; edi < ed.Count; edi++)
                            {
                                posi = new Position(double.Parse(ed[edi].GPSPoint_lat), double.Parse(ed[edi].GPSPoint_lon));
                                map.Pins.Add(new Pin { Label = PinLabel(ed[edi]), Position = posi, Address = ed[edi].Address, Icon = BitmapDescriptorFactory.FromBundle("pin30_blue.png") });
                            }
                        }
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(posi, Distance.FromMeters(10000)));
                    }
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => {
                    RestClient client = new RestClient();
                    var response = await client.GetEventsVehicle<AccountData>(deviceID,rt);
                    if (response != null)
                    {
                        List<DeviceList> ld = response.DeviceList;
                        for (int di = 0; di < ld.Count; di++)
                        {
                            List<EventData> ed = ld[di].EventData;
                            for (int edi = 0; edi < ed.Count; edi++)
                            {
                                posi = new Position(double.Parse(ed[edi].GPSPoint_lat), double.Parse(ed[edi].GPSPoint_lon));
                                polyline.Positions.Add(posi);
                                map.Pins.Add(new Pin { Label = PinLabel(ed[edi]), Position = posi, Address = ed[edi].Address, Tag=ed[edi].Index, Icon = BitmapDescriptorFactory.FromBundle(CalculatePushPin(ed[edi].Heading,ed[edi].Speed)) });
                            }
                        }
                        if (response.DeviceList[0].EventData.Count > 1)
                        {
                            map.Polylines.Add(polyline);
                        }
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(posi, Distance.FromMeters(1000)));
                    }
                });
            }
        }

        /*Limpiar lineas y puntos*/
        void cleanMap()
        {
            map.Pins.Clear();
            map.Polylines.Clear();
        }

        private void PickerVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = PickerVehicle.SelectedIndex;
            ConfigModel.lastVehicleSelected = index;
            string itemIndexString = PickerVehicle.Items[index];
            vehiculo.Text = itemIndexString;
            bool isFleet = false;
            if (itemIndexString.Equals(GrupoTextString))
            {
                isFleet = true;
                vehiculo.Text = GrupoTextString;
            }
            else
            {
                itemIndexString = Global.deviceList[index - 1].Device;
            }
            RenderMap(isFleet, itemIndexString);

        }

        /* carga los datos en el picker */
        void chargerData()
        {
            PickerVehicle.Items.Clear();
            PickerVehicle.Items.Add(GrupoTextString);
            for (int i = 0; i < Global.deviceList.Count; i++)
            {
                PickerVehicle.Items.Add(Global.deviceList[i].Device_desc);
            }
            int index = (ConfigModel.lastVehicleSelected > Global.deviceList.Count) ? 0 : ConfigModel.lastVehicleSelected;
            PickerVehicle.SelectedIndex = index;
            string itemIndexString = PickerVehicle.Items[index];
            vehiculo.Text = itemIndexString;
            bool isFleet = false;
            if (itemIndexString.Equals(GrupoTextString))
            {
                isFleet = true;
                vehiculo.Text = GrupoTextString;
            }
            else
            {
                itemIndexString = Global.deviceList[index].Device;
            }
            RenderMap(isFleet, itemIndexString);    
        }

        /* redner label pin*/
        public string PinLabel(EventData model)
        {

            /*
                [#350]   [Viejo de Jose Ortega] : Estacionado
                Fecha: 25/06/2018 19:01:22 [GMT-05:00]
                GPS: -2.14046 / -79.92809 [#Sats 18] MAPA GOOGLE
                Velocidad: 0.0 km/h
                Altitud: 21 Metros
                Dirección: Oficina
                Número de satélites: 18
                Odómetro (km): 9681.2 Km
             */
            string data = "";
            data += string.Format("[#{0}] [{1}]: {2} \n", model.Index, model.Device, model.StatusCode_desc);
            data += string.Format("Fecha: {0} {1} [GMT-05:00] \n", model.Timestamp_date, model.Timestamp_time);
            data += string.Format("GPS: {0} \n", model.GPSPoint);
            data += string.Format("Velocidad: {0} km/h \n", model.Speed);
            data += string.Format("Altitud: {0} Metros \n", model.Altitude);
            data += string.Format("Dirección: {0} \n", model.Address);
            data += string.Format("Odómetro (km): {0} \n", model.Odometer);
            return data;
        }

        /* tap label vehicle*/
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            PickerVehicle.Focus();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            exit();
        }

        /*exit*/
        async void exit()
        {
            LoginModel.AccountID = "";
            LoginModel.Mail = "";
            LoginModel.Password = "";
            LoginModel.autoLogin = false;
            Navigation.InsertPageBefore(new LoginView(), this);
            await Navigation.PopAsync(true);
        }

        private void buttonExit_Clicked(object sender, EventArgs e)
        {
            exit();
        }

        /* Calcular direccion del push pin */
        private string CalculatePushPin(string heading, string speed)
        {
            double _speed = (!string.IsNullOrEmpty(speed))? double.Parse(speed) : 0.0;
            double _heading = (!string.IsNullOrEmpty(heading))? double.Parse(heading) : 0.0;
            if (_speed < 2)
            {
                return "pin30_blue_dot.png";
            }
            if ((_heading >= 337 && _heading <= 360) || (_heading >= 1 && _heading < 23))
            {
                return "pin30_blue_h0.png";
            }
            if (_heading >= 23 && _heading < 68)
            {
                return "pin30_blue_h1.png";
            }
            if (_heading >= 68 && _heading < 113)
            {
                return "pin30_blue_h2.png";
            }
            if (_heading >= 113 && _heading < 158)
            {
                return "pin30_blue_h3.png";
            }
            if (_heading >= 158 && _heading < 203)
            {
                return "pin30_blue_h4.png";
            }
            if(_heading >= 203 && _heading < 248)
            {
                return "pin30_blue_h5.png";
            }
            if (_heading >= 248 && _heading < 293)
            {
                return "pin30_blue_h6.png";
            }
            if (_heading >= 293 && _heading < 337)
            {
                return "pin30_blue_h7.png";
            }
            return "pin30_blue_dot.png";
        }

        /* focus picekr vheicle */
        private void Vehiculo_Clicked(object sender, EventArgs e)
        {
            PickerVehicle.Focus();
        }

        /* Focus time picker */
        private void TimeDate_Clicked(object sender, EventArgs e)
        {
            TimePicker.Focus();
        }

        /* command picker focus*/
        private void CommandPicker_Clicked(object sender, EventArgs e)
        {
            SelectCommand.Focus();
        }

        private void EventDetailList_Clicked(object sender, EventArgs e)
        {
            /* aqui va al salto de pagina de detalles de eventos 
                
               aun no esta construido.

             */
        }
    }
}
