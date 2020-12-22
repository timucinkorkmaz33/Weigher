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
    public partial class FrmStok : DevExpress.XtraEditors.XtraForm
    {
        DoraKantar db = new DoraKantar();
        public FrmStok()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (lblid.Text == null || lblid.Text == "")
            {
                StokBilgileri stk = new StokBilgileri();
                stk.Stok_Adi = txtStokAdi.Text;
                stk.Stok_Kodu = txtStokKodu.Text;
                stk.Stok_Birim = txtStokBirim.Text;
                stk.Stok_KisaAdi = txtKisaAdi.Text;
                stk.Stok_Birim_Fiyat = txtBirimfiyat.Text;
                stk.Stok_Tanim_Tarihi = Convert.ToDateTime(txtTarih.Text.ToString());
                db.StokBilgileri.Add(stk);
                db.SaveChanges();
            }
            else
            {
                var id =Convert.ToInt32(lblid.Text);
                StokBilgileri stk = db.StokBilgileri.Where(u => u.Id == id).Select(u => u).ToList().FirstOrDefault();
                stk.Stok_Adi = txtStokAdi.Text;
                stk.Stok_Kodu = txtStokKodu.Text;
                stk.Stok_Birim = txtStokBirim.Text;
                stk.Stok_KisaAdi = txtKisaAdi.Text;
                stk.Stok_Birim_Fiyat = txtBirimfiyat.Text;
                //stk.Stok_Tanim_Tarihi = Convert.ToDateTime(txtTarih.Text);

                db.SaveChanges();
            }
            MessageBox.Show("Stok Kayıt İşlemi Başarılı");
            FrmStok_Load(sender, e);
            Temizle();
        }
        private void Temizle()
        {
            txtTarih.ReadOnly = false;
            txtStokAdi.Text = "";
            txtStokKodu.Text = "";
            txtStokBirim.Text = "";
            txtKisaAdi.Text = "";
            txtTarih.Text = "";
            lblid.Text = "";
            txtBirimfiyat.Text = "";
        }

        private void FrmStok_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stokBilgileriDataset.StokBilgileri' table. You can move, or remove it, as needed.
            this.stokBilgileriTableAdapter2.Fill(this.stokBilgileriDataset.StokBilgileri);
            // TODO: This line of code loads data into the 'doraKantarDataSet1.StokBilgileri' table. You can move, or remove it, as needed.
            this.stokBilgileriTableAdapter1.Fill(this.doraKantarDataSet1.StokBilgileri);

            this.stokBilgileriTableAdapter.Fill(this.doraKantarDataSet.StokBilgileri);
            // TODO: This line of code loads data into the 'doraKantarDataSet.StokBilgileri' table. You can move, or remove it, as needed.
            txtTarih.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var a = gridView1.FocusedRowHandle;
            var x = gridView1.GetRowCellValue(a, "Id").ToString();
            var id = Convert.ToInt32(x);
            var sorgu = db.StokBilgileri.Where(u => u.Id == id).ToList().FirstOrDefault();
            txtKisaAdi.Text = sorgu.Stok_KisaAdi;
            txtStokAdi.Text = sorgu.Stok_Adi;
            txtStokBirim.Text = sorgu.Stok_Birim;
            txtStokKodu.Text = sorgu.Stok_Kodu;
            txtTarih.Text = sorgu.Stok_Tanim_Tarihi.ToString();
            txtTarih.ReadOnly = true;
            txtBirimfiyat.Text = sorgu.Stok_Birim_Fiyat;
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
                if (MessageBox.Show("Kaydı Silmek İstiyor Musunuz?", "Stok Bilgileri Silme Onayı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var stokBilgileri = db.StokBilgileri.Find(id);
                    db.StokBilgileri.Remove(stokBilgileri);
                    db.SaveChanges();
                }
                FrmStok_Load(sender, e);
            }else
            {
                MessageBox.Show("İşlemi yapmaya yetkiniz yoktur!");
            }
        }
    }
}