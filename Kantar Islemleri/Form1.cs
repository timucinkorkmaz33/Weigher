using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using Kantar_Islemleri.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kantar_Islemleri
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        DoraKantar db = new DoraKantar();
        public Form1()
        { 
            InitializeComponent();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Blue";
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FrmHesapla frmhesapla = new FrmHesapla();
            if (CheckMdiChild(frmhesapla) == true) return;
            frmhesapla.MdiParent = this;
            frmhesapla.Show();
            var yetki = db.KullaniciBilgileri.Where(u => u.KullaniciAdi == Login.Loginname).FirstOrDefault().Roles;
            if (yetki=="User")
            {
                ribbonPage3.Visible = false;
            }
            if (yetki != "AppAdmin")
            {
                ribbonPage4.Visible = false;
            }
        }
        private bool CheckMdiChild(Form frm)
        {
            if (ActiveMdiChild != null)
            {
                foreach (Form MdiChild in MdiChildren)
                {
                    if (MdiChild.Name.Equals(frm.Name))
                    {
                        MdiChild.Activate();
                        return true;
                    }
                }
            }
            return false;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmHesapla frmhesapla = new FrmHesapla();
            if (CheckMdiChild(frmhesapla) == true) return;
            frmhesapla.MdiParent = this;
            frmhesapla.Show();
            BCloseAllButThis_ItemClick(sender, e);
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmStok frmstk = new FrmStok();
            if (CheckMdiChild(frmstk) == true) return;
            frmstk.MdiParent = this;
            frmstk.Show();
            BCloseAllButThis_ItemClick(sender, e);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmCari frmcari = new FrmCari();
            if (CheckMdiChild(frmcari) == true) return;
            frmcari.MdiParent = this;
            frmcari.Show();
            BCloseAllButThis_ItemClick(sender, e);
        }
        private void AracBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmArac frmarac = new FrmArac();
            if (CheckMdiChild(frmarac) == true) return;
            frmarac.MdiParent = this;
            frmarac.Show();
            BCloseAllButThis_ItemClick(sender, e);
        }
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmSofor frmsofor = new FrmSofor();
            if (CheckMdiChild(frmsofor) == true) return;
            frmsofor.MdiParent = this;
            frmsofor.Show();
            BCloseAllButThis_ItemClick(sender, e);
        }
        BarManager Barman;
        PopupMenu Popmenu;
        BarButtonItem BClose;
        BarButtonItem BCloseAllButThis;
        private void xtraTabbedMdiManager1_MouseUp(object sender, MouseEventArgs e)
        {
            if (xtraTabbedMdiManager1.Pages.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (Barman == null)
                    {
                        Barman = new BarManager();
                        Barman.Form = this;
                    }
                    if (Popmenu == null)
                    {
                        Popmenu = new PopupMenu(Barman);
                    }
                    Popmenu.ItemLinks.Clear();
                    BClose = new BarButtonItem(Barman, "Kapat");
                    BCloseAllButThis = new BarButtonItem(Barman, "Diğer Sekmeleri Kapat");
                    Popmenu.AddItems(new BarButtonItem[] { BClose, BCloseAllButThis });
                    Popmenu.ShowPopup(Control.MousePosition);
                    BClose.ItemClick += new ItemClickEventHandler(BClose_ItemClick);
                    BCloseAllButThis.ItemClick += new ItemClickEventHandler(BCloseAllButThis_ItemClick);

                }
            }
        }
        private void BClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach(Form frm in MdiChildren)
            {
                if(frm!= null)
                {
                    if (this.ActiveMdiChild == frm)
                    {
                        frm.Close();
                        return;
                    }
                }
            }
        }

        private void BCloseAllButThis_ItemClick(object sender,ItemClickEventArgs e)
        {
            foreach(Form frm in MdiChildren)
            {
                if(frm!= null)
                {
                    if (this.ActiveMdiChild == frm)
                    {
                        continue;
                    }
                    frm.Close();
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            KullaniciKayit frmkul = new KullaniciKayit();
            if (CheckMdiChild(frmkul) == true) return;
            frmkul.MdiParent = this;
            frmkul.Show();
            BCloseAllButThis_ItemClick(sender, e);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmYazici frmyaz = new FrmYazici();
            if (CheckMdiChild(frmyaz) == true) return;
            frmyaz.MdiParent = this;
            frmyaz.Show();
            BCloseAllButThis_ItemClick(sender, e);
        }
        XtraReport rapor = new XtraReport();
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            rapor.LoadLayout(Application.StartupPath + @"\FisReport.repx");
            rapor.DataSource = db.StokHareketBilgileri.ToList();
            rapor.ShowDesigner();
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            rapor.LoadLayout(Application.StartupPath + @"\StokHareket.repx");
            rapor.DataSource = db.StokHareketBilgileri.ToList();
            rapor.ShowDesigner();
        }
    }
}
