using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGTSMobil.Models
{
    public class ConfigModel
    {
        /*obtiene el permiso*/
        private static ISettings AppSettings
        {
            get
            {
                if (CrossSettings.IsSupported)
                    return CrossSettings.Current;

                return null;
            }
        }

        //ultimo vehiculo seleccionado en al sesion pasada
        public static int lastVehicleSelected
        {
            get => AppSettings.GetValueOrDefault(nameof(lastVehicleSelected), 0);
            set => AppSettings.AddOrUpdateValue(nameof(lastVehicleSelected), value);
        }
    }
}
