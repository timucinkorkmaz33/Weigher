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
    public partial class FrmYazici : DevExpress.XtraEditors.XtraForm
    {
        DoraKantar db = new DoraKantar();
        public FrmYazici()
        {
            InitializeComponent();
        }

        private void FrmYazici_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'doraKantarDataSet1.YaziciBilgileri' table. You can move, or remove it, as needed.
            this.yaziciBilgileriTableAdapter.Fill(this.doraKantarDataSet1.YaziciBilgileri);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {if (txtId.Text == null || txtId.Text == "")
            {

                YaziciBilgileri yz = new YaziciBilgileri();
                yz.Yazici1 = txtYazici1.Text;
                yz.Yazici2 = txtYazici2.Text;
                db.YaziciBilgileri.Add(yz);
                db.SaveChanges();
            }
        else
            {
                var id = Convert.ToInt32(txtId.Text);
                YaziciBilgileri yz = db.YaziciBilgileri.Where(u => u.Id == id).FirstOrDefault();
                yz.Yazici1 = txtYazici1.Text;
                yz.Yazici2 = txtYazici2.Text;
               
                db.SaveChanges();
            }
            MessageBox.Show("Kaydetme İşlemi Başarılı!");
            FrmYazici_Load(sender, e);
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var a = gridView1.FocusedRowHandle;
            var x = gridView1.GetRowCellValue(a, "Id").ToString();
            var id = Convert.ToInt32(x);
            var sorgu = db.YaziciBilgileri.Where(u => u.Id == id).ToList().FirstOrDefault();
            txtYazici1.Text = sorgu.Yazici1;
            txtYazici2.Text = sorgu.Yazici2;
            txtId.Text = sorgu.Id.ToString();
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
                if (MessageBox.Show("Kaydı Silmek İstiyor Musunuz?", "Yazıcı Silme Onayı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var yazici = db.YaziciBilgileri.Find(id);
                    db.YaziciBilgileri.Remove(yazici);
                    db.SaveChanges();
                }
                FrmYazici_Load(sender, e);
            }
            else
            {
                MessageBox.Show("İşlemi yapmaya yetkiniz yoktur!");
            }
        }

    
    }
}
