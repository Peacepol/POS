using RestaurantPOS;
using RestaurantPOS.Inventory;
using RestaurantPOS.Purchase;
using RestaurantPOS.References;
using RestaurantPOS.Reports;
using RestaurantPOS.Reports.PurchaseReports;
using RestaurantPOS.Reports.SalesReports;
using RestaurantPOS.Sales;
using RestaurantPOS.Setup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using RestaurantPOS.Reports.SalesReports;

namespace RestaurantPOS
{
    public partial class MainPage : Form
    {
        private int childFormNumber = 0;
        private CoProForm CoPro = null;
        DateTime sDate;
        private int TerminalID = 0;

        public MainPage()
        {
            InitializeComponent();

        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            toolStrip.AutoSize = false; toolStrip.Height = 50;
            toolStrip.ImageScalingSize = new Size(40, 40);
            toolStrip.Enabled = false;
            menuStrip.Enabled = false;

            Hide();
            CoPro = new CoProForm(this);
            CoPro.ShowDialog();
            if (CommonClass.ConStr != "" && !isReminder())
            {
                Reminder();
            }

            //force to enable purchase and enter sales
            enterSalesToolStripMenuItem.Enabled = false;
            receivePaymentToolStripMenuItem.Enabled = false;
            btnPurchase.Enabled = true;
            toolStripUser.Text = "User: " + CommonClass.UserName + ",";
            toolStripCompanyName.Text = "Company Name: " + CommonClass.LoggedInCompany + ",";
            toolStripDatabaseName.Text = "Database: " + CommonClass.LoggedInDbName + ",";
            toolStripServerName.Text = "Server: " + CommonClass.LoggedInServerName;
            tsCurrentYear.Text = "Current Year: " + CommonClass.CurFY;
        }

        private bool CheckMenuItemRights(ToolStripMenuItem item)
        {

            if (item.DropDown.Items.Count == 0)
            {
                Dictionary<string, Boolean> lDic;
                if (CommonClass.UserAccess.TryGetValue(item.Text, out lDic))
                {
                    Boolean valstr = false;
                    if (lDic.TryGetValue("View", out valstr))
                    {
                        if (valstr == true)
                        {
                            item.Enabled = true;
                        }
                        else
                        {
                            item.Enabled = false;
                        }
                    }
                }
                else
                {
                    item.Enabled = false;
                }
            }
            else
            {
                foreach (Object recurseditem in item.DropDown.Items)
                {
                    if (recurseditem.GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                        CheckMenuItemRights((ToolStripMenuItem)recurseditem);
                }
            }

            return item.Enabled;
        }

        public void ApplyAccess(String pUID)
        {
            bool lMenuEnabled = false;
            bool EnableMenu = false;
            CommonClass.GetAccess(pUID);
            toolStripUser.Text = "User: " + CommonClass.UserName + ",";
            toolStripCompanyName.Text = "Company Name: " + CommonClass.LoggedInCompany + ",";
            toolStripDatabaseName.Text = "Database: " + CommonClass.LoggedInDbName + ",";
            toolStripServerName.Text = "Server: " + CommonClass.LoggedInServerName;
            tsCurrentYear.Text = "Current Year: " + CommonClass.CurFY;


            //REFERENCES
            EnableMenu = false;
            foreach (ToolStripMenuItem item in References.DropDown.Items)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                {
                    lMenuEnabled = CheckMenuItemRights(item);
                    if (lMenuEnabled)
                    {
                        EnableMenu = true;
                    }
                }

            }
            References.Enabled = EnableMenu;

            //REPORTS
            EnableMenu = false;
            foreach (ToolStripMenuItem item in Reports.DropDown.Items)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                {
                    lMenuEnabled = CheckMenuItemRights(item);
                    if (lMenuEnabled)
                    {
                        EnableMenu = true;
                    }
                }
            }
            Reports.Enabled = EnableMenu;

            //Utilities
            EnableMenu = false;
            foreach (ToolStripMenuItem item in utilitiesToolStripMenuItem.DropDown.Items)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                {
                    lMenuEnabled = CheckMenuItemRights(item);
                    if (lMenuEnabled)
                    {
                        EnableMenu = true;
                    }
                }
            }
            utilitiesToolStripMenuItem.Enabled = EnableMenu;

            //SETUP
            EnableMenu = false;
            foreach (ToolStripMenuItem item in Setup.DropDown.Items)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                {
                    lMenuEnabled = CheckMenuItemRights(item);
                    if (lMenuEnabled)
                    {
                        EnableMenu = true;
                    }
                }
            }
            Setup.Enabled = EnableMenu;

            //SALES
            EnableMenu = false;
            foreach (ToolStripMenuItem item in Sales.DropDown.Items)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                {
                    lMenuEnabled = CheckMenuItemRights(item);
                    if (lMenuEnabled)
                    {
                        EnableMenu = true;
                    }
                }
            }
            Sales.Enabled = EnableMenu;

            //PURCHASE
            EnableMenu = false;
            foreach (ToolStripMenuItem item in purchaseToolStripMenuItem.DropDown.Items)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                {
                    lMenuEnabled = CheckMenuItemRights(item);
                    if (lMenuEnabled)
                    {
                        EnableMenu = true;
                    }
                }
            }
            purchaseToolStripMenuItem.Enabled = EnableMenu;

            //INVENTORY
            EnableMenu = false;
            foreach (ToolStripMenuItem item in inventoryToolStripMenuItem.DropDown.Items)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.ToolStripMenuItem")
                {
                    lMenuEnabled = CheckMenuItemRights(item);
                    if (lMenuEnabled)
                    {
                        EnableMenu = true;
                    }
                }

            }
            inventoryToolStripMenuItem.Enabled = EnableMenu;

            menuStrip.Enabled = true;

            btnSession.Visible = true;
            //enterSalesToolStripMenuItem.Enabled = false;
            //receivePaymentToolStripMenuItem.Enabled = false;

            //if (CommonClass.isSalesperson == true)
            //{
            //    btnPurchase.Visible  = false;
            //    btnInventory.Visible = false;
            //    btnReports.Visible = false;
            //    btnCustomer.Visible = false;
            //    btnSupplier.Visible = false;
            //    purchaseToolStripMenuItem.Visible = false;
            //    Reports.Visible = false;
            //    userMaintenanceToolStripMenuItem.Visible = false;
            //    //inventoryToolStripMenuItem.Visible = false;
            //    systemSettingsToolStripMenuItem.Visible = false;
            //    enterSalesToolStripMenuItem.Enabled = false;
            //    receivePaymentToolStripMenuItem.Enabled = false;
            //    btnSetup.Visible = false;
            //}else
            //{
            //    btnPurchase.Visible = true;
            //    btnInventory.Visible = true;
            //    btnReports.Visible = true;
            //    btnCustomer.Visible = true;
            //    btnSupplier.Visible = true;
            //    purchaseToolStripMenuItem.Visible = true;
            //    Reports.Visible = true;
            //    userMaintenanceToolStripMenuItem.Visible = true;
            //    inventoryToolStripMenuItem.Visible = true;
            //    systemSettingsToolStripMenuItem.Visible = true;
            //    enterSalesToolStripMenuItem.Enabled = true;
            //    receivePaymentToolStripMenuItem.Enabled = true;
            //    btnSetup.Visible = true;
            //}

            btnPurchase.Visible = purchaseToolStripMenuItem.Enabled;
            btnInventory.Visible = inventoryToolStripMenuItem.Enabled;
            btnReports.Visible = Reports.Enabled;
            btnCustomer.Visible = true;
            btnSupplier.Visible = true;
            btnSetup.Visible = Setup.Enabled;
        }

        private void buttonCompany_Click(object sender, EventArgs e)
        {
            if (CommonClass.DataInfoFrm == null || CommonClass.DataInfoFrm.IsDisposed)
            {
                CommonClass.DataInfoFrm = new DataInformation();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.DataInfoFrm.MdiParent = this;
            CommonClass.DataInfoFrm.Show();
            CommonClass.DataInfoFrm.Focus();
            if (CommonClass.DataInfoFrm.DialogResult == DialogResult.Cancel
                || CommonClass.DataInfoFrm.DialogResult == DialogResult.OK)
            {
                CommonClass.DataInfoFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void jobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.JobFrm == null || CommonClass.JobFrm.IsDisposed)
            {
                CommonClass.JobFrm = new Job();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.JobFrm.MdiParent = this;
            CommonClass.JobFrm.Show();
            CommonClass.JobFrm.Focus();
            if (CommonClass.JobFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.JobFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void taxCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.TaxCodeFrm == null || CommonClass.TaxCodeFrm.IsDisposed)
            {
                CommonClass.TaxCodeFrm = new CreateTaxCode();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.TaxCodeFrm.MdiParent = this;
            CommonClass.TaxCodeFrm.Show();
            CommonClass.TaxCodeFrm.Focus();
            if (CommonClass.TaxCodeFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.TaxCodeFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void currencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.CurrencyFrm == null || CommonClass.CurrencyFrm.IsDisposed)
            {
                CommonClass.CurrencyFrm = new Currency();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CurrencyFrm.MdiParent = this;
            CommonClass.CurrencyFrm.Show();
            CommonClass.CurrencyFrm.Focus();
            if (CommonClass.CurrencyFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CurrencyFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void paymentMethodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PaymentMethodFrm == null || CommonClass.PaymentMethodFrm.IsDisposed)
            {
                CommonClass.PaymentMethodFrm = new CreatePaymentMethod();
            }

            this.Cursor = Cursors.WaitCursor;
            CommonClass.PaymentMethodFrm.MdiParent = this;
            CommonClass.PaymentMethodFrm.Show();
            CommonClass.PaymentMethodFrm.Focus();
            if (CommonClass.PaymentMethodFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PaymentMethodFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void shippingMethodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ShippingMethodFrm == null || CommonClass.ShippingMethodFrm.IsDisposed)
            {
                CommonClass.ShippingMethodFrm = new CreateShippingMethod();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ShippingMethodFrm.MdiParent = this;
            CommonClass.ShippingMethodFrm.Show();
            CommonClass.ShippingMethodFrm.Focus();
            if (CommonClass.ShippingMethodFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ShippingMethodFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.CustomerListFrm == null || CommonClass.CustomerListFrm.IsDisposed)
            {
                CommonClass.CustomerListFrm = new CustomerList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomerListFrm.MdiParent = this;
            CommonClass.CustomerListFrm.Show();
            CommonClass.CustomerListFrm.Focus();
            if (CommonClass.CustomerListFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomerListFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void userMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (CommonClass.UserMaintenanceFrm == null || CommonClass.UserMaintenanceFrm.IsDisposed)
            {
                CommonClass.UserMaintenanceFrm = new UserMaintenance();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.UserMaintenanceFrm.MdiParent = this;
            CommonClass.UserMaintenanceFrm.Show();
            CommonClass.UserMaintenanceFrm.Focus();
            if (CommonClass.UserMaintenanceFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.UserMaintenanceFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SupplierListFrm == null || CommonClass.SupplierListFrm.IsDisposed)
            {
                CommonClass.SupplierListFrm = new SupplierList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SupplierListFrm.MdiParent = this;
            CommonClass.SupplierListFrm.Show();
            CommonClass.SupplierListFrm.Focus();
            if (CommonClass.SupplierListFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SupplierListFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void reportsNavigationPaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptPanelFrm == null || CommonClass.RptPanelFrm.IsDisposed)
            {
                CommonClass.RptPanelFrm = new ReportsNavigation();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptPanelFrm.MdiParent = this;
            CommonClass.RptPanelFrm.Show();
            CommonClass.RptPanelFrm.Focus();
            if (CommonClass.RptPanelFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptPanelFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void tsmBackupDatabase_Click(object sender, EventArgs e)
        {
            if (saveFileDialogBackup.ShowDialog() == DialogResult.OK)
            {
                if (CommonClass.CreateDbBackup(saveFileDialogBackup.FileName))
                {
                    MessageBox.Show("The database has been backed up in: " + saveFileDialogBackup.FileName);
                }
                else
                {
                    MessageBox.Show("Database backup failed");
                }
            }
        }

        private void activateSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Activate")
                {
                    Isopen = true;
                    f.BringToFront();
                    break;
                }
            }

            if (Isopen == false)
            {
                Utilities.Activate frmActivate = new Utilities.Activate("Activate");
                frmActivate.MdiParent = MainPage.ActiveForm;
                frmActivate.Show();
            }
        }

        private void activateOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string uuid = CommonClass.base64Encode(CommonClass.LoggedInCompany + "," + CommonClass.LoggedInRegNo + "," + CommonClass.LoggedInSerialNo);
            string sourcePath = @Application.StartupPath;

            System.IO.StreamReader file = null;
            try
            {
                file = new System.IO.StreamReader(sourcePath + "\\api.json");
                string apijson = file.ReadToEnd();
                apijson = apijson.Replace("\t", "");

                ApiElements compinfo = JsonConvert.DeserializeObject<ApiElements>(apijson);

                string url = compinfo.apiprotocol + "://" + compinfo.apihost + ":" + compinfo.apiport + "/" + compinfo.apimethod + "/" + uuid;

                try
                {
                    WebRequest request = HttpWebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responsestr = reader.ReadToEnd();
                    responsestr = responsestr.Replace("\\t", "");
                    responsestr = responsestr.Replace("\\r", "");
                    responsestr = responsestr.Replace("\\n", "");
                    responsestr = responsestr.Replace("\\", "");
                    responsestr = responsestr.Replace("\"{", "{");
                    responsestr = responsestr.Replace("}\"", "}");

                    ActivateResult result = JsonConvert.DeserializeObject<ActivateResult>(responsestr);
                    if (result != null
                        && result.IsActivated)
                    {
                        string updatesql = "UPDATE DataFileInformation SET IsActive = 1, ActivationDate = GETUTCDATE()";

                        int rowsaffected = CommonClass.runSql(updatesql);
                        if (rowsaffected > 0)
                        {
                            MessageBox.Show("Your software is now activated. Thank you!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failure to activate the software");
                    }
                }
                catch (WebException wex)
                {
                    throw wex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "About")
                {
                    Isopen = true;
                    f.BringToFront();
                    break;
                }
            }

            if (Isopen == false)
            {
                About frmAbout = new About();
                frmAbout.MdiParent = MainPage.ActiveForm;
                frmAbout.Show();
            }
        }

        public void reactivateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Activate")
                {
                    f.Close();
                    break;
                }
            }

            Utilities.Activate frmActivate = new Utilities.Activate("Reactivate");
            frmActivate.MdiParent = MainPage.ActiveForm;
            frmActivate.Show();
        }

        private void dataInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.DataInfoFrm == null || CommonClass.DataInfoFrm.IsDisposed)
            {
                CommonClass.DataInfoFrm = new DataInformation();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.DataInfoFrm.MdiParent = this;
            CommonClass.DataInfoFrm.Show();
            CommonClass.DataInfoFrm.Focus();
            if (CommonClass.DataInfoFrm.DialogResult == DialogResult.Cancel
                || CommonClass.DataInfoFrm.DialogResult == DialogResult.OK)
            {
                CommonClass.DataInfoFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void taxTransactionsSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptTaxTransactionsSFrm == null || CommonClass.RptTaxTransactionsSFrm.IsDisposed)
            {
                CommonClass.RptTaxTransactionsSFrm = new RptTaxTransactionsS();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptTaxTransactionsSFrm.MdiParent = this;
            CommonClass.RptTaxTransactionsSFrm.Show();
            CommonClass.RptTaxTransactionsSFrm.Focus();
            if (CommonClass.RptTaxTransactionsSFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptTaxTransactionsSFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void taxTransactionsDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptTaxTransactionsDFrm == null || CommonClass.RptTaxTransactionsDFrm.IsDisposed)
            {
                CommonClass.RptTaxTransactionsDFrm = new RptTaxTransactionsD();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptTaxTransactionsDFrm.MdiParent = this;
            CommonClass.RptTaxTransactionsDFrm.Show();
            CommonClass.RptTaxTransactionsDFrm.Focus();
            if (CommonClass.RptTaxTransactionsDFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptTaxTransactionsDFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void jobBalancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.JobBalancesFrm == null || CommonClass.JobBalancesFrm.IsDisposed)
            {
                CommonClass.JobBalancesFrm = new JobBalances();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.JobBalancesFrm.MdiParent = this;
            CommonClass.JobBalancesFrm.Show();
            CommonClass.JobBalancesFrm.Focus();
            if (CommonClass.JobBalancesFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.JobBalancesFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnRef_Click(object sender, EventArgs e)
        {
            if (CommonClass.RefPanelFrm == null || CommonClass.RefPanelFrm.IsDisposed)
            {
                CommonClass.RefPanelFrm = new ReferencesNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RefPanelFrm.MdiParent = this;
            CommonClass.RefPanelFrm.Show();
            CommonClass.RefPanelFrm.Focus();
            if (CommonClass.RefPanelFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RefPanelFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void systemSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (CommonClass.PreferencesFrm == null || CommonClass.PreferencesFrm.IsDisposed)
            {
                CommonClass.PreferencesFrm = new Preferences();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PreferencesFrm.MdiParent = this;
            CommonClass.PreferencesFrm.Show();
            CommonClass.PreferencesFrm.Focus();
            if (CommonClass.PreferencesFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PreferencesFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void jobTransactionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptJobTransactionsOptionsFrm == null || CommonClass.RptJobTransactionsOptionsFrm.IsDisposed)
            {
                CommonClass.RptJobTransactionsOptionsFrm = new Reports.RptJobTransactionsOptions();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptJobTransactionsOptionsFrm.MdiParent = this;
            CommonClass.RptJobTransactionsOptionsFrm.Show();
            CommonClass.RptJobTransactionsOptionsFrm.Focus();
            if (CommonClass.RptJobTransactionsOptionsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptJobTransactionsOptionsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void jobActivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptActivityOptionsFrm == null || CommonClass.RptActivityOptionsFrm.IsDisposed)
            {
                CommonClass.RptActivityOptionsFrm = new Reports.RptActivityOptions();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptActivityOptionsFrm.MdiParent = this;
            CommonClass.RptActivityOptionsFrm.Show();
            CommonClass.RptActivityOptionsFrm.Focus();
            if (CommonClass.RptActivityOptionsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptActivityOptionsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void jobProfitAndLossToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptJobProfitAndLossFrm == null || CommonClass.RptJobProfitAndLossFrm.IsDisposed)
            {
                CommonClass.RptJobProfitAndLossFrm = new RptJobProfitAndLoss();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptJobProfitAndLossFrm.MdiParent = this;
            CommonClass.RptJobProfitAndLossFrm.Show();
            CommonClass.RptJobProfitAndLossFrm.Focus();
            if (CommonClass.RptJobProfitAndLossFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptJobProfitAndLossFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void customerOrSupplierListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptProfileListOptionsFrm == null || CommonClass.RptProfileListOptionsFrm.IsDisposed)
            {
                CommonClass.RptProfileListOptionsFrm = new Reports.RptProfileListOptions();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptProfileListOptionsFrm.MdiParent = this;
            CommonClass.RptProfileListOptionsFrm.Show();
            CommonClass.RptProfileListOptionsFrm.Focus();
            if (CommonClass.RptProfileListOptionsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptProfileListOptionsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void jobListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptJobListFrm == null || CommonClass.RptJobListFrm.IsDisposed)
            {
                CommonClass.RptJobListFrm = new RptJobList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptJobListFrm.MdiParent = this;
            CommonClass.RptJobListFrm.Show();
            CommonClass.RptJobListFrm.Focus();
            if (CommonClass.RptJobListFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptJobListFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            CoPro.Show();
            //LogOutStatus();
        }

        public void LogOutStatus()
        {
            DateTime dtNow = DateTime.Now;
            string updateLoginStatus = @"UPDATE Users SET IsLoggedIn = 0, EndSession = @EndSession WHERE user_id = " + CommonClass.UserID;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@EndSession", dtNow);
            CommonClass.runSql(updateLoginStatus, CommonClass.RunSqlInsertMode.QUERY, param);
        }

        private void Sales_Btn_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesPanelform == null || CommonClass.SalesPanelform.IsDisposed)
            {
                CommonClass.SalesPanelform = new SalesNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesPanelform.MdiParent = this;
            CommonClass.SalesPanelform.Show();
            CommonClass.SalesPanelform.Focus();
            if (CommonClass.SalesPanelform.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesPanelform.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void salesNavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesPanelform == null || CommonClass.SalesPanelform.IsDisposed)
            {
                CommonClass.SalesPanelform = new SalesNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesPanelform.MdiParent = this;
            CommonClass.SalesPanelform.Show();
            CommonClass.SalesPanelform.Focus();
            if (CommonClass.SalesPanelform.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesPanelform.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void salesRegisterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesRegfrm == null || CommonClass.SalesRegfrm.IsDisposed)
            {
                CommonClass.SalesRegfrm = new SalesRegister();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesRegfrm.MdiParent = this;
            CommonClass.SalesRegfrm.Show();
            CommonClass.SalesRegfrm.Focus();
            if (CommonClass.SalesRegfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesRegfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void enterSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.EnterSalesfrm == null || CommonClass.EnterSalesfrm.IsDisposed)
            {
                CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.EnterSalesfrm.MdiParent = this;
            CommonClass.EnterSalesfrm.Show();
            CommonClass.EnterSalesfrm.Focus();
            if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.EnterSalesfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void receivePaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SRPaymentsfrm == null || CommonClass.SRPaymentsfrm.IsDisposed)
            {
                CommonClass.SRPaymentsfrm = new SalesReceivePayment(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SRPaymentsfrm.MdiParent = this;
            CommonClass.SRPaymentsfrm.Show();
            CommonClass.SRPaymentsfrm.Focus();
            if (CommonClass.SRPaymentsfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SRPaymentsfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            if (CommonClass.PurchasePanelform == null || CommonClass.PurchasePanelform.IsDisposed)
            {
                CommonClass.PurchasePanelform = new PurchaseNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PurchasePanelform.MdiParent = this;
            CommonClass.PurchasePanelform.Show();
            CommonClass.PurchasePanelform.Focus();
            if (CommonClass.PurchasePanelform.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PurchasePanelform.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void purchaseNavigationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PurchasePanelform == null || CommonClass.PurchasePanelform.IsDisposed)
            {
                CommonClass.PurchasePanelform = new PurchaseNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PurchasePanelform.MdiParent = this;
            CommonClass.PurchasePanelform.Show();
            CommonClass.PurchasePanelform.Focus();
            if (CommonClass.PurchasePanelform.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PurchasePanelform.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void purchaseRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PurchaseRegfrm == null || CommonClass.PurchaseRegfrm.IsDisposed)
            {
                CommonClass.PurchaseRegfrm = new PurchaseRegister();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PurchaseRegfrm.MdiParent = this;
            CommonClass.PurchaseRegfrm.Show();
            CommonClass.PurchaseRegfrm.Focus();
            if (CommonClass.PurchaseRegfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PurchaseRegfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void enterPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.EnterPurchasefrm == null || CommonClass.EnterPurchasefrm.IsDisposed)
            {
                CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.EnterPurchasefrm.MdiParent = this;
            CommonClass.EnterPurchasefrm.Show();
            CommonClass.EnterPurchasefrm.Focus();
            if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.EnterPurchasefrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void recievePaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PRPaymentsfrm == null || CommonClass.PRPaymentsfrm.IsDisposed)
            {
                CommonClass.PRPaymentsfrm = new PurchasePayments(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PRPaymentsfrm.MdiParent = this;
            CommonClass.PRPaymentsfrm.Show();
            CommonClass.PRPaymentsfrm.Focus();
            if (CommonClass.PRPaymentsfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PRPaymentsfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void aRBalancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ARBalancesFrm == null || CommonClass.ARBalancesFrm.IsDisposed)
            {
                CommonClass.ARBalancesFrm = new ARBalances();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ARBalancesFrm.MdiParent = this;
            CommonClass.ARBalancesFrm.Show();
            CommonClass.ARBalancesFrm.Focus();
            if (CommonClass.ARBalancesFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ARBalancesFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void aPBalancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.APBalancesFrm == null || CommonClass.APBalancesFrm.IsDisposed)
            {
                CommonClass.APBalancesFrm = new APBalances();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.APBalancesFrm.MdiParent = this;
            CommonClass.APBalancesFrm.Show();
            CommonClass.APBalancesFrm.Focus();
            if (CommonClass.APBalancesFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.APBalancesFrm.Close();
            }
            this.Cursor = Cursors.Default;

        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (CommonClass.InventoryPanelfrm == null || CommonClass.InventoryPanelfrm.IsDisposed)
            {
                CommonClass.InventoryPanelfrm = new InventoryNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.InventoryPanelfrm.MdiParent = this;
            CommonClass.InventoryPanelfrm.Show();
            CommonClass.InventoryPanelfrm.Focus();
            if (CommonClass.InventoryPanelfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.InventoryPanelfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void inventoryNavigationPaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.InventoryPanelfrm == null || CommonClass.InventoryPanelfrm.IsDisposed)
            {
                CommonClass.InventoryPanelfrm = new InventoryNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.InventoryPanelfrm.MdiParent = this;
            CommonClass.InventoryPanelfrm.Show();
            CommonClass.InventoryPanelfrm.Focus();
            if (CommonClass.InventoryPanelfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.InventoryPanelfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemListfrm == null || CommonClass.ItemListfrm.IsDisposed)
            {
                CommonClass.ItemListfrm = new ItemList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemListfrm.MdiParent = this;
            CommonClass.ItemListfrm.Show();
            CommonClass.ItemListfrm.Focus();
            if (CommonClass.ItemListfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemListfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void customFieldsListsNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.CustomNamesFrm == null || CommonClass.CustomNamesFrm.IsDisposed)
            {
                CommonClass.CustomNamesFrm = new CustomNames();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomNamesFrm.MdiParent = this;
            CommonClass.CustomNamesFrm.Show();
            CommonClass.CustomNamesFrm.Focus();
            if (CommonClass.CustomNamesFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomNamesFrm.Close();
            }
            this.Cursor = Cursors.Default;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CommonClass.CustomList1Frm == null || CommonClass.CustomList1Frm.IsDisposed)
            {
                CommonClass.CustomList1Frm = new CustomList1();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomList1Frm.MdiParent = this;
            CommonClass.CustomList1Frm.Show();
            CommonClass.CustomList1Frm.Focus();
            if (CommonClass.CustomList1Frm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomList1Frm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void customList2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.CustomList2Frm == null || CommonClass.CustomList2Frm.IsDisposed)
            {
                CommonClass.CustomList2Frm = new CustomList2();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomList2Frm.MdiParent = this;
            CommonClass.CustomList2Frm.Show();
            CommonClass.CustomList2Frm.Focus();
            if (CommonClass.CustomList2Frm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomList2Frm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void customList3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.CustomList3Frm == null || CommonClass.CustomList3Frm.IsDisposed)
            {
                CommonClass.CustomList3Frm = new CustomList3();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomList3Frm.MdiParent = this;
            CommonClass.CustomList3Frm.Show();
            CommonClass.CustomList3Frm.Focus();
            if (CommonClass.CustomList3Frm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomList3Frm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void purchaseSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PurchaseReportSummary == null || CommonClass.PurchaseReportSummary.IsDisposed)
            {
                CommonClass.PurchaseReportSummary = new rptPurchaseReportSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PurchaseReportSummary.MdiParent = this;
            CommonClass.PurchaseReportSummary.Show();
            CommonClass.PurchaseReportSummary.Focus();
            if (CommonClass.PurchaseReportSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PurchaseReportSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void salesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesReportSummaryCustomizer == null || CommonClass.SalesReportSummaryCustomizer.IsDisposed)
            {
                CommonClass.SalesReportSummaryCustomizer = new rptCusomizerSalesSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesReportSummaryCustomizer.MdiParent = this;
            CommonClass.SalesReportSummaryCustomizer.Show();
            CommonClass.SalesReportSummaryCustomizer.Focus();
            if (CommonClass.SalesReportSummaryCustomizer.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesReportSummaryCustomizer.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void salesDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesReportDetail == null || CommonClass.SalesReportDetail.IsDisposed)
            {
                CommonClass.SalesReportDetail = new rptSalesReportDetail();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesReportDetail.MdiParent = this;
            CommonClass.SalesReportDetail.Show();
            CommonClass.SalesReportDetail.Focus();
            if (CommonClass.SalesReportDetail.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesReportDetail.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void purchaseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PurchaseReportDetails == null || CommonClass.PurchaseReportDetails.IsDisposed)
            {
                CommonClass.PurchaseReportDetails = new PurchaseReportDetails();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PurchaseReportDetails.MdiParent = this;
            CommonClass.PurchaseReportDetails.Show();
            CommonClass.PurchaseReportDetails.Focus();
            if (CommonClass.PurchaseReportDetails.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PurchaseReportDetails.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void analysePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.AnalysePurchaseRptfrm == null || CommonClass.AnalysePurchaseRptfrm.IsDisposed)
            {
                CommonClass.AnalysePurchaseRptfrm = new AnalysePurchaseRpt();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AnalysePurchaseRptfrm.MdiParent = this;
            CommonClass.AnalysePurchaseRptfrm.Show();
            CommonClass.AnalysePurchaseRptfrm.Focus();
            if (CommonClass.AnalysePurchaseRptfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AnalysePurchaseRptfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void analysePurchaseFinancialYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.AnalysePuchaseFYComparison == null || CommonClass.AnalysePuchaseFYComparison.IsDisposed)
            {
                CommonClass.AnalysePuchaseFYComparison = new AnalysePuchaseFYComparison();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AnalysePuchaseFYComparison.MdiParent = this;
            CommonClass.AnalysePuchaseFYComparison.Show();
            CommonClass.AnalysePuchaseFYComparison.Focus();
            if (CommonClass.AnalysePuchaseFYComparison.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AnalysePuchaseFYComparison.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void supplierLedgersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.rptSupllierLedger == null || CommonClass.rptSupllierLedger.IsDisposed)
            {
                CommonClass.rptSupllierLedger = new rptSupplierLedger();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.rptSupllierLedger.MdiParent = this;
            CommonClass.rptSupllierLedger.Show();
            CommonClass.rptSupllierLedger.Focus();
            if (CommonClass.rptSupllierLedger.DialogResult == DialogResult.Cancel)
            {
                CommonClass.rptSupllierLedger.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void closedBillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ReceivedOrdersSummary == null || CommonClass.ReceivedOrdersSummary.IsDisposed)
            {
                CommonClass.ReceivedOrdersSummary = new ReceivedOrdersSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ReceivedOrdersSummary.MdiParent = this;
            CommonClass.ReceivedOrdersSummary.Show();
            CommonClass.ReceivedOrdersSummary.Focus();
            if (CommonClass.ReceivedOrdersSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ReceivedOrdersSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void openBillsAndOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ReceivedOrdersDetail == null || CommonClass.ReceivedOrdersDetail.IsDisposed)
            {
                CommonClass.ReceivedOrdersDetail = new ReceivedOrdersDetail();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ReceivedOrdersDetail.MdiParent = this;
            CommonClass.ReceivedOrdersDetail.Show();
            CommonClass.ReceivedOrdersDetail.Focus();
            if (CommonClass.ReceivedOrdersDetail.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ReceivedOrdersDetail.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void allPurchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.AllPurchaseReport == null || CommonClass.AllPurchaseReport.IsDisposed)
            {
                CommonClass.AllPurchaseReport = new AllPurchaseReport();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AllPurchaseReport.MdiParent = this;
            CommonClass.AllPurchaseReport.Show();
            CommonClass.AllPurchaseReport.Focus();
            if (CommonClass.AllPurchaseReport.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AllPurchaseReport.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void itemListSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemReportSummary == null || CommonClass.ItemReportSummary.IsDisposed)
            {
                CommonClass.ItemReportSummary = new Reports.InventoryReports.RptItemSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemReportSummary.MdiParent = this;
            CommonClass.ItemReportSummary.Show();
            CommonClass.ItemReportSummary.Focus();
            if (CommonClass.ItemReportSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemReportSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void itemListDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemReportDetails == null || CommonClass.ItemReportDetails.IsDisposed)
            {
                CommonClass.ItemReportDetails = new Reports.InventoryReports.RptItemDetails();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemReportDetails.MdiParent = this;
            CommonClass.ItemReportDetails.Show();
            CommonClass.ItemReportDetails.Focus();
            if (CommonClass.ItemReportDetails.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemReportDetails.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void quotesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CommonClass.PurchaseQuotesReport == null || CommonClass.PurchaseQuotesReport.IsDisposed)
            {
                CommonClass.PurchaseQuotesReport = new PurchaseQuotesReport();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PurchaseQuotesReport.MdiParent = this;
            CommonClass.PurchaseQuotesReport.Show();
            CommonClass.PurchaseQuotesReport.Focus();
            if (CommonClass.PurchaseQuotesReport.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PurchaseQuotesReport.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void returnsAndDebitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PurchaseReturnandCredit == null || CommonClass.PurchaseReturnandCredit.IsDisposed)
            {
                CommonClass.PurchaseReturnandCredit = new PurchaseReturnandCredit();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PurchaseReturnandCredit.MdiParent = this;
            CommonClass.PurchaseReturnandCredit.Show();
            CommonClass.PurchaseReturnandCredit.Focus();
            if (CommonClass.PurchaseReturnandCredit.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PurchaseReturnandCredit.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void openItemRecieptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ReceivedOrdersDetail == null || CommonClass.ReceivedOrdersDetail.IsDisposed)
            {
                CommonClass.ReceivedOrdersDetail = new ReceivedOrdersDetail();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ReceivedOrdersDetail.MdiParent = this;
            CommonClass.ReceivedOrdersDetail.Show();
            CommonClass.ReceivedOrdersDetail.Focus();
            if (CommonClass.ReceivedOrdersDetail.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ReceivedOrdersDetail.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void priceDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PriceReportDetails == null || CommonClass.PriceReportDetails.IsDisposed)
            {
                CommonClass.PriceReportDetails = new Reports.InventoryReports.RptPriceDetails();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PriceReportDetails.MdiParent = this;
            CommonClass.PriceReportDetails.Show();
            CommonClass.PriceReportDetails.Focus();
            if (CommonClass.PriceReportDetails.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PriceReportDetails.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void priceAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PriceReportAnalysis == null || CommonClass.PriceReportAnalysis.IsDisposed)
            {
                CommonClass.PriceReportAnalysis = new Reports.InventoryReports.RptPriceAnalysis();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PriceReportAnalysis.MdiParent = this;
            CommonClass.PriceReportAnalysis.Show();
            CommonClass.PriceReportAnalysis.Focus();
            if (CommonClass.PriceReportAnalysis.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PriceReportAnalysis.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void analyseSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesReportAnalyse == null || CommonClass.SalesReportAnalyse.IsDisposed)
            {
                CommonClass.SalesReportAnalyse = new Reports.RptAnalyseSales();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesReportAnalyse.MdiParent = this;
            CommonClass.SalesReportAnalyse.Show();
            CommonClass.SalesReportAnalyse.Focus();
            if (CommonClass.SalesReportAnalyse.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesReportAnalyse.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void allSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.AllSales == null || CommonClass.AllSales.IsDisposed)
            {
                CommonClass.AllSales = new RptAllSales();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AllSales.MdiParent = this;
            CommonClass.AllSales.Show();
            CommonClass.AllSales.Focus();
            if (CommonClass.AllSales.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AllSales.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void closedInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ClosedInvoices == null || CommonClass.AllSales.IsDisposed)
            {
                CommonClass.ClosedInvoices = new RptClosedInvoices();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ClosedInvoices.MdiParent = this;
            CommonClass.ClosedInvoices.Show();
            CommonClass.ClosedInvoices.Focus();
            if (CommonClass.ClosedInvoices.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ClosedInvoices.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void openInvoicesAndOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.OpenInvoiceOrder == null || CommonClass.OpenInvoiceOrder.IsDisposed)
            {
                CommonClass.OpenInvoiceOrder = new RptOpenInvoiceOrder();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.OpenInvoiceOrder.MdiParent = this;
            CommonClass.OpenInvoiceOrder.Show();
            CommonClass.OpenInvoiceOrder.Focus();
            if (CommonClass.OpenInvoiceOrder.DialogResult == DialogResult.Cancel)
            {
                CommonClass.OpenInvoiceOrder.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void quotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.Quotes == null || CommonClass.Quotes.IsDisposed)
            {
                CommonClass.Quotes = new RptQuotes();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.Quotes.MdiParent = this;
            CommonClass.Quotes.Show();
            CommonClass.Quotes.Focus();
            if (CommonClass.Quotes.DialogResult == DialogResult.Cancel)
            {
                CommonClass.Quotes.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void analyseSalesFinancialYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.AnalyseSalesFY == null || CommonClass.AnalyseSalesFY.IsDisposed)
            {
                CommonClass.AnalyseSalesFY = new RptAnalyseSalesFY();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AnalyseSalesFY.MdiParent = this;
            CommonClass.AnalyseSalesFY.Show();
            CommonClass.AnalyseSalesFY.Focus();
            if (CommonClass.AnalyseSalesFY.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AnalyseSalesFY.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void customerLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.CustomerLedger == null || CommonClass.CustomerLedger.IsDisposed)
            {
                CommonClass.CustomerLedger = new RptCustomerLedger();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomerLedger.MdiParent = this;
            CommonClass.CustomerLedger.Show();
            CommonClass.CustomerLedger.Focus();
            if (CommonClass.CustomerLedger.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomerLedger.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void itemRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemRegisterFrm == null || CommonClass.ItemRegisterFrm.IsDisposed)
            {
                CommonClass.ItemRegisterFrm = new ItemRegister();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemRegisterFrm.MdiParent = this;
            CommonClass.ItemRegisterFrm.Show();
            CommonClass.ItemRegisterFrm.Focus();
            if (CommonClass.ItemRegisterFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemRegisterFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void salesItemSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesItemSummary == null || CommonClass.SalesItemSummary.IsDisposed)
            {
                CommonClass.SalesItemSummary = new RptSalesItemSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesItemSummary.MdiParent = this;
            CommonClass.SalesItemSummary.Show();
            CommonClass.SalesItemSummary.Focus();
            if (CommonClass.SalesItemSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesItemSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void salesItemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesItemDetails == null || CommonClass.SalesItemDetails.IsDisposed)
            {
                CommonClass.SalesItemDetails = new RptSalesItemDetails();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesItemDetails.MdiParent = this;
            CommonClass.SalesItemDetails.Show();
            CommonClass.SalesItemDetails.Focus();
            if (CommonClass.SalesItemDetails.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesItemDetails.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void ageReceivableSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.AgeReceivableSummary == null || CommonClass.AgeReceivableSummary.IsDisposed)
            {
                CommonClass.AgeReceivableSummary = new RptAgeReceivableSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AgeReceivableSummary.MdiParent = this;
            CommonClass.AgeReceivableSummary.Show();
            CommonClass.AgeReceivableSummary.Focus();
            if (CommonClass.AgeReceivableSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AgeReceivableSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void ageReceivableDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.AgeReceivableDetail == null || CommonClass.AgeReceivableDetail.IsDisposed)
            {
                CommonClass.AgeReceivableDetail = new RptAgeReceivableDetail();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AgeReceivableDetail.MdiParent = this;
            CommonClass.AgeReceivableDetail.Show();
            CommonClass.AgeReceivableDetail.Focus();
            if (CommonClass.AgeReceivableDetail.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AgeReceivableDetail.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void supplierItemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptSupplierItemDetail == null || CommonClass.RptSupplierItemDetail.IsDisposed)
            {
                CommonClass.RptSupplierItemDetail = new RptSupplierItemDetail();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptSupplierItemDetail.MdiParent = this;
            CommonClass.RptSupplierItemDetail.Show();
            CommonClass.RptSupplierItemDetail.Focus();
            if (CommonClass.RptSupplierItemDetail.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptSupplierItemDetail.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void supplierPaymentHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptSupplierPaymentHistory == null || CommonClass.RptSupplierPaymentHistory.IsDisposed)
            {
                CommonClass.RptSupplierPaymentHistory = new RptSupplierPaymentHistory();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptSupplierPaymentHistory.MdiParent = this;
            CommonClass.RptSupplierPaymentHistory.Show();
            CommonClass.RptSupplierPaymentHistory.Focus();
            if (CommonClass.RptSupplierPaymentHistory.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptSupplierPaymentHistory.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void supplierPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.rptSupplierPayments == null || CommonClass.rptSupplierPayments.IsDisposed)
            {
                CommonClass.rptSupplierPayments = new RptSupplierPayments();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.rptSupplierPayments.MdiParent = this;
            CommonClass.rptSupplierPayments.Show();
            CommonClass.rptSupplierPayments.Focus();
            if (CommonClass.rptSupplierPayments.DialogResult == DialogResult.Cancel)
            {
                CommonClass.rptSupplierPayments.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void supplierItemSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.rptSupplierItemSummary == null || CommonClass.rptSupplierItemSummary.IsDisposed)
            {
                CommonClass.rptSupplierItemSummary = new RptSupplierItemSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.rptSupplierItemSummary.MdiParent = this;
            CommonClass.rptSupplierItemSummary.Show();
            CommonClass.rptSupplierItemSummary.Focus();
            if (CommonClass.rptSupplierItemSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.rptSupplierItemSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void ageingSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptAgeingSummary == null || CommonClass.RptAgeingSummary.IsDisposed)
            {
                CommonClass.RptAgeingSummary = new RptAgeingSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptAgeingSummary.MdiParent = this;
            CommonClass.RptAgeingSummary.Show();
            CommonClass.RptAgeingSummary.Focus();
            if (CommonClass.RptAgeingSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptAgeingSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void ageingDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptAgeingDetail == null || CommonClass.RptAgeingDetail.IsDisposed)
            {
                CommonClass.RptAgeingDetail = new RptAgeingDetail();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptAgeingDetail.MdiParent = this;
            CommonClass.RptAgeingDetail.Show();
            CommonClass.RptAgeingDetail.Focus();
            if (CommonClass.RptAgeingDetail.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptAgeingDetail.Close();
            }
            this.Cursor = Cursors.Default;

        }

        private void customerPaymentsClosedInvoiceToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (CommonClass.CustomerPayment == null || CommonClass.CustomerPayment.IsDisposed)
            {
                CommonClass.CustomerPayment = new RptCustomerPayments();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomerPayment.MdiParent = this;
            CommonClass.CustomerPayment.Show();
            CommonClass.CustomerPayment.Focus();
            if (CommonClass.CustomerPayment.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomerPayment.Close();

            }
            this.Cursor = Cursors.Default;
        }

        private void buildItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.BuildItemsFrm == null || CommonClass.BuildItemsFrm.IsDisposed)
            {
                CommonClass.BuildItemsFrm = new BuildItems();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.BuildItemsFrm.MdiParent = this;
            CommonClass.BuildItemsFrm.Show();
            CommonClass.BuildItemsFrm.Focus();
            if (CommonClass.BuildItemsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.BuildItemsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void inventoryAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.StockAdjustmentsFrm == null || CommonClass.StockAdjustmentsFrm.IsDisposed)
            {
                CommonClass.StockAdjustmentsFrm = new StockAdjustments();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.StockAdjustmentsFrm.MdiParent = this;
            CommonClass.StockAdjustmentsFrm.Show();
            CommonClass.StockAdjustmentsFrm.Focus();
            if (CommonClass.StockAdjustmentsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.StockAdjustmentsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void autoBuildInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.AutoBuildItemsFrm == null || CommonClass.AutoBuildItemsFrm.IsDisposed)
            {
                CommonClass.AutoBuildItemsFrm = new AutoBuildItems();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AutoBuildItemsFrm.MdiParent = this;
            CommonClass.AutoBuildItemsFrm.Show();
            CommonClass.AutoBuildItemsFrm.Focus();
            if (CommonClass.AutoBuildItemsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AutoBuildItemsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void stocktakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.StocktakeFrm == null || CommonClass.StocktakeFrm.IsDisposed)
            {
                CommonClass.StocktakeFrm = new Stocktake();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.StocktakeFrm.MdiParent = this;
            CommonClass.StocktakeFrm.Show();
            CommonClass.StocktakeFrm.Focus();
            if (CommonClass.StocktakeFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.StocktakeFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void analyseInventorySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT i.ItemName,
                                i.ItemNumber, 
                                iq.OnHandQty ,
                                pl.OrderQty,
                                sl.ShipQty,
                                s.SalesType
                            FROM Items i 
                            LEFT JOIN SalesLines sl ON i.ID = sl.EntityID 
                            LEFT JOIN Sales s ON sl.SalesID=s.SalesID 
                            INNER JOIN ItemsQty iq ON i.ID = iq.ItemID
                            LEFT JOIn PurchaseLines pl ON i.ID = pl.EntityID";

            DataTable TbRep = new DataTable();
            CommonClass.runSql(ref TbRep, sql);

            Reports.ReportParams inventoryparams = new Reports.ReportParams();
            inventoryparams.PrtOpt = 1;
            inventoryparams.Rec.Add(TbRep);

            inventoryparams.ReportName = "AnalyseInventorySummary.rpt";
            inventoryparams.RptTitle = "Analyse Inventory Summary";

            inventoryparams.Params = "compname";
            inventoryparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(inventoryparams);
        }

        private void analyseInventoryDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT 
                                i.ItemNumber,
                                i.ItemName,
                                s.SalesNumber AS IDNumber,
                                p.Name AS ProfileName,
                                s.TransactionDate,
                                s.PromiseDate,
                                (SELECT COUNT(SalesID) FROM Sales WHERE SalesType='ORDER'AND InvoiceStatus='Order') AS Committed,
                                0 AS OnOrder,
                                0 AS Available,
                                iq.OnHandQty
                            FROM Items i
                            INNER JOIN ItemsQty iq ON iq.ItemID = i.ID
                            LEFT JOIN SalesLines sl ON sl.EntityID = i.ID
                            LEFT JOIN Sales s ON sl.SalesID = s.SalesID
                            LEFT JOIN Profile p ON p.ID = s.CustomerID
                            WHERE s.SalesType = 'ORDER'
                            AND s.InvoiceStatus = 'Order'
                            UNION
                            SELECT 
                                i.ItemNumber,
                                i.ItemName,
                                s.PurchaseNumber AS IDNumber,
                                p.Name AS ProfileName,
                                s.TransactionDate,
                                s.PromiseDate,
                                0 AS committed,
                                ((SELECT COUNT(PurchaseID) FROM Purchases WHERE PurchaseType = 'ORDER' AND POStatus = 'Order') + (sl.OrderQty - sl.ReceiveQty)) AS OnOrder,
                                (iq.OnHandQty) - 0 + ((SELECT COUNT(PurchaseID) FROM Purchases WHERE PurchaseType = 'ORDER' AND POStatus = 'Order') + (sl.OrderQty - sl.ReceiveQty)) AS Available,
                                iq.OnHandQty
                            FROM Items i
                            INNER JOIN ItemsQty iq ON iq.ItemID = i.ID
                            LEFT JOIN PurchaseLines sl ON sl.EntityID = i.ID
                            LEFT JOIN Purchases s ON sl.PurchaseID = s.PurchaseID
                            LEFT JOIN Profile p ON p.ID = s.SupplierID
                            WHERE s.PurchaseType = 'ORDER'
                            AND POStatus = 'Order'";

            DataTable TbRep = new DataTable();

            CommonClass.runSql(ref TbRep, sql);

            Reports.ReportParams profilelistparams = new Reports.ReportParams();
            profilelistparams.PrtOpt = 1;
            profilelistparams.Rec.Add(TbRep);

            profilelistparams.ReportName = "AnalyseInventoryDetail.rpt";
            profilelistparams.RptTitle = "Analyse Inventory Detail";

            profilelistparams.Params = "compname";
            profilelistparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(profilelistparams);
        }

        private void autoBuildItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT a.PartItemQty,
                                    a.ItemID, 
                                    a.PartItemID,
                                    i.ItemNumber,
                                    i.PartNumber, 
                                    i.ItemName,
                                    p.AverageCostEx,
                                    q.OnHandQty
                                FROM ItemsAutoBuild a 
                                INNER JOIN Items i ON a.PartItemID = i.ID
                                INNER JOIN ItemsCostPrice p ON p.ItemID = i.ID
                                INNER JOIN ItemsQty q ON q.ItemID = i.ID";

            DataTable TbRep = new DataTable();
            CommonClass.runSql(ref TbRep, sql);
            string sql2 = @"SELECT a.PartItemQty, 
                                a.ItemID, 
                                a.PartItemID,
                                i.ItemNumber, 
                                i.PartNumber, 
                                i.ItemName,
                                p.AverageCostEx, 
                                q.OnHandQty
                            FROM ItemsAutoBuild a 
                            INNER JOIN Items i ON a.ItemID = i.ID
                            INNER JOIN ItemsCostPrice p ON p.ItemID = i.ID
                            INNER JOIN ItemsQty q ON q.ItemID = i.ID";

            DataTable TbRep2 = new DataTable();
            CommonClass.runSql(ref TbRep2, sql2);

            Reports.ReportParams profilelistparams = new Reports.ReportParams();
            profilelistparams.PrtOpt = 1;
            profilelistparams.Rec.Add(TbRep);
            profilelistparams.Rec.Add(TbRep2);

            profilelistparams.ReportName = "AutoBuildItems.rpt";
            profilelistparams.RptTitle = "Auto-Build Items";

            profilelistparams.Params = "compname";
            profilelistparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(profilelistparams);
        }

        private void inventoryCountSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT i.ItemNumber, 
                                i.ItemName, 
                                iq.OnHandQty, 
                                i.BuyingUOM
                                FROM Items i 
                                INNER JOIN ItemsQty iq ON i.ID = iq.ItemID";

            DataTable TbRep = new DataTable();

            CommonClass.runSql(ref TbRep, sql);

            Reports.ReportParams profilelistparams = new Reports.ReportParams();
            profilelistparams.PrtOpt = 1;
            profilelistparams.Rec.Add(TbRep);

            profilelistparams.ReportName = "InventoryCountSheet.rpt";
            profilelistparams.RptTitle = "Inventory Count Sheet";

            profilelistparams.Params = "compname";
            profilelistparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(profilelistparams);
        }

        private void priceSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT i.ItemName, 
                                i.ItemNumber, 
                                isp.Level0 
                                FROM Items i
                                INNER JOIN ItemsSellingPrice isp ON i.ID = isp.ItemID";

            DataTable TbRep = new DataTable();
            CommonClass.runSql(ref TbRep, sql);

            Reports.ReportParams profilelistparams = new Reports.ReportParams();
            profilelistparams.PrtOpt = 1;
            profilelistparams.Rec.Add(TbRep);

            profilelistparams.ReportName = "PriceListSummary.rpt";
            profilelistparams.RptTitle = "Price List Summary";

            profilelistparams.Params = "compname";
            profilelistparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(profilelistparams);
        }

        private void customerStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesStatement == null
         || CommonClass.SalesStatement.IsDisposed)
            {
                CommonClass.SalesStatement = new SalesStatement();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesStatement.MdiParent = this;
            CommonClass.SalesStatement.Show();
            CommonClass.SalesStatement.Focus();
            if (CommonClass.SalesStatement.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesStatement.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void itemPriceUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemPriceUpdateFrm == null
         || CommonClass.ItemPriceUpdateFrm.IsDisposed)
            {
                CommonClass.ItemPriceUpdateFrm = new ItemPriceUpdate();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemPriceUpdateFrm.MdiParent = this;
            CommonClass.ItemPriceUpdateFrm.Show();
            CommonClass.ItemPriceUpdateFrm.Focus();
            if (CommonClass.ItemPriceUpdateFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemPriceUpdateFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            if (CommonClass.setupNP == null
            || CommonClass.setupNP.IsDisposed)
            {
                CommonClass.setupNP = new SetupNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.setupNP.MdiParent = this;
            CommonClass.setupNP.Show();
            CommonClass.setupNP.Focus();
            if (CommonClass.setupNP.DialogResult == DialogResult.Cancel)
            {
                CommonClass.setupNP.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void reconciliationDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptReconciliationDetail == null
           || CommonClass.RptReconciliationDetail.IsDisposed)
            {
                CommonClass.RptReconciliationDetail = new RptReconciliationDetail();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptReconciliationDetail.MdiParent = this;
            CommonClass.RptReconciliationDetail.Show();
            CommonClass.RptReconciliationDetail.Focus();
            if (CommonClass.RptReconciliationDetail.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptReconciliationDetail.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void reconciliationSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptReconciliationSummary == null
           || CommonClass.RptReconciliationSummary.IsDisposed)
            {
                CommonClass.RptReconciliationSummary = new Reports.SalesReports.RptReconciliationSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptReconciliationSummary.MdiParent = this;
            CommonClass.RptReconciliationSummary.Show();
            CommonClass.RptReconciliationSummary.Focus();
            if (CommonClass.RptReconciliationSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptReconciliationSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void PurchasereconciliationSummaryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CommonClass.rptPurchaseReconSummary == null || CommonClass.rptPurchaseReconSummary.IsDisposed)
            {
                CommonClass.rptPurchaseReconSummary = new Reports.PurchaseReports.RptPurchaseReconciliationSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.rptPurchaseReconSummary.MdiParent = this;
            CommonClass.rptPurchaseReconSummary.Show();
            CommonClass.rptPurchaseReconSummary.Focus();
            if (CommonClass.rptPurchaseReconSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.rptPurchaseReconSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void purchasereconciliationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.rptPurchaseReconDetails == null || CommonClass.rptPurchaseReconDetails.IsDisposed)
            {
                CommonClass.rptPurchaseReconDetails = new Reports.PurchaseReports.RptPurchaseReconciliationDetails();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.rptPurchaseReconDetails.MdiParent = this;
            CommonClass.rptPurchaseReconDetails.Show();
            CommonClass.rptPurchaseReconDetails.Focus();
            if (CommonClass.rptPurchaseReconDetails.DialogResult == DialogResult.Cancel)
            {
                CommonClass.rptPurchaseReconDetails.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void itemRegisterSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemReportRegisterSummary == null || CommonClass.ItemReportRegisterSummary.IsDisposed)
            {
                CommonClass.ItemReportRegisterSummary = new Reports.InventoryReports.RptItemRegisterSummary();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemReportRegisterSummary.MdiParent = this;
            CommonClass.ItemReportRegisterSummary.Show();
            CommonClass.ItemReportRegisterSummary.Focus();
            if (CommonClass.ItemReportRegisterSummary.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemReportRegisterSummary.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void billTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.rptBillTransaction == null || CommonClass.rptBillTransaction.IsDisposed)
            {
                CommonClass.rptBillTransaction = new Reports.PurchaseReports.RptBillTransaction();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.rptBillTransaction.MdiParent = this;
            CommonClass.rptBillTransaction.Show();
            CommonClass.rptBillTransaction.Focus();
            if (CommonClass.rptBillTransaction.DialogResult == DialogResult.Cancel)
            {
                CommonClass.rptBillTransaction.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void itemTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemReportTransactions == null || CommonClass.ItemReportTransactions.IsDisposed)
            {
                CommonClass.ItemReportTransactions = new Reports.InventoryReports.RptItemTransactions();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemReportTransactions.MdiParent = this;
            CommonClass.ItemReportTransactions.Show();
            CommonClass.ItemReportTransactions.Focus();
            if (CommonClass.ItemReportTransactions.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemReportTransactions.Close();
            }
            this.Cursor = Cursors.Default;
        }
        private void summaryWithTaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesSummarywithTax == null || CommonClass.SalesSummarywithTax.IsDisposed)
            {
                CommonClass.SalesSummarywithTax = new Reports.SalesReports.SalesSummarywithTax();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesSummarywithTax.MdiParent = this;
            CommonClass.SalesSummarywithTax.Show();
            CommonClass.SalesSummarywithTax.Focus();
            if (CommonClass.SalesSummarywithTax.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesSummarywithTax.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void salesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptReceivableJournal == null || CommonClass.RptReceivableJournal.IsDisposed)
            {
                CommonClass.RptReceivableJournal = new Reports.SalesReports.RptReceivableJournal();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptReceivableJournal.MdiParent = this;
            CommonClass.RptReceivableJournal.Show();
            CommonClass.RptReceivableJournal.Focus();
            if (CommonClass.RptReceivableJournal.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptReceivableJournal.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void purchasesPayablesJournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.rptPurchasePayablesJournal == null || CommonClass.rptPurchasePayablesJournal.IsDisposed)
            {
                CommonClass.rptPurchasePayablesJournal = new Reports.PurchaseReports.RptPurchasePayablesJournal();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.rptPurchasePayablesJournal.MdiParent = this;
            CommonClass.rptPurchasePayablesJournal.Show();
            CommonClass.rptPurchasePayablesJournal.Focus();
            if (CommonClass.rptPurchasePayablesJournal.DialogResult == DialogResult.Cancel)
            {
                CommonClass.rptPurchasePayablesJournal.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void setupGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.setupNP == null
                || CommonClass.setupNP.IsDisposed)
            {
                CommonClass.setupNP = new SetupNP();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.setupNP.MdiParent = this;
            CommonClass.setupNP.Show();
            CommonClass.setupNP.Focus();
            if (CommonClass.setupNP.DialogResult == DialogResult.Cancel)
            {
                CommonClass.setupNP.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            if (CommonClass.RptPanelFrm == null || CommonClass.RptPanelFrm.IsDisposed)
            {
                CommonClass.RptPanelFrm = new ReportsNavigation();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptPanelFrm.MdiParent = this;
            CommonClass.RptPanelFrm.Show();
            CommonClass.RptPanelFrm.Focus();
            if (CommonClass.RptPanelFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptPanelFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void taxCodeListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void emailStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.EmailStatementfrm == null || CommonClass.EmailStatementfrm.IsDisposed)
            {
                CommonClass.EmailStatementfrm = new EmailStatement();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.EmailStatementfrm.MdiParent = this;
            CommonClass.EmailStatementfrm.Show();
            CommonClass.EmailStatementfrm.Focus();
            if (CommonClass.EmailStatementfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.EmailStatementfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void remittanceAdvicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.rptRemittanceAdvicePayBills == null || CommonClass.rptRemittanceAdvicePayBills.IsDisposed)
            {
                CommonClass.rptRemittanceAdvicePayBills = new Reports.PurchaseReports.RptRemittanceAdvicePayBills();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.rptRemittanceAdvicePayBills.MdiParent = this;
            CommonClass.rptRemittanceAdvicePayBills.Show();
            CommonClass.rptRemittanceAdvicePayBills.Focus();
            if (CommonClass.rptRemittanceAdvicePayBills.DialogResult == DialogResult.Cancel)
            {
                CommonClass.rptRemittanceAdvicePayBills.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (CommonClass.CustomerListFrm == null || CommonClass.CustomerListFrm.IsDisposed)
            {
                CommonClass.CustomerListFrm = new CustomerList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomerListFrm.MdiParent = this.MdiParent;
            CommonClass.CustomerListFrm.Show();
            CommonClass.CustomerListFrm.Focus();
            if (CommonClass.CustomerListFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomerListFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void bttnSupplier_Click(object sender, EventArgs e)
        {
            if (CommonClass.SupplierListFrm == null || CommonClass.SupplierListFrm.IsDisposed)
            {
                CommonClass.SupplierListFrm = new SupplierList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SupplierListFrm.MdiParent = this.MdiParent;
            CommonClass.SupplierListFrm.Show();
            CommonClass.SupplierListFrm.Focus();
            if (CommonClass.SupplierListFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SupplierListFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void Reminder()
        {
            CommonClass.Reminder = new Reminder();

            this.Cursor = Cursors.WaitCursor;
            CommonClass.Reminder.MdiParent = this.MdiParent;
            CommonClass.Reminder.Show();
            CommonClass.Reminder.Focus();
            if (CommonClass.Reminder.DialogResult == DialogResult.Cancel)
            {
                CommonClass.Reminder.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private static bool isReminder()
        {
            DateTime tdate = DateTime.Now.ToUniversalTime();
            SqlConnection cona = null;
            try
            {
                cona = new SqlConnection(CommonClass.ConStr);
                string selectSqla = "SELECT * FROM Recurring WHERE NotifyDate <= @tdate AND Frequency != 'Daily'  AND NotifyUserID is null OR NotifyUserID =" + CommonClass.UserID + "";
                SqlCommand cmda = new SqlCommand(selectSqla, cona);
                cmda.Parameters.AddWithValue("@tdate", tdate);
                cona.Open();
                using (SqlDataReader reader = cmda.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (cona != null)
                    cona.Close();
            }
        }

        private void remindersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reminder();
        }

        private void systemAuditTrailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonClass.AuditTrail = new Utilities.AuditTrail();

            this.Cursor = Cursors.WaitCursor;
            CommonClass.AuditTrail.MdiParent = this;
            CommonClass.AuditTrail.Show();
            CommonClass.AuditTrail.Focus();
            if (CommonClass.AuditTrail.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AuditTrail.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void emailAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (CommonClass.EmailAcctfrm == null
            //    || CommonClass.EmailAcctfrm.IsDisposed)
            //{
            //    CommonClass.EmailAcctfrm = new EmailAccount();
            //}
            //this.Cursor = Cursors.WaitCursor;
            //CommonClass.EmailAcctfrm.MdiParent = this;
            //CommonClass.EmailAcctfrm.Show();
            //CommonClass.EmailAcctfrm.Focus();
            //this.Cursor = Cursors.Default;
        }

        private void toDoListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonClass.ToDoList = new Utilities.ToDoList();

            this.Cursor = Cursors.WaitCursor;
            CommonClass.ToDoList.MdiParent = this;
            CommonClass.ToDoList.Show();
            CommonClass.ToDoList.Focus();
            if (CommonClass.ToDoList.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ToDoList.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void pNGGSTReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonClass.RptPNGGSTFrm = new RptPNGGST();

            this.Cursor = Cursors.WaitCursor;
            CommonClass.RptPNGGSTFrm.MdiParent = this;
            CommonClass.RptPNGGSTFrm.Show();
            CommonClass.RptPNGGSTFrm.Focus();
            if (CommonClass.RptPNGGSTFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RptPNGGSTFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void redeemableItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.RedeemItemfrm == null
                || CommonClass.RedeemItemfrm.IsDisposed)
            {
                CommonClass.RedeemItemfrm = new RedeemItem();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RedeemItemfrm.MdiParent = this;
            CommonClass.RedeemItemfrm.Show();
            CommonClass.RedeemItemfrm.Focus();
            if (CommonClass.RedeemItemfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RedeemItemfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void promotionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PointAccumulation == null
                || CommonClass.PointAccumulation.IsDisposed)
            {
                CommonClass.PointAccumulation = new PointsAccumulation();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PointAccumulation.MdiParent = this;
            CommonClass.PointAccumulation.Show();
            CommonClass.PointAccumulation.Focus();
            if (CommonClass.PointAccumulation.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PointAccumulation.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void pointsExchangeRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.PointExhangeRate == null
                || CommonClass.PointExhangeRate.IsDisposed)
            {
                CommonClass.PointExhangeRate = new PointsExchangeRate();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PointExhangeRate.MdiParent = this;
            CommonClass.PointExhangeRate.Show();
            CommonClass.PointExhangeRate.Focus();
            if (CommonClass.PointExhangeRate.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PointExhangeRate.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void giftCertificatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.GiftCertificateLookup == null
                || CommonClass.GiftCertificateLookup.IsDisposed)
            {
                CommonClass.GiftCertificateLookup = new GiftCertificateLookup();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.GiftCertificateLookup.MdiParent = this;
            CommonClass.GiftCertificateLookup.Show();
            CommonClass.GiftCertificateLookup.Focus();
            if (CommonClass.GiftCertificateLookup.DialogResult == DialogResult.Cancel)
            {
                CommonClass.GiftCertificateLookup.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void loyaltyMembersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (CommonClass.LoyaltyMemberLookup == null
                || CommonClass.LoyaltyMemberLookup.IsDisposed)
            {
                CommonClass.LoyaltyMemberLookup = new LoyaltyMemberList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.LoyaltyMemberLookup.MdiParent = this;
            CommonClass.LoyaltyMemberLookup.Show();
            CommonClass.LoyaltyMemberLookup.Focus();
            if (CommonClass.LoyaltyMemberLookup.DialogResult == DialogResult.Cancel)
            {
                CommonClass.LoyaltyMemberLookup.Close();
            }
            this.Cursor = Cursors.Default;
        }

        public string GetMcAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }
            return "";
        }

        private void SessionStart()
        {
            if (CommonClass.isSalesperson == true || CommonClass.isAdministrator == true)
            {
                if (btnSession.Text == "Begin Session")
                {
                    if (CheckNoofTerminals() && CheckPreference())
                    {
                        OpeningFund EnterOpening = new OpeningFund(false);
                        {
                            if (EnterOpening.ShowDialog() == DialogResult.OK)
                            {
                                if (CommonClass.SessionID != 0)
                                {
                                    string sqlupdateTerminal = "UPDATE Sessions SET UserID = '" + CommonClass.UserID + "' WHERE SessionID = " + CommonClass.SessionID;
                                    int i = CommonClass.runSql(sqlupdateTerminal);
                                    enterSalesToolStripMenuItem.Enabled = true;
                                    receivePaymentToolStripMenuItem.Enabled = true;
                                    btnSession.Text = "End Session";
                                    sDate = EnterOpening.GetsDate;
                                    CommonClass.SessionRunning = true;
                                    if (CommonClass.AutoEnd)
                                    {
                                        EndSessionCountDown.Enabled = true;
                                        EndSessionCountDown.Start();
                                    }
                                    return;
                                }
                                sDate = DateTime.Now;
                                string InsertSession = @"INSERT INTO Sessions (SessionKey, SessionStart, SessionEnd, SessionStatus, OpeningFund, FloatFund,UserID)
                                                                VALUES(@SessionKey, @SessionStart, @SessionEnd, @SessionStatus, @OpeningFund, @FloatFund, @UserID)";
                                Dictionary<string, object> param = new Dictionary<string, object>();
                                param.Add("@SessionKey", EnterOpening.GetTerminal);
                                param.Add("@SessionStart", sDate);
                                param.Add("@SessionEnd", "");
                                param.Add("@SessionStatus", "Open");
                                param.Add("@OpeningFund", EnterOpening.GetOpeningFund);
                                param.Add("@FloatFund", GetFloatFund());
                                param.Add("@UserID", CommonClass.UserID);
                                CommonClass.FloatFund = GetFloatFund();
                                TerminalID = int.Parse(EnterOpening.GetTerminal);

                                CommonClass.SessionID = CommonClass.runSql(InsertSession, CommonClass.RunSqlInsertMode.SCALAR, param);
                                MessageBox.Show("New session started at " + sDate.ToShortTimeString());
                                enterSalesToolStripMenuItem.Enabled = true;
                                receivePaymentToolStripMenuItem.Enabled = true;
                                btnSession.Text = "End Session";
                                CommonClass.SessionDate = sDate;
                                CommonClass.SessionRunning = true;
                                if (CommonClass.AutoEnd)
                                {
                                    EndSessionCountDown.Enabled = true;
                                    EndSessionCountDown.Start();
                                }

                            }
                        }
                    }
                }
                else
                {
                    SessionTransactions sessionsForm = new SessionTransactions();
                    decimal floatamt = 0;
                    if (sessionsForm.ShowDialog() == DialogResult.OK)
                    {
                        DateTime eDate = DateTime.Now;
                        string UpdateSession = @"UPDATE Sessions SET SessionEnd = @SessionEnd, SessionStatus = @SessionStatus, FloatFund = @floatFund WHERE SessionID = " + CommonClass.SessionID;
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@SessionEnd", eDate);
                        param.Add("@SessionStatus", "Close");

                        enterSalesToolStripMenuItem.Enabled = false;
                        receivePaymentToolStripMenuItem.Enabled = false;
                        btnSession.Text = "Begin Session";
                        EndSessionCountDown.Stop();
                        EndSessionCountDown.Enabled = false;
                        floatamt = sessionsForm.GetFloatFund;
                        param.Add("@floatFund", floatamt);
                        CommonClass.runSql(UpdateSession, CommonClass.RunSqlInsertMode.QUERY, param);
                        CommonClass.SessionID = 0;
                        CommonClass.SessionRunning = false;
                        MessageBox.Show("Session ended at " + eDate.ToShortTimeString());
                    }


                }
            }
        }

        private void btnSession_Click(object sender, EventArgs e)
        {

            if ((btnSession.Text == "End Session") || !CheckRunningSession())
            {
                SessionStart();
            }
            else
            {
                EndSessionCountDown_Tick(sender, e);

            }
        }

        private void EndSessionCountDown_Tick(object sender, EventArgs e)
        {
            if (!CommonClass.SessionRunning)
                return;

               DateTime endS = sDate.Add(TimeSpan.Parse(CommonClass.ShiftTimeSpan));
            DateTime timeCheck = DateTime.Now;
            if (timeCheck >= endS)
            {
                DialogResult PrintInvoice = MessageBox.Show("Your session has EXPIRED. \n Would you like to start a new one?", "Session Warning!", MessageBoxButtons.YesNo);
                if (PrintInvoice == DialogResult.Yes)
                {
                    btnSession.PerformClick();
                    btnSession.Text = "Begin Session";
                }else
                {
                    btnSession.PerformClick();
                }

               
            }
        }

        public bool CheckRunningSession()
        {
            DataTable SessionDt = new DataTable();
            //string sessionSql = @"SELECT * From Sessions WHERE SessionKey = @TerminalID AND SessionStatus = @SessionStatus AND UserID ='0'";
            string sessionSql = @"SELECT * From Sessions WHERE SessionStatus = @SessionStatus AND UserID ='0'";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionStatus", "Open");
            CommonClass.runSql(ref SessionDt, sessionSql, param);
            if (SessionDt.Rows.Count > 0)
            {
                DataRow dr = SessionDt.Rows[0];
                DialogResult PrintInvoice = MessageBox.Show("System detected a running session. \n Would you like to continue your existing session?", "Session Warning!", MessageBoxButtons.YesNo);
                if (PrintInvoice == DialogResult.Yes)
                {
                    OpeningFund EnterOpening = new OpeningFund(true);
                    {
                        if (EnterOpening.ShowDialog() == DialogResult.OK)
                        {
                            if (CommonClass.SessionID != 0)
                            {
                                string sqlupdateTerminal = "UPDATE Sessions SET UserID = '" + CommonClass.UserID + "' WHERE SessionID = " + CommonClass.SessionID;
                                int i = CommonClass.runSql(sqlupdateTerminal);
                                enterSalesToolStripMenuItem.Enabled = true;
                                receivePaymentToolStripMenuItem.Enabled = true;
                                btnSession.Text = "End Session";
                                sDate = EnterOpening.GetsDate;
                                CommonClass.SessionRunning = true;
                                if (CommonClass.AutoEnd)
                                {
                                    EndSessionCountDown.Enabled = true;
                                    EndSessionCountDown.Start();
                                }
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public float GetFloatFund()
        {
            DataTable SessionDt = new DataTable();
            string sessionSql = @"SELECT TOP 1 FloatFund,OpeningFund  FROM Sessions WHERE SessionKey = @MacAddress ORDER BY SessionID DESC";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@MacAddress", TerminalID);
            CommonClass.runSql(ref SessionDt, sessionSql, param);
            if (SessionDt.Rows.Count > 0)
            {
                DataRow dr = SessionDt.Rows[0];
                CommonClass.OpeningFund = float.Parse(dr["FloatFund"].ToString());
                return float.Parse(dr["FloatFund"].ToString());

            }
            return 0;
        }

        public bool CheckNoofTerminals()
        {
            DataTable SessionDt = new DataTable();
            string sessionSql = @"SELECT SessionStatus From Sessions WHERE SessionStatus = @SessionStatus ";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionStatus", "Open");
            CommonClass.runSql(ref SessionDt, sessionSql, param);
            if (CommonClass.MaxTerminalAllowed == SessionDt.Rows.Count && CommonClass.MaxTerminalAllowed != -1)
            {
                MessageBox.Show("You have reach the maximum number of terminals allowed in your License. \n For more information contact our Customer Service", "License Warning!");

                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckPreference()
        {
            DataTable SessionDt = new DataTable();
            string sessionSql = @"SELECT * From Preference";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionStatus", "Open");
            CommonClass.runSql(ref SessionDt, sessionSql, param);
            if (SessionDt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Setup Preference ", "License Warning!");

                return false;
            }
        }

        private void tspSessionReport_Click(object sender, EventArgs e)
        {
            string lstaccesssql = "SELECT date_lastaccess FROM Users WHERE user_id = " + CommonClass.UserID;

            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, lstaccesssql);
            if (dt.Rows.Count > 0)
            {
                string selectSql = @"SELECT j.*, 
                                        jb.JobCode 
                                    FROM Journal j
                                    LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                    WHERE TransactionNumber NOT LIKE 'SYS-%' and TransactionDate BETWEEN '" + dt.Rows[0]["date_lastaccess"].ToString() + "' AND GETUTCDATE()"
                                    + " ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";

                DataTable dtSession = new DataTable();
                CommonClass.runSql(ref dtSession, selectSql);

                Reports.ReportParams sessionjournalparams = new Reports.ReportParams();
                sessionjournalparams.PrtOpt = 1;
                sessionjournalparams.Rec.Add(dtSession);
                sessionjournalparams.ReportName = "SessionJournal.rpt";
                sessionjournalparams.RptTitle = "Session Journal";

                sessionjournalparams.Params = "compname";
                sessionjournalparams.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(sessionjournalparams);
            }
        }

        private void bestSellingItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommonClass.BestSellingItem == null || CommonClass.BestSellingItem.IsDisposed)
            {
                CommonClass.BestSellingItem = new Reports.InventoryReports.BestSellingItem();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.BestSellingItem.MdiParent = this.MdiParent;
            CommonClass.BestSellingItem.Show();
            CommonClass.BestSellingItem.Focus();
            if (CommonClass.BestSellingItem.DialogResult == DialogResult.Cancel)
            {
                CommonClass.BestSellingItem.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Display a MsgBox asking the user.
            if (MessageBox.Show("Do you want to quit?", "Information",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (btnSession.Text == "End Session")
                {
                    string sqlupdateTerminal = "UPDATE Sessions SET UserID = '0' WHERE SessionID = " + CommonClass.SessionID;
                    int i = CommonClass.runSql(sqlupdateTerminal);
                    if (i > 0)
                    {
                        Application.Exit();
                    }

                }
                // Call method to save file...
            }
            else
            {
                e.Cancel = true;
            }

        }
        private void terminalSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonClass.terminalSetup = new Utilities.TerminalSetup();

            this.Cursor = Cursors.WaitCursor;
            CommonClass.terminalSetup.MdiParent = this;
            CommonClass.terminalSetup.Show();
            CommonClass.terminalSetup.Focus();
            if (CommonClass.terminalSetup.DialogResult == DialogResult.Cancel)
            {
                CommonClass.terminalSetup.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void StartingBalances_Click(object sender, EventArgs e)
        {
            CommonClass.ARBalancesFrm = new ARBalances();

            this.Cursor = Cursors.WaitCursor;
            CommonClass.ARBalancesFrm.MdiParent = this;
            CommonClass.ARBalancesFrm.Show();
            CommonClass.ARBalancesFrm.Focus();
            if (CommonClass.ARBalancesFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ARBalancesFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void importItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonClass.itemImport = new Utilities.ItemImport();

            this.Cursor = Cursors.WaitCursor;
            CommonClass.itemImport.MdiParent = this;
            CommonClass.itemImport.Show();
            CommonClass.itemImport.Focus();
            if (CommonClass.itemImport.DialogResult == DialogResult.Cancel)
            {
                CommonClass.itemImport.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (btnSession.Text == "End Session")
            {
                string sqlupdateTerminal = "UPDATE Sessions SET UserID = '0' WHERE SessionID = " + CommonClass.SessionID;
                int i = CommonClass.runSql(sqlupdateTerminal);
                if (i > 0)
                {
                    Application.Exit();
                }

            }
        }

        private void sessionManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CommonClass.SessionManager = new Utilities.SessionManager();

            this.Cursor = Cursors.WaitCursor;
            CommonClass.SessionManager.MdiParent = this;
            CommonClass.SessionManager.Show();
            CommonClass.SessionManager.Focus();
            if (CommonClass.SessionManager.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SessionManager.Close();
            }
            this.Cursor = Cursors.Default;
        }
    }
}
