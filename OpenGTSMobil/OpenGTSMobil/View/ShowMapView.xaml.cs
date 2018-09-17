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
                MapaProvider = "GoogleMaps";
                Attr.IsVisible = false;
            }
            else
            {
                map.MapType = (ShowMap.typeMap == MapType.None) ? MapType.Street : ShowMap.typeMap;
                map.TileLayers.Clear();
                var tiles = TileLayer.FromTileUri((int x, int y, int z) => new Uri($"" + ShowMap.MapProviderServer));
                map.TileLayers.Add(tiles);
                MapaProvider = "OSM";
                Attr.IsVisible = true;
            }
            if (string.IsNullOrEmpty(ShowMap.cerrarCuenta))
            {
                buttonExit.IsVisible = true;
                exitImage.IsVisible = false;
            }
            else
            {
                buttonExit.IsVisible = false;
                exitImage.Source = ShowMap.cerrarCuenta;
                exitImage.IsVisible = true;
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
                DisplayAlert("Error", Global.failNetwork, "Entendido.");
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
            for (int i = 0; i < Global.deviceList.Count; i++)
            {
                List<EventData> ed = Global.deviceList[i].EventData;
                for (int edi = 0; edi < ed.Count; edi++)
                {
                    posi = new Position(double.Parse(ed[edi].GPSPoint_lat), double.Parse(ed[edi].GPSPoint_lon));
                    //polyline.Positions.Add(posi);
                    if (!isFleet && !string.IsNullOrEmpty(deviceID))
                    {
                        if (ed[edi].Device == deviceID)
                        {
                            map.Pins.Add(new Pin { Label = PinLabel(ed[edi]), Position = posi, Address = ed[edi].Address });
                        }
                    }
                    else
                    {
                        map.Pins.Add(new Pin { Label = PinLabel(ed[edi]), Position = posi, Address = ed[edi].Address });
                    }
                }
            }
            map.MoveToRegion(MapSpan.FromCenterAndRadius(posi, Distance.FromMeters(1000)));
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
            labelvehicle.Text = itemIndexString;
            bool isFleet = false;
            if (itemIndexString.Equals("Grupo"))
            {
                isFleet = true;
                labelvehicle.Text = "Grupo";
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
            PickerVehicle.Items.Add("Grupo");
            for (int i = 0; i < Global.deviceList.Count; i++)
            {
                PickerVehicle.Items.Add(Global.deviceList[i].Device_desc);
            }
            int index = (ConfigModel.lastVehicleSelected > Global.deviceList.Count) ? 0 : ConfigModel.lastVehicleSelected;
            PickerVehicle.SelectedIndex = index;
            string itemIndexString = PickerVehicle.Items[index];
            labelvehicle.Text = itemIndexString;
            bool isFleet = false;
            if (itemIndexString.Equals("Grupo"))
            {
                isFleet = true;
                labelvehicle.Text = "Grupo";
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
             *  [#350]   [Viejo de Jose Ortega] : Estacionado
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
    }
}
