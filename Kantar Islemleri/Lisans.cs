using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kantar_Islemleri
{
    public partial class Lisans : DevExpress.XtraEditors.XtraForm
    {
        public Lisans()
        {
            InitializeComponent();
        }

        private void Lisans_Load(object sender, EventArgs e)
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Blue";
        }
    }
}
