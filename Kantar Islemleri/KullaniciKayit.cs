using DevExpress.XtraEditors.Controls;
using Kantar_Islemleri.Model;
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
    public partial class KullaniciKayit : DevExpress.XtraEditors.XtraForm
    {
        DoraKantar db = new DoraKantar();
 
        public KullaniciKayit()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
            if (txtKullaniciId.Text == null || txtKullaniciId.Text == "")
            {
                KullaniciBilgileri kul = new KullaniciBilgileri();
                kul.AdiSoyadi = txtAdSoyad.Text;
                kul.Adres = txtAdres.Text;
                kul.EMail = txtEMail.Text;
                kul.KullaniciAdi = txtKullanici.Text;
                kul.Sifre = txtSifre.Text;
                kul.TelefonNo = txtTelefon.Text;
                kul.Roles = cmbRole.Text;
                db.KullaniciBilgileri.Add(kul);
                db.SaveChanges();
                //RolKullanici rolkul = new RolKullanici();
                //rolkul.RolId = db.RolTanimlari.Where(u => u.RolAdi == cmbRole.SelectedText).FirstOrDefault().Id;
                //rolkul.UserId = db.KullaniciBilgileri.Where(u => u.KullaniciAdi == txtKullanici.Text && u.Sifre == txtSifre.Text).FirstOrDefault().Id;
                //db.RolKullanici.Add(rolkul);
                //db.SaveChanges();
            }
            else
            {
                var id = Convert.ToInt32(txtKullaniciId.Text);
                KullaniciBilgileri kul = db.KullaniciBilgileri.Where(u => u.Id == id).FirstOrDefault();
                kul.AdiSoyadi = txtAdSoyad.Text;
                kul.Adres = txtAdres.Text;
                kul.EMail = txtEMail.Text;
                kul.KullaniciAdi = txtKullanici.Text;
                kul.Sifre = txtSifre.Text;
                kul.TelefonNo = txtTelefon.Text;
                kul.Roles = cmbRole.Text;
                db.SaveChanges();
                //RolKullanici rolkul = db.RolKullanici.Where(u => u.UserId == id).FirstOrDefault();
                //rolkul.RolId = db.RolTanimlari.Where(u => u.RolAdi == cmbRole.SelectedText).FirstOrDefault().Id;
                //rolkul.UserId = db.KullaniciBilgileri.Where(u => u.KullaniciAdi == txtKullanici.Text && u.Sifre == txtSifre.Text).FirstOrDefault().Id;
                //db.SaveChanges();
            }


            MessageBox.Show("Kullanıcı Ekleme işlemi Başarılı");
            Temizle();
            KullaniciKayit_Load(sender,e);
        }
        public void Temizle()
        {
            txtKullaniciId.Text = "";
            txtSifre.Text = "";
            txtTelefon.Text = "";
            txtAdres.Text = "";
            txtAdSoyad.Text = "";
            txtEMail.Text = "";
            txtKullanici.Text = "";
            cmbRole.Text = "";

        }



        private void KullaniciKayit_Load(object sender, EventArgs e)
        {
            cmbRole.Properties.Items.Clear();
            // TODO: This line of code loads data into the 'doraKantarDataSet1.KullaniciBilgileri' table. You can move, or remove it, as needed.
            this.kullaniciBilgileriTableAdapter.Fill(this.doraKantarDataSet1.KullaniciBilgileri);
            var roles = db.RolTanimlari.Where(u=>u.RolAdi!="AppAdmin").Select(u => u.RolAdi).ToList();
           
            foreach (var item in roles)
            {
               cmbRole.Properties.Items.Add(item);
            }
         
           
        }

       

        private void repositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            Temizle();
            var a = gridView1.FocusedRowHandle;
            var x = gridView1.GetRowCellValue(a, "Id").ToString();
            var id = Convert.ToInt32(x);
            var sorgu = db.KullaniciBilgileri.Where(u => u.Id == id).ToList().FirstOrDefault();
            txtAdSoyad.Text = sorgu.AdiSoyadi;
            txtAdres.Text = sorgu.Adres;
            txtEMail.Text = sorgu.EMail;
            txtKullanici.Text = sorgu.KullaniciAdi;
            txtSifre.Text = sorgu.Sifre;
            txtTelefon.Text=sorgu.TelefonNo;
            txtKullaniciId.Text=sorgu.Id.ToString();
            if (sorgu.Roles == "User")
            {
              
                cmbRole.SelectedText = "User";
            }
            else
            {
              
                cmbRole.SelectedText = "Admin";
            }
            
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var kullanici = Login.Loginname;
            var yetki = db.KullaniciBilgileri.Where(u => u.KullaniciAdi == kullanici).Select(u => u.Roles).FirstOrDefault();
            if (yetki != "User")
            {
                var a = gridView1.FocusedRowHandle;
                var x = gridView1.GetRowCellValue(a, "Id").ToString();
                var id = Convert.ToInt32(x);
                if (MessageBox.Show("Kaydı Silmek İstiyor Musunuz?", "Kullanıcı Bilgileri Silme Onayı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var kullaniciBilgileri = db.KullaniciBilgileri.Find(id);
                    db.KullaniciBilgileri.Remove(kullaniciBilgileri);
                    db.SaveChanges();
                }
                KullaniciKayit_Load(sender, e);
            }
            else
            {
                MessageBox.Show("İşlemi yapmaya yetkiniz yoktur!");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
