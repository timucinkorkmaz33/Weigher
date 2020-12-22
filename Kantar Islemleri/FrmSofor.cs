using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Kantar_Islemleri.Model;

namespace Kantar_Islemleri
{
    public partial class FrmSofor : DevExpress.XtraEditors.XtraForm
    {
        DoraKantar db = new DoraKantar();

        public FrmSofor()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (lblid.Text == "" || lblid.Text == null)
            {
                SoforBilgileri sfr = new SoforBilgileri();
                sfr.Adres = txtAdres.Text;
                sfr.IsimSoyisim = txtIsimSoyisim.Text;
                sfr.TC = txtTC.Text;
                sfr.Telefon = txtTelefon.Text;
                db.SoforBilgileri.Add(sfr);
                db.SaveChanges();
            }
            else
            {
                var id = Convert.ToInt32(lblid.Text);
                SoforBilgileri sfr = db.SoforBilgileri.Where(u => u.Id == id).Select(u => u).ToList().FirstOrDefault();
                sfr.Adres = txtAdres.Text;
                sfr.IsimSoyisim = txtIsimSoyisim.Text;
                sfr.TC = txtTC.Text;
                sfr.Telefon = txtTelefon.Text;
                db.SaveChanges();
            }
            MessageBox.Show("Şöför Kayıt İşlemi Başarılı");
            FrmSofor_Load(sender, e);
            Temizle();

        }
        private void Temizle()
        {
            txtAdres.Text = "";
            txtIsimSoyisim.Text = "";
            txtTC.Text = "";
            txtTelefon.Text = "";
            lblid.Text = "";
        }

        private void FrmSofor_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'doraKantarDataSet.SoforBilgileri' table. You can move, or remove it, as needed.
            this.soforBilgileriTableAdapter.Fill(this.doraKantarDataSet.SoforBilgileri);
            // TODO: This line of code loads data into the 'doraKantarDataSet.SoforBilgileri' table. You can move, or remove it, as needed.

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var a = gridView1.FocusedRowHandle;
            var x = gridView1.GetRowCellValue(a, "Id").ToString();
            var id = Convert.ToInt32(x);
            var sorgu = db.SoforBilgileri.Where(u => u.Id == id).ToList().FirstOrDefault();
            txtAdres.Text = sorgu.Adres;
            txtIsimSoyisim.Text = sorgu.IsimSoyisim;
            txtTC.Text = sorgu.TC;
            txtTelefon.Text = sorgu.Telefon;
            lblid.Text = sorgu.Id.ToString();
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        { var kullanici = Login.Loginname;
            var yetki = db.KullaniciBilgileri.Where(u => u.KullaniciAdi == kullanici).Select(u => u.Roles).FirstOrDefault();
            if (yetki != "User")
            {
                var a = gridView1.FocusedRowHandle;
                var x = gridView1.GetRowCellValue(a, "Id").ToString();
                var id = Convert.ToInt32(x);
                if (MessageBox.Show("Kaydı Silmek İstiyor Musunuz?", "Şöför Bilgileri Silme Onayı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var söförBilgileri = db.SoforBilgileri.Find(id);
                    db.SoforBilgileri.Remove(söförBilgileri);
                    db.SaveChanges();
                }
                FrmSofor_Load(sender, e);
            }
            else
            {
                MessageBox.Show("İşlemi yapmaya yetkiniz yoktur");
            }
        }
    }
}
