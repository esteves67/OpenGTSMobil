using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
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
        public static bool showNavBar = true;
        public static string colorNavBar = "";                              //el color debe ser definido en HTML (#ffffff)
        public static string colorTextNavBar = "";
    }
}
