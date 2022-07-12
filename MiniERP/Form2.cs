using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Transactions;
using System.Text.RegularExpressions;

using FatherForm.Dbs;
using FatherForm.Controls;
using FatherForm.Extensions;

namespace MiniERP
{
    public partial class Form2 : Form
    {
        public string QuotationNumber;
        public string CustomerCode;
        public string WorkingDays;
        public string Contract;
        public string Deposit;
        public string FinalPayment;

        public string mode;
        public BindingSource BindingSource;

        public IDbContext _dbContext;
        public string _SQLiteDbName;
        private DataTable dtCustomer;
        private bool _isFormLoad = false;
        public Form2()
        {
            InitializeComponent();            
        }
        /// <summary>
        /// 取消鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsNumber(string str) 
        {
            Regex rgDate = new Regex(@"\d+");
            var num = rgDate.Match(str).Groups.Cast<Match>().Select(r => r.Value).Where(r => r !="").FirstOrDefault();
            if(string.IsNullOrEmpty(num))
            {
                return false;
            }
            return true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            


            this.txtQuotationNumber.Text = QuotationNumber;
            this.txtCustomerCode.Text = CustomerCode;
            this.txtWorkingDays.Text = WorkingDays;
            this.txtDeposit.Text = Deposit;
            this.txtFinalPayment.Text = FinalPayment;
            this.richTextBoxContract.Text = Contract;

            dtCustomer = this._dbContext.Select("SELECT '' As CustomerCode UNION SELECT CustomerCode As CustomerCode FROM Customer");
            Console.WriteLine("");
            this.cbCustomerCode.LoadDataSoucre(dtCustomer, "CustomerCode");
            this.cbCustomerCode.DisplayMember = "CustomerCode";
            this.cbCustomerCode.ValueMember = "CustomerCode";
            Console.WriteLine("");

            var selectItem = dtCustomer.AsEnumerable().First(r => r.Field<string>("CustomerCode") == this.txtCustomerCode.Text);
            Console.WriteLine("");
            this.cbCustomerCode.SelectedValue = selectItem.ItemArray[0].ToString();
            
            Console.WriteLine("");

            if (mode == "update") 
            { 
                this.txtQuotationNumber.Enabled = false;
            }
            this._isFormLoad = true;
        }
        /// <summary>
        /// 儲存鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var quotationSQL = "SELECT * FROM Quotation";
            var dtQuotation = this._dbContext.Select(quotationSQL);

            //update多了textbox enabled false
            if(mode == "insert" &&
               dtQuotation.AsEnumerable().Any(r => r.Field<string>("QuotationNumber") == this.txtQuotationNumber.Text))
            {
                MessageBox.Show("報價單編號已經存在");
                return;
            }


            //valid PK不能重複
            //valid 數字
            if (!this.IsNumber(this.txtDeposit.Text))
            {
                MessageBox.Show("訂金欄位必需為數字");
                return;
            }
            if (!this.IsNumber(this.txtFinalPayment.Text))
            {
                MessageBox.Show("尾款欄位必需為數字");
                return;
            }

            //valid不能為空白
            var valids = new List<KeyValuePair<string, string>> 
            { 
                new KeyValuePair<string, string>("報價單編號不能為空白",this.txtQuotationNumber.Text),
                new KeyValuePair<string, string>("客戶編號不能為空白",this.txtCustomerCode.Text),
                new KeyValuePair<string, string>("預計工作日不能為空白",this.txtWorkingDays.Text),
                new KeyValuePair<string, string>("報價單內容不能為空白",this.richTextBoxContract.Text),
            };

            var valid = valids.Where(r => r.Value == "").FirstOrDefault();
            if (valid.Key != null)
            {
                MessageBox.Show(valid.Key);
                return;
            }

            //insert失敗通知資料庫
            //success roll back
            //close Form2

            string insert = string.Format(@"insert into [Quotation]
                                            ([QuotationNumber]
                                            ,[CustomerCode]
                                            ,[WorkingDays]
                                            ,[Deposit]
                                            ,[FinalPayment]

                                            ,[Contract]
                                            ,[LastChangeTime])
                                            values
                                            ('{0}','{1}','{2}','{3}','{4}','{5}','{6}');"
                                , this.txtQuotationNumber.Text
                                , this.txtCustomerCode.Text
                                , this.txtWorkingDays.Text
                                , this.txtDeposit.Text
                                , this.txtFinalPayment.Text

                                , this.richTextBoxContract.Text
                                , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                              );

            string del = String.Format(@"DELETE FROM Quotation WHERE QuotationNumber = '{0}'",this.QuotationNumber);

            if (mode == "insert")
            {
                this._dbContext.Excute(insert);
            }

            if (mode == "update")
            {
                var delInsertSQL = string.Format("{0};{1};", del, insert);
                this._dbContext.Excute(delInsertSQL);
            }

            if(this._dbContext.Err != "")
            {
                MessageBox.Show(this._dbContext.Err.ToDbClientError());
            }
            else
            {
                MessageBox.Show("資料庫寫入成功");
            }

            #region rollback refresh data

            this.txtQuotationNumber.Text = "";
            this.txtCustomerCode.Text = "";
            this.txtWorkingDays.Text = "";
            this.txtDeposit.Text = "";
            this.txtFinalPayment.Text = "";
            this.richTextBoxContract.Text = "";

            var selectSQL = @"SELECT [QuotationNumber]
                                                      ,[CustomerCode]
                                                      ,[WorkingDays]
                                                      ,[Contract]
                                                      ,[Deposit]
                                                      ,[FinalPayment]
                                                  FROM [Quotation]";

            System.Data.DataTable quotationData = this._dbContext.Select(selectSQL);

            BindingSource.DataSource = quotationData;

            this.Close(); 
            #endregion
        }

        private void cbCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbCustomerCode_SelectedValueChanged(object sender, EventArgs e)
        {
            if(!this._isFormLoad)
            {
                return;
            }
            this.txtCustomerCode.Text = (string)this.cbCustomerCode.SelectedValue;
             
            //var selectIdx = dtCustomer.AsEnumerable().Select((r, idx) => {
            //    var selectItem = (System.Data.DataRowView)this.cbCustomerCode.SelectedItem;
            //    var row = selectItem.Row.Field<string>("CustomerCode");
            //    var tmpIdx = (r.Field<string>("CustomerCode") == row) ? idx : 0;
            //    return tmpIdx;
            //}).First();
            //Console.WriteLine("");
            //this.cbCustomerCode.SelectedIndex = selectIdx;
        }
    }
}
