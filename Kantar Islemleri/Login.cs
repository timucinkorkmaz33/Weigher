using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Kantar_Islemleri.Model
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        DoraKantar db = new DoraKantar();
        public Login()
        {
            InitializeComponent();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Blue";
        }
        public static string Loginname = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

            var user = txtUser.Text;
            var pass = txtPass.Text;
            var sorgu = db.KullaniciBilgileri.Where(u => u.KullaniciAdi == user && u.Sifre == pass).Count();
            if (sorgu != 0)
            {
                Loginname = user;
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Girdiğiniz Bilgileri Tekrar Kontrol Edin!");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        { 
            
           
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Blue";
          
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1_Click(sender, e);
            }
        }
    }
}
