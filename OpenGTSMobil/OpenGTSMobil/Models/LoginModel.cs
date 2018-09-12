using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGTSMobil.Models
{

    public class LoginModel
    {
        /*obtiene el permiso*/
        private static ISettings AppSettings
        {
            get
            {
                if (CrossSettings.IsSupported)
                    return CrossSettings.Current;

                return null; // or your custom implementation 
            }
        }

        //accountID
        public static string AccountID
        {
            get => AppSettings.GetValueOrDefault(nameof(AccountID), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(AccountID), value);
        }

        //Mail
        public static string Mail
        {
            get => AppSettings.GetValueOrDefault(nameof(Mail), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Mail), value);
        }

        //Password
        //0 es la contrase#a por defecto errada.
        public static int Password
        {
            get => AppSettings.GetValueOrDefault(nameof(Password), 0);
            set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        }
    }
}
