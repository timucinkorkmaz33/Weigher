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

using DevExpress.XtraReports.UI;
using Kantar_Islemleri.Model;
using System.Data.SqlClient;

namespace Kantar_Islemleri
{
    public partial class FrmHesapla : DevExpress.XtraEditors.XtraForm
    {
        private string veri;
        DoraKantar db = new DoraKantar();
        public FrmHesapla()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }


        private void FrmHesapla_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tumDataSet.StokBilgileri' table. You can move, or remove it, as needed.
            this.stokBilgileriTableAdapter1.Fill(this.tumDataSet.StokBilgileri);
            // TODO: This line of code loads data into the 'tumDataSet.AracBilgileri' table. You can move, or remove it, as needed.
            this.aracBilgileriTableAdapter1.Fill(this.tumDataSet.AracBilgileri);
            // TODO: This line of code loads data into the 'tumDataSet.CariBilgileri' table. You can move, or remove it, as needed.
            this.cariBilgileriTableAdapter1.Fill(this.tumDataSet.CariBilgileri);
            // TODO: This line of code loads data into the 'stokHareketBilgileriDataSet3.StokHareketBilgileri' table. You can move, or remove it, as needed.
            this.stokHareketBilgileriTableAdapter3.Fill(this.stokHareketBilgileriDataSet3.StokHareketBilgileri);

            txtSoforler1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //  txtplaka.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //  txtStok.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            AutoCompleteStringCollection DataCollection1 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection DataCollection2 = new AutoCompleteStringCollection();
            addSofor(DataCollection);
            addPlaka(DataCollection1);
            addStok(DataCollection2);
            txtSoforler1.AutoCompleteCustomSource = DataCollection;
            // txtplaka.AutoCompleteCustomSource = DataCollection1;
            // txtStok.AutoCompleteCustomSource = DataCollection2;

            var sonevrak = db.StokHareketBilgileri.OrderByDescending(u => u.Id).Select(u => u).FirstOrDefault();
            if (sonevrak != null)
            {
                txtEvrakno.Text = (sonevrak.Evrak_No + 1).ToString();
                txtEvrakseri.Text = sonevrak.Evrak_Seri;
                textirsaliye.Text = (Convert.ToInt32(sonevrak.IrsaliyeNo)+1).ToString();
            }
            txtTarih.Text = DateTime.Now.ToString("dd.MM.yyyy");
            txtEvrakseri.ReadOnly = true;
            txtEvrakno.ReadOnly = true;
            txtTarih.ReadOnly = true;
            btnFis.Enabled = false;
            btnIrsaliye.Enabled = false;


        }
        public void addSofor(AutoCompleteStringCollection col)
        {
            var sorgu = db.SoforBilgileri.Select(u => u).ToList();
            foreach (var item in sorgu)
                col.Add(item.IsimSoyisim);
        }
        public void addPlaka(AutoCompleteStringCollection col)
        {
            var sorgu = db.AracBilgileri.Select(u => u).ToList();
            foreach (var item in sorgu)
                col.Add(item.Plaka);
        }
        public void addStok(AutoCompleteStringCollection col)
        {
            var sorgu = db.StokBilgileri.Select(u => u).ToList();
            foreach (var item in sorgu)
                col.Add(item.Stok_Adi);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var stokkodu = db.StokBilgileri.Where(u => u.Stok_Adi == txtStok.Text).FirstOrDefault().Stok_Kodu;
            var carikodu = db.CariBilgileri.Where(u => u.Cari_Adi == txtCari.Text).FirstOrDefault().Cari_Kodu;
            var tartimdeger = digitalGauge2.Text.Substring(0, 5);
            var aracdara = db.AracBilgileri.Where(u => u.Plaka == txtplaka.Text).FirstOrDefault().Tartım1;
            if (serialPort1.IsOpen)
            {
                if (Convert.ToDouble(digitalGauge2.Text) >= 150)
                {

                    if (lblid.Text == null || lblid.Text == "")
                    {
                        StokHareketBilgileri stkBilgiler = new StokHareketBilgileri();
                        stkBilgiler.Sofor_Adi = txtSoforler1.Text;
                        stkBilgiler.Arac_Plaka = txtplaka.Text;
                        stkBilgiler.Stok_Adi = txtStok.Text;
                        stkBilgiler.Islem_Tarihi = DateTime.Now;
                        stkBilgiler.Stok_Miktar = (Convert.ToDouble(digitalGauge2.Text.Substring(0, 5)) - Convert.ToDouble(aracdara)).ToString();
                        stkBilgiler.Cari_Adi = txtCari.Text;
                        stkBilgiler.Cari_Kodu = carikodu;
                        stkBilgiler.Stok_Kodu = stokkodu;
                        stkBilgiler.Fiyat = txtFiyat.Text;
                        var sorgu = db.StokHareketBilgileri.OrderByDescending(u => u.Id).Select(u => u).FirstOrDefault();
                        if (sorgu == null)
                        {
                            stkBilgiler.Evrak_No = 1;
                        }
                        else
                        {
                            stkBilgiler.Evrak_No = sorgu.Evrak_No + 1;
                        }
                        stkBilgiler.Evrak_Seri = "KNTR";
                        stkBilgiler.IrsaliyeNo = textirsaliye.Text;
                        stkBilgiler.Aciklama1 = txtaciklama1.Text;
                        stkBilgiler.Aciklama2 = txtaciklama2.Text;
                        stkBilgiler.Aciklama3 = txtaciklama3.Text;
                        stkBilgiler.Aciklama4 = txtaciklama4.Text;
                        stkBilgiler.Aciklama5 = txtaciklama5.Text;
                        stkBilgiler.Aciklama6 = txtaciklama6.Text;
                        stkBilgiler.Aciklama7 = txtaciklama7.Text;
                        stkBilgiler.Aciklama8 = txtaciklama8.Text;
                        stkBilgiler.ToplamFiyat = Convert.ToDouble(txtFiyat.Text) * Convert.ToDouble(digitalGauge2.Text);
                        db.StokHareketBilgileri.Add(stkBilgiler);
                        db.SaveChanges();
                        var sonid = db.StokHareketBilgileri.OrderByDescending(u => u.Id).FirstOrDefault().Id;
                        rapor.LoadLayout(Application.StartupPath + @"\StokHareket.repx");
                        rapor.DataSource = db.StokHareketBilgileri.ToList().Where(x => x.Id == sonid);
                        //rapor.ShowPreview();
                        //  rapor.Print("Samsung SCX-4623 Series Class Driver");
                        //rapor.Print("Send To OneNote 2016");
                        var yazici1 = db.YaziciBilgileri.FirstOrDefault().Yazici1;
                        rapor.Print(yazici1);//varsayılan
                        rapor.LoadLayout(Application.StartupPath + @"\FisReport.repx");
                        rapor.DataSource = db.StokHareketBilgileri.ToList().Where(x => x.Id == sonid);
                        //rapor.ShowPreview();
                        //  rapor.Print("Samsung SCX-4623 Series Class Driver");
                        //rapor.Print("Send To OneNote 2016");
                        var yazici2 = db.YaziciBilgileri.FirstOrDefault().Yazici2;
                        rapor.Print(yazici2);//varsayılan
                    }
                    else
                    {
                        var id = Convert.ToInt32(lblid.Text);
                        StokHareketBilgileri stkBilgiler = db.StokHareketBilgileri.Where(u => u.Id == id).Select(u => u).FirstOrDefault();
                        stkBilgiler.Sofor_Adi = txtSoforler1.Text;
                        stkBilgiler.Arac_Plaka = txtplaka.Text;
                        stkBilgiler.Cari_Adi = txtCari.Text;
                        stkBilgiler.Cari_Kodu = carikodu;
                        stkBilgiler.Stok_Kodu = stokkodu;
                        stkBilgiler.Stok_Adi = txtStok.Text;
                        stkBilgiler.Fiyat = txtFiyat.Text;
                        stkBilgiler.ToplamFiyat = Convert.ToDouble(txtFiyat.Text) * Convert.ToDouble(digitalGauge2.Text);
                        //  stkBilgiler.Stok_Miktar = digitalMiktar.Text;
                        stkBilgiler.IrsaliyeNo = textirsaliye.Text;
                        stkBilgiler.Aciklama1 = txtaciklama1.Text;
                        stkBilgiler.Aciklama2 = txtaciklama2.Text;
                        stkBilgiler.Aciklama3 = txtaciklama3.Text;
                        stkBilgiler.Aciklama4 = txtaciklama4.Text;
                        stkBilgiler.Aciklama5 = txtaciklama5.Text;
                        stkBilgiler.Aciklama6 = txtaciklama6.Text;
                        stkBilgiler.Aciklama7 = txtaciklama7.Text;
                        stkBilgiler.Aciklama8 = txtaciklama8.Text;

                        db.SaveChanges();
                    }
                    MessageBox.Show("Kaydetme İşlemi Başarılı!");
                    Temizle();
                    FrmHesapla_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Değer 150den buyuk olmalı");
                }
            }
            else
            {
                MessageBox.Show("Lütfen Tartımı Başlatın");
            }
        }
        private void Temizle()
        {
            serialPort1.Close();
            txtplaka.Text = "";
            txtSoforler1.Text = "";
            txtStok.Text = "";
            lblid.Text = "";
            digitalGauge2.Text = "00000";
            txtTarih.Text = "";
            txtEvrakseri.Text = "";
            txtEvrakno.Text = "";
            txtaciklama1.Text = "";
            txtaciklama2.Text = "";
            txtaciklama3.Text = "";
            txtaciklama4.Text = "";
            txtaciklama5.Text = "";
            txtaciklama6.Text = "";
            txtaciklama7.Text = "";
            txtaciklama8.Text = "";
            txtEvrakno.Text = "";
            txtEvrakseri.Text = "";
            txtTarih.Text = "";
            txtCari.Text = "";
            txtFiyat.Text = "";

            txtStok.ReadOnly = false;
            simpleButton2.Enabled = true;

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
            digitalGauge2.Text = veri;



            //textBox2.Text = veri;
        }

        private void Btneditgrid_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var a = gridView1.FocusedRowHandle;
            var x = gridView1.GetRowCellValue(a, "Id").ToString();
            var id = Convert.ToInt32(x);
            var sorgu = db.StokHareketBilgileri.Where(u => u.Id == id).ToList().FirstOrDefault();
            txtplaka.Text = sorgu.Arac_Plaka;
            txtTarih.Text = sorgu.Islem_Tarihi.ToString();
            txtEvrakseri.Text = sorgu.Evrak_Seri;
            txtEvrakno.Text = sorgu.Evrak_No.ToString();
            txtaciklama1.Text = sorgu.Aciklama1;
            txtaciklama2.Text = sorgu.Aciklama2;
            txtaciklama3.Text = sorgu.Aciklama3;
            txtaciklama4.Text = sorgu.Aciklama4;
            txtaciklama5.Text = sorgu.Aciklama5;
            txtaciklama6.Text = sorgu.Aciklama6;
            txtaciklama7.Text = sorgu.Aciklama7;
            txtaciklama8.Text = sorgu.Aciklama8;
            txtSoforler1.Text = sorgu.Sofor_Adi;
            txtStok.EditValue = sorgu.Stok_Kodu;
            txtTarih.Text = sorgu.Islem_Tarihi.ToString();
            txtCari.Text = sorgu.Cari_Kodu;
            txtEvrakno.Text = sorgu.Evrak_No.ToString();
            txtEvrakseri.Text = sorgu.Evrak_Seri;
            txtFiyat.Text = sorgu.Fiyat;
            textirsaliye.Text = sorgu.IrsaliyeNo;
            lblid.Text = sorgu.Id.ToString();
            txtStok.ReadOnly = true;
            btnFis.Enabled = true;
            btnIrsaliye.Enabled = true;
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var kullanici = Login.Loginname;
            var yetki = db.KullaniciBilgileri.Where(u => u.KullaniciAdi == kullanici).Select(u => u.Roles).FirstOrDefault();
            if (yetki != "User")
            {
                var a = gridView1.FocusedRowHandle;
                var x = gridView1.GetRowCellValue(a, "Id").ToString();
                var id = Convert.ToInt32(x);
                if (MessageBox.Show("Kaydı Silmek İstiyor Musunuz?", "Hareket Bilgileri Silme Onayı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var hareketBilgileri = db.StokHareketBilgileri.Find(id);
                    db.StokHareketBilgileri.Remove(hareketBilgileri);
                    db.SaveChanges();
                }
                FrmHesapla_Load(sender, e);
            }
            else
            {
                MessageBox.Show("İşlemi yapmaya yetkiniz yoktur!");
            }
        }

        XtraReport rapor = new XtraReport();
        private void button1_Click(object sender, EventArgs e)
        {

            //using (Report report = new Report())
            //{
            //    report.Load(@"C:\Users\MiaSoft\documents\visual studio 2015\Projects\Kantar Islemleri\Kantar Islemleri\kantar.frx");//program içerisinde raporlar klasörü altındaki raporadi.frx raporunu gösterdik
            //    var a = DataSetGetir(1093);
            //    report.RegisterData(a, "NewDataSet");
            //    report.Show();
            //}
            rapor.LoadLayout(Application.StartupPath + @"\StokHareket.repx");
            rapor.DataSource = db.StokHareketBilgileri.ToList();
            rapor.ShowDesigner();

        }
        private DataSet DataSetGetir(int id)
        {
            var sorgu = db.StokHareketBilgileri.FirstOrDefault(u => u.Id == id);
            var resultDataSet = new DataSet();

            using (var valueTable = new DataTable("Arac_Plaka"))
            {
                valueTable.Columns.Add("Arac_Plaka");



                var ilkRow = valueTable.NewRow();
                ilkRow["Arac_Plaka"] = sorgu.Arac_Plaka;

                valueTable.Rows.Add(ilkRow);
                resultDataSet.Tables.Add(valueTable);
                return resultDataSet;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            rapor.LoadLayout(Application.StartupPath + @"\StokHareket.repx");
            rapor.DataSource = db.StokHareketBilgileri.ToList().Where(x => x.Id == 1093);
            //rapor.ShowPreview();
            //  rapor.Print("Samsung SCX-4623 Series Class Driver");
            //rapor.Print("Send To OneNote 2016");
            rapor.Print();//varsayılan
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Temizle();
            FrmHesapla_Load(sender, e);
        }

        private void btnFis_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(lblid.Text);
            rapor.LoadLayout(Application.StartupPath + @"\FisReport.repx");
            rapor.DataSource = db.StokHareketBilgileri.ToList().Where(x => x.Id == id);
            //rapor.ShowPreview();
            //  rapor.Print("Samsung SCX-4623 Series Class Driver");
            //rapor.Print("Send To OneNote 2016");
            var yazici2 = db.YaziciBilgileri.FirstOrDefault().Yazici2;
            rapor.Print(yazici2);//varsayılan
        }

        private void btnIrsaliye_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(lblid.Text);
            rapor.LoadLayout(Application.StartupPath + @"\StokHareket.repx");
            rapor.DataSource = db.StokHareketBilgileri.ToList().Where(x => x.Id == id);
            //rapor.ShowPreview();
            //  rapor.Print("Samsung SCX-4623 Series Class Driver");
            //rapor.Print("Send To OneNote 2016");
            var yazici1 = db.YaziciBilgileri.FirstOrDefault().Yazici1;
            rapor.Print(yazici1);//varsayılan
        }

        private void txtStok_EditValueChanged(object sender, EventArgs e)
        {
            if (txtStok.Text != "")
            {
                var sorgu = db.StokBilgileri.Where(u => u.Stok_Kodu == txtStok.EditValue).FirstOrDefault().Stok_Birim_Fiyat;
                txtFiyat.Text = sorgu;
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}