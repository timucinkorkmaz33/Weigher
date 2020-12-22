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
using System.IO;
using System.IO.Ports;
using System.Threading;
using Kantar_Islemleri.Model;

namespace Kantar_Islemleri
{
    public partial class FrmArac : DevExpress.XtraEditors.XtraForm
    {
        DoraKantar db = new DoraKantar();
        string veri;
        public FrmArac()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtPlaka.Text !="" && serialPort1.IsOpen == true && txtAracMarka.Text!="")
            {
                if (lblid.Text == null || lblid.Text == "")
                {
                    AracBilgileri arc = new AracBilgileri();
                    arc.AracMarka = txtAracMarka.Text;
                    arc.Plaka = txtPlaka.Text;
                    arc.DorsePlaka = txtDorsePlaka.Text;
                    arc.Tartım1 = Convert.ToInt32(digittartim1.Text.Substring(0, 5));
                    arc.AracModeli = txtAracModeli.Text;
                    db.AracBilgileri.Add(arc);
                    db.SaveChanges();
                }
                else
                {
                    var id = Convert.ToInt32(lblid.Text);
                    AracBilgileri arc = db.AracBilgileri.Where(u => u.Id == id).Select(u => u).ToList().FirstOrDefault();
                    arc.AracMarka = txtAracMarka.Text;
                    arc.Plaka = txtPlaka.Text;
                    arc.DorsePlaka = txtDorsePlaka.Text;
                    arc.Tartım1 = Convert.ToInt32(digittartim1.Text.Substring(0, 5));
                    arc.AracModeli = txtAracModeli.Text;
                    db.SaveChanges();
                }
                MessageBox.Show("Araç Kaydı Başarılı");
                FrmArac_Load(sender, e);
                Temizle();
            }
            else
            {
                MessageBox.Show("Tartımı Başlatın ve Gerekli Alanları doldurunuz");
            }
        }
        private void Temizle()
        {
            serialPort1.Close();
            txtAracMarka.Text = "";
            txtAracModeli.Text = "";
            digittartim1.Text = "00000";
            txtDorsePlaka.Text = "";
            txtPlaka.Text = "";
            lblid.Text = "";
            simpleButton2.Enabled = true;
        }

        private void FrmArac_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'doraKantarDataSet.AracBilgileri' table. You can move, or remove it, as needed.
            this.aracBilgileriTableAdapter.Fill(this.doraKantarDataSet.AracBilgileri);


        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = "COM2";
                serialPort1.BaudRate = 4800;
                serialPort1.DataBits = 8;
                serialPort1.Handshake = Handshake.None;
                serialPort1.Open();
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            }
            simpleButton2.Enabled = false;
        }
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                veri = serialPort1.ReadExisting();

                this.Invoke(new EventHandler(DisplayText));
                Thread.Sleep(2000);
            }
        }
        private void DisplayText(object sender, EventArgs e)
        {
            digittartim1.Text = veri;
        
            simpleButton2.Enabled = false;
            

            //textBox2.Text = veri;
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var a = gridView1.FocusedRowHandle;
            var x = gridView1.GetRowCellValue(a, "Id").ToString();
            var id = Convert.ToInt32(x);
            var sorgu = db.AracBilgileri.Where(u => u.Id == id).ToList().FirstOrDefault();
            txtAracMarka.Text = sorgu.AracMarka;
            txtAracModeli.Text = sorgu.AracModeli;
            txtDorsePlaka.Text = sorgu.DorsePlaka;
            txtPlaka.Text = sorgu.Plaka;
            digittartim1.Text = sorgu.Tartım1.ToString();
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
                if (MessageBox.Show("Kaydı Silmek İstiyor Musunuz?", "Araç Silme Onayı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var aracBilgileri = db.AracBilgileri.Find(id);
                    db.AracBilgileri.Remove(aracBilgileri);
                    db.SaveChanges();
                }
                FrmArac_Load(sender, e);
            }
            else
            {
                MessageBox.Show("İşlemi yapmaya yetkiniz yoktur!");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}