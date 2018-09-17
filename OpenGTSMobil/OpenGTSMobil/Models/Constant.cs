using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using static OpenGTSMobil.Models.ResultAPI;

namespace OpenGTSMobil.Models
{
    /// <summary>
    /// Clase de configuracion del clinte, todo ajuste o cambio en las vistas es obtenido desde esta clase.
    /// para algun ajuste personalizado contacte con leonardomanrique9@gmail.com.
    /// puede obtener la app con soporte y modificacion anual por un costo.
    /// 
    /// - todos los campos en formato String referente a colores deben ingresarse en codigo HEX HTML.
    /// - recuerde compilar y copiar el Events.war en su servidor para hacer las solicitudes.
    /// - para el soporte de google maps debe ingresar el ApiKEY referente a cada proyecto.
    /// - soporte para mapas open source por ejemplo OSM. Recuerde que debe agregar sus respectivas atribuciones de Tiles para evitar bloqueos.
    /// </summary>
    public class Global
    {
        public static bool isConected { get; set; }                                     //define el estado de red
        public static string urlServer = "";                                            //Url de consulta Events de OpenGTS en formato "JSON"
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
        public static string ImageLogo = "logo.png";
        public static string IconAccount = "cuenta.png";
        public static string IconMail = "usuario.png";
        public static string IconPassword = "pass.png";
        public static string ButtonLogin = "";

        /* Configuracion, validaciones */
        public static bool showCopyright = true;       // Muestra el label de copyright en la parte inferior del login
        public static bool ValidarEntry = true;        // validar entry que sea un correo.
        public static int AccountOrMail = 0;           // 0 = "Todos(defecto)", 1 = "Correo", 2 = "Account"
        public static bool autoLogin = true;           // Auto iniciar sesion al reaperturar la app

        /*Display Alerts Mensajes*/
        public static string labelCopyright = "Desarrollado por 4jay contacto: Leonardomanrique9@gmail.com";
        public static string EntryAccountEmpty = "Campo de cuenta vació por favor verifique.";
        public static string EntryMailEmpty = "Campo de correo vacio por favor verifique.";
        public static string EntryMailNotValid = "No es un correo valido";
        public static string MailNotValid = "Correo ingresado no valido no valido";
        public static string EntryPasswordEmpty = "Campo de contraseña vació";
        public static string FailLogin = "Credenciales incorrectas por favor verifique.";
    }

    public class ShowMap
    {
        /* Estilo */
        public static bool showNavBar = false;
        public static string colorNavBar = "";                              //el color debe ser definido en HTML (#ffffff)
        public static string colorTextNavBar = "";

        /*Icon*/
        public static string cerrarCuenta = "exit.png";                     //icono de salida

        /* Maps, Configuracion, Servidor */
        public static bool showZoomMap = true;
        public static string MapProviderServer = "https://a.tile.openstreetmap.org/{z}/{x}/{y}.png";          //por defecto es OSM, se omite si se utiliza Google Maps
        public static string attrMap = "http://www.openstreetmap.org/copyright OpenStreetMap";                //este es el texto de atribuciones al mapa no lo desactive para evitar bloqueos
        public static bool useGoogleMaps = true;                                                              //debe preconfigurar el APIKEY en los lanzadores de cada solucion.
        public static MapType typeMap = MapType.None;                                                         //tipo de mapa con GoogleMaps.
        public static string colorBackgroundMap = "";                                                         //el color debe ser definido en HTML (#ffffff)
        public static string colorLineMap = "";                                                               //color de las lineas en el mapa.
        public static float anchoLineaMap = 5f;                                                               //ancho de marca de lineas en el mapa.
        public static bool showMyLocation = false;                                                            //mostrar ubacion del usuario.
        public static string defaultPosition = "-2.0000000,-77.5000000,1000";                                 //posicion del mapa por defecto ejemplo:(latitud,longitud,metros) Ecuador
    }
}
