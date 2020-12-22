using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using Kantar_Islemleri.Model;
using System.Configuration;

namespace Kantar_Islemleri
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            var licence = ConfigurationManager.AppSettings["ProductKey"];
            var licencecontrol = Licence.GetLicenseCode();
            if (licence != licencecontrol)
            {

                Application.Run(new Lisans());
            }
            else
            {
                Application.Run(new Login());
            }
        }
    }
}
