using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using static OpenGTSMobil.Models.ResultAPI;

namespace OpenGTSMobil.Models
{
    public class Global
    {
        public static bool isConected { get; set; }                                     //define el estado de red
        public static string urlServer = "";     //Url de consulta Events de OpenGTS en formato "JSON"
        public static List<DeviceList> deviceList { get; set; }
        public static string failNetwork = "Verifique su estado de red";
    }

    public class Login
    {
        /* Estilo */
        public static bool showNavBar = false;
        public static string colorNavBar = "";                              //el color debe ser definido en HTML (#ffffff)
        public static string colorTextNavBar = "";                          //el color debe ser definido en HTML (#ffffff)

        /*Iconos, logos, background */
        public static string BackgroundImage = "";
        public static string ImageLogo = "";
        public static string IconAccount = "";
        public static string IconMail = "";
        public static string IconPassword = "";
        public static string ButtonLogin = "";

        /* Configuracion, validaciones */
        public static bool showCopyright = true;       // Muestra el label de copyright en la parte inferior del login
        public static bool ValidarEntry = true;        // validar entry que sea un correo.
        public static int AccountOrMail = 0;           // 0 = "Todos(defecto)", 1 = "Correo", 2 = "Account"
        public static bool autoLogin = true;           // Auto iniciar sesion al reaperturar la app

        /*Display Alerts Mensajes*/
        public static string labelCopyright = "Desarrollado por 4jay contacto: Leonardomanrique9@gmail.com";
        public static string EntryAccountEmpty = "Campo de cuenta vacio por favor verifique.";
        public static string EntryMailEmpty = "Campo de correo vacio por favor verifique.";
        public static string EntryMailNotValid = "No es un correo valido";
        public static string MailNotValid = "Correo ingresado no valido no valido";
        public static string EntryPasswordEmpty = "Campo de contrase#a vacio";
        public static string FailLogin = "Credenciales incorrectas por favopr verifique.";
    }

    public class ShowMap
    {
        /* Estilo */
        public static bool showNavBar = false;
        public static string colorNavBar = "";                              //el color debe ser definido en HTML (#ffffff)
        public static string colorTextNavBar = "";

        /* Maps, Configuracion, Servidor */
        public static bool showZoomMap = true;
        public static string MapProviderServer = "https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}";        //por defecto es OSM, se omite si se utiliza Google Maps
        public static string attrMap = "";                                                                    //este es el texto de atribuciones al mapa no lo desactive para evitar bloqueos
        public static bool useGoogleMaps = false;                                                             //debe preconfigurar el APIKEY en los lanzadores de cada solucion.
        public static MapType typeMap = MapType.None;                                                         //tipo de mapa con GoogleMaps.
        public static string colorBackgroundMap = "";                                                         //el color debe ser definido en HTML (#ffffff)
        public static string colorLineMap = "";                                                               //color de las lineas en el mapa.
        public static float anchoLineaMap = 5f;                                                               //ancho de marca de lineas en el mapa.
        public static bool showMyLocation = false;                                                            //mostrar ubacion del usuario.
        public static string defaultPosition = "-2.0000000,-77.5000000";                                      //posicion del mapa por defecto ejemplo:(-2.0000000,-77.5000000) Ecuador
    }
}
