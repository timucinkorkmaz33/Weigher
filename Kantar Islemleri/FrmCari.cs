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
    public partial class FrmCari : DevExpress.XtraEditors.XtraForm
    {
        DoraKantar db = new DoraKantar();
        public FrmCari()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (lblid.Text == null || lblid.Text == "")
            {
                CariBilgileri cari = new CariBilgileri();
                cari.Cari_Adi = txtCariAdi.Text;
                cari.Cari_Kodu = txtCariKodu.Text;
                cari.Cari_Kayit_Tarihi = DateTime.Now;
                cari.Cari_Vergi_Dairesi = txtVergiDairesi.Text;
                cari.Cari_Vergi_No = txtVergiNo.Text;
                cari.Cari_Telefon = txtTelefon.Text;
                cari.Cari_Adres1 = txtAdres1.Text;
                cari.Cari_Adres2 = txtAdres2.Text;
                cari.Cari_Yetkili = txtYetkili.Text;
                cari.Cari_Yetkili_Telefon = txtYetkiliTelefon.Text;
                cari.Cari_Yetkili_EMail = txtYetkiliEMail.Text;
                db.CariBilgileri.Add(cari);
                db.SaveChanges();
            }
            else
            {
                var id = Convert.ToInt32(lblid.Text);
                CariBilgileri cari = db.CariBilgileri.Where(u => u.Id == id).Select(u => u).ToList().FirstOrDefault();
                cari.Cari_Adi = txtCariAdi.Text;
                cari.Cari_Kodu = txtCariKodu.Text;
                cari.Cari_Kayit_Tarihi = DateTime.Now;
                cari.Cari_Vergi_Dairesi = txtVergiDairesi.Text;
                cari.Cari_Vergi_No = txtVergiNo.Text;
                cari.Cari_Telefon = txtTelefon.Text;
                cari.Cari_Adres1 = txtAdres1.Text;
                cari.Cari_Adres2 = txtAdres2.Text;
                cari.Cari_Yetkili = txtYetkili.Text;
                cari.Cari_Yetkili_Telefon = txtYetkiliTelefon.Text;
                cari.Cari_Yetkili_EMail = txtYetkiliEMail.Text;

                db.SaveChanges();
            }
            MessageBox.Show("Cari Ekleme İşlemi Başarılı");
            Temizle();
            FrmCari_Load(sender, e);

        }
        private void Temizle()
        {
            txtAdres1.Text = "";
            txtAdres2.Text = "";
            txtCariAdi.Text = "";
            txtCariKodu.Text = "";
            txtTelefon.Text = "";
            txtVergiDairesi.Text = "";
            txtVergiNo.Text = "";
            txtYetkili.Text = "";
            txtYetkiliEMail.Text = "";
            txtYetkiliTelefon.Text = "";
            lblid.Text = "";
        }

        private void FrmCari_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'doraKantarDataSet.CariBilgileri' table. You can move, or remove it, as needed.
            this.cariBilgileriTableAdapter.Fill(this.doraKantarDataSet.CariBilgileri);

            // TODO: This line of code loads data into the 'doraKantarDataSet1.CariBilgileri' table. You can move, or remove it, as needed.
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var a = gridView1.FocusedRowHandle;
            var x = gridView1.GetRowCellValue(a, "Id").ToString();
            var id = Convert.ToInt32(x);
            var sorgu = db.CariBilgileri.Where(u => u.Id == id).ToList().FirstOrDefault();
            txtAdres1.Text = sorgu.Cari_Adres1;
            txtAdres2.Text = sorgu.Cari_Adres2;
            txtCariAdi.Text = sorgu.Cari_Adi;
            txtCariKodu.Text = sorgu.Cari_Kodu;
            txtTelefon.Text = sorgu.Cari_Telefon;
            txtVergiDairesi.Text = sorgu.Cari_Vergi_Dairesi;
            txtVergiNo.Text = sorgu.Cari_Vergi_No.ToString();
            txtYetkili.Text = sorgu.Cari_Yetkili;
            txtYetkiliEMail.Text = sorgu.Cari_Yetkili_EMail;
            txtYetkiliTelefon.Text = sorgu.Cari_Yetkili_Telefon;
            lblid.Text = sorgu.Id.ToString();
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var kullanici = Login.Loginname;
            var yetki = db.KullaniciBilgileri.Where(u => u.KullaniciAdi == kullanici).Select(u => u.Roles).FirstOrDefault();
            if (yetki != "User")
            {
                var a = gridView1.FocusedRowHandle;
                var x = gridView1.GetRowCellValue(a, "Id").ToString();
                var id = Convert.ToInt32(x);
                if (MessageBox.Show("Kaydı Silmek İstiyor Musunuz?", "Cari Silme Onayı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var cariBilgileri = db.CariBilgileri.Find(id);
                    db.CariBilgileri.Remove(cariBilgileri);
                    db.SaveChanges();
                }
                FrmCari_Load(sender, e);
            }
            else
            {
                MessageBox.Show("İşlemi yapmaya yetkiniz yoktur!");
            }
        }
    }

}