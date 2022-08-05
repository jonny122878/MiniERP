using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Transactions;
using FatherForm.Dbs;
using FatherForm.Controls;
using FatherForm.Extensions;
using Microsoft.Office.Interop.Word;
using System.Configuration;
namespace MiniERP
{
    public partial class Form1 : Form
    {
        private IDbContext _dbContext;
        private IDbContext _dbMSSQLContext;
        private string _DbName;
        private string _SQLiteDbName;
        private string _customerSQL;
        private string _quotationSQL;
        private List<string> _delCodes = new List<string>();
        private List<KeyValuePair<string,string>> _editCodes = new List<KeyValuePair<string, string>>();
        private string _tmpCode;
        private int _tmpIdx;
        public Form1()
        {
            InitializeComponent();
            //原始
            //this.tabPage1.Size = new System.Drawing.Size(830, 433);
            //控制this.tabControl1.Controls、this.bindingNavigator1.Items大小
            this.tabControl1.Font = new System.Drawing.Font("新細明體", 14f);
            this.bindingNavigator1.Font = new System.Drawing.Font("新細明體", 14f);
            this.bindingNavigator2.Font = new System.Drawing.Font("新細明體", 14f);



            this._DbName = "MiniERP";
            this._SQLiteDbName = this._DbName + ".sqlite";
            var testDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._SQLiteDbName);
            this._dbContext = new SQLiteDbContext(testDb);
            //this._dbContext = new SQLiteDbContext(this._SQLiteDbName);
            //this._dbMSSQLContext = new MSSQLDbContext("LAPTOP-H1031HR7\\SQLEXPRESS", this._DbName);
            this._customerSQL = @"SELECT [CustomerCode]
                                        ,[Customer]
                                        ,[Telephone]
                                        ,[Address]
                                        ,[Email]
                                        ,[LastChangeTime]
                                    FROM [Customer]";
            this._quotationSQL = @"SELECT [QuotationNumber]
                                        ,[CustomerCode]
                                        ,[WorkingDays]
                                        ,[Contract]
                                        ,[Deposit]
                                        ,[FinalPayment]
                                    FROM [Quotation]";
        }

        private void RollBackMSSQL()
        {
            #region old convert SQL
            var customerSQL = @"SELECT [CustomerCode]
                                      ,[Customer]
                                      ,[Telephone]
                                      ,[Address]
                                      ,[Email]
                                      ,[LastChangeTime]
                                  FROM [Customer]";

            var dtCustomer = this._dbMSSQLContext.Select(customerSQL);
            MessageBox.Show(this._dbMSSQLContext.Err);
            var quotationSQL = @"SELECT [QuotationNumber]
                                      ,[CustomerCode]
                                      ,[WorkingDays]
                                      ,[Contract]
                                      ,[Deposit]
                                      ,[FinalPayment]
                                      ,[LastChangeTime]
                                  FROM [Quotation]";
            var dtQuotation = this._dbMSSQLContext.Select(quotationSQL);
            MessageBox.Show(this._dbMSSQLContext.Err);
            Console.Write("");

            //var createCustomer = @"CREATE TABLE [Customer](
            //                                     [CustomerCode] [char](6) NOT NULL,
            //                                     [Customer] [nvarchar](40) NOT NULL,
            //                                     [Telephone] [nvarchar](30) NOT NULL,
            //                                     [Address] [nvarchar](60) NOT NULL,
            //                                     [Email] [char](40) NOT NULL,
            //                                     [LastChangeTime] [datetime] NOT NULL,
            //                                    PRIMARY KEY  
            //                                    (
            //                                     [CustomerCode] ASC
            //                                    ))";

            //var createQuotation = @"CREATE TABLE [Quotation](
            //                                         [QuotationNumber] [char](10) NOT NULL,
            //                                         [CustomerCode] [char](6) NOT NULL,
            //                                         [WorkingDays] [nvarchar](40) NOT NULL,
            //                                         [Contract] [text] NOT NULL,
            //                                         [Deposit] [int] NOT NULL,
            //                                         [FinalPayment] [int] NOT NULL,
            //                                         [LastChangeTime] [datetime] NOT NULL,
            //                                         CONSTRAINT [PK__Quotatio] PRIMARY KEY  
            //                                        (
            //                                         [QuotationNumber] ASC
            //                                        ))";

            var createCustomer = @"DELETE FROM [Customer]";
            var createQuotation = @"DELETE FROM [Quotation]";
            this._dbContext.Excute(createCustomer);
            MessageBox.Show(this._dbContext.Err);
            this._dbContext.Excute(createQuotation);
            MessageBox.Show(this._dbContext.Err);
            Console.Write("");

            foreach (var row in dtCustomer.AsEnumerable())
            {
                var insertSQL = string.Format(@"INSERT INTO Customer([CustomerCode]
                                                                    ,[Customer]
                                                                    ,[Telephone]
                                                                    ,[Address]
                                                                    ,[Email]
                                                                    ,[LastChangeTime]) 
                                                            VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",
                                                            row.Field<string>("CustomerCode")
                                                            , row.Field<string>("Customer")
                                                            , row.Field<string>("Telephone")
                                                            , row.Field<string>("Address")
                                                            , row.Field<string>("Email")
                                                            , row.Field<DateTime>("LastChangeTime").ToString("yyyy-MM-dd HH:mm:ss"));
                this._dbContext.Excute(insertSQL);
            }

            foreach (var row in dtQuotation.AsEnumerable())
            {
                var insertSQL = string.Format(@"INSERT INTO Quotation([QuotationNumber]
                                                                    ,[CustomerCode]
                                                                    ,[WorkingDays]
                                                                    ,[Contract]
                                                                    ,[Deposit]
                                                                    ,[FinalPayment]
                                                                    ,[LastChangeTime]) 
                                                            VALUES ('{0}','{1}','{2}','{3}',{4},{5},'{6}')",
                                                            row.Field<string>("QuotationNumber")
                                                            , row.Field<string>("CustomerCode")
                                                            , row.Field<string>("WorkingDays")
                                                            , row.Field<string>("Contract")
                                                            , row.Field<int>("Deposit").ToString()
                                                            , row.Field<int>("FinalPayment").ToString()
                                                            , row.Field<DateTime>("LastChangeTime").ToString("yyyy-MM-dd HH:mm:ss"));
                this._dbContext.Excute(insertSQL);
            }


            Console.Write("");

            var dtSQLiteCustomer = this._dbContext.Select(customerSQL);
            var dtSQLiteQuotation = this._dbContext.Select(quotationSQL);

            Console.Write("");

            #endregion
        }

        private void RollBackSQLite()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.RollBackMSSQL();
            //if (ConfigurationManager.AppSettings["IsTest"] == "1")
            //{
            //    this.RollBackMSSQL();
            //}
            System.Data.DataTable customerData = this._dbContext.Select(this._customerSQL);
            this.bindingSource1.DataSource = customerData;
            System.Data.DataTable quotationData = this._dbContext.Select(this._quotationSQL);
            this.bindingSource2.DataSource = quotationData;

            this.bindingNavigator1.BindingSource = this.bindingSource1;
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Columns[5].ReadOnly = true;
            this.bindingNavigator2.BindingSource = this.bindingSource2;
            this.dataGridView2.DataSource = this.bindingSource2;


            //dataGridView1格式調整
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            }






        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.ToString() == "TabPage: {" + tabPage2.Text + "}")
            {
                
            }

            if (this.tabControl1.SelectedTab.ToString() == "TabPage: {" + tabPage3.Text + "}")
            {
                this.toolStripButton3_Click(null,null);

                //dataGridView2格式調整
                for (int i = 0; i < this.dataGridView2.Columns.Count; i++)
                {
                    this.dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                }


            }


        }

        
        /// <summary>
        /// 建立報價單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.BindingSource = this.bindingSource2;
            form2._SQLiteDbName = this._SQLiteDbName;
            form2._dbContext = this._dbContext;
            form2.mode = "insert";
            form2.ShowDialog();
            
            Console.WriteLine("");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.toolStripButton2.Visible = true;
            this.toolStripButton3.Visible = true;
            this.bindingNavigatorAddNewItem.Visible = true;
            this.bindingNavigatorDeleteItem.Visible = true;
            this.dataGridView1.ReadOnly = false;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Columns[5].ReadOnly = true;


            //var focus = dataGridView1.CurrentCell.RowIndex;
            //this.dataGridView1.Rows[focus].ReadOnly = false;

            //this.dataGridView1.Rows[1].ReadOnly = true;
            //this.dataGridView1.Rows[2].ReadOnly = true;


            Console.WriteLine();

        }

        //取消報價單變更

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.toolStripButton2.Visible = false;
            this.toolStripButton3.Visible = false;
            this.bindingNavigatorAddNewItem.Visible = false;
            this.bindingNavigatorDeleteItem.Visible = false;
            this.dataGridView1.ReadOnly = true;

            
            System.Data.DataTable customerData = this._dbContext.Select(this._customerSQL);
            this.bindingSource1.DataSource = customerData;
            this.dataGridView1.DataSource = this.bindingSource1.DataSource;
            this._delCodes.Clear();
        }

        /// <summary>
        /// 儲存報價單變更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            

            //string strTest = null;
            //object obTest = strTest;

            //string result = strTest.ToString();
            Console.WriteLine("");
            this.bindingSource1.EndEdit();
            var dialogResult = CannedMessage.DoubleCheck("是否儲存變更?");           

            System.Data.DataTable dtUpdate = (System.Data.DataTable)this.bindingSource1.DataSource;

            dtUpdate.AcceptChanges();
            #region 防呆資料有含空白
            var rows = dtUpdate.AsEnumerable().Select((r, idx) =>
            {
                //take5 最後變更時間不檢查
                var lists = r.ItemArray.Take(5).Select(c =>
                {
                    var val = (c == null) ? "" : c.ToString();
                    return val;
                }).ToList();
                return new Tuple<int, List<string>>(idx + 1, lists);
            }).ToList();
            var validRows = rows.FirstOrDefault(c => c.Item2.Any(cc => cc == ""));

            if (validRows != null)
            {
                MessageBox.Show("第" + validRows.Item1.ToString() + "列含有空白資料");
                return;
            }

            #endregion
            Console.WriteLine("");
            #region 防呆客戶編號為有重複值
            if (dtUpdate.AsEnumerable().GroupBy(r => r.Field<string>("CustomerCode"))
                    .Any(r => r.Count() > 1)
                    )
            {
                var customerID = dtUpdate.AsEnumerable().GroupBy(r => r.Field<string>("CustomerCode")).
                Where(r => r.Count() > 1).First().Key;
                MessageBox.Show(customerID + "有重複值");
                return;
            }
            #endregion
            Console.WriteLine("");


            #region 更新報價單SQL
            var groupQuotationSQL = string.Format(@"SELECT CustomerCode FROM Quotation GROUP BY CustomerCode");
            var groupQuotations = this._dbContext.Select(groupQuotationSQL).AsEnumerable().Select(r => r.Field<string>("CustomerCode"));

            var updateCodes = groupQuotations.Join(this._editCodes,
                inner => inner,
                outer => outer.Key,
                (inner, outer) => outer).ToList();

            var updateSQL = updateCodes.Select(r =>
            {
                var tmpSQL = string.Format(@"UPDATE Quotation 
                                            SET CustomerCode = '{0}' 
                                            WHERE CustomerCode = '{1}'", r.Value, r.Key);
                return tmpSQL;
            }).Aggregate(new StringBuilder(), (cur, next) => cur.Append(next).Append(";")).ToString();
            //updateSQL = updateSQL.Substring(0, updateSQL.Length - 1); 
            #endregion

            Console.WriteLine("");

            #region 找出要更新資料
            var customerSQL = "SELECT * FROM Customer";
            var dtCustomer = this._dbContext.Select(customerSQL);
            var dbCustomers = dtCustomer.AsEnumerable().
                Select(r => new CustomerModel
                {
                    Customer = r.Field<string>("Customer"),
                    CustomerCode = r.Field<string>("CustomerCode"),
                    Address = r.Field<string>("Address"),
                    Email = r.Field<string>("Email"),
                    Telephone = r.Field<string>("Telephone"),
                }).ToList();


            var updateCustomers = dtUpdate.AsEnumerable().
                Select(r => new CustomerModel
                {
                    Customer = r.Field<string>("Customer"),
                    CustomerCode = r.Field<string>("CustomerCode"),
                    Address = r.Field<string>("Address"),
                    Email = r.Field<string>("Email"),
                    Telephone = r.Field<string>("Telephone"),
                }).ToList();
            
            var exceptCustomers = updateCustomers.Except(dbCustomers, new CustomerCompare()).ToList();
            //if (!exceptCustomers.Any())
            //{
            //    MessageBox.Show("資料無做任何異動");
            //    return;
            //} 
            #endregion
            Console.WriteLine("");

            #region 轉成要寫入資料
            System.Data.DataTable dtInsert = new System.Data.DataTable();
            DataColumn[] dataColumns = new DataColumn[]
            {
                new DataColumn("CustomerCode",typeof(string)),
                new DataColumn("Customer",typeof(string)),
                new DataColumn("Telephone",typeof(string)),
                new DataColumn("Address",typeof(string)),
                new DataColumn("Email",typeof(string)),
                new DataColumn("LastChangeTime",typeof(string)),
            };
            dtInsert.Columns.AddRange(dataColumns);
            var rowInserts = exceptCustomers.Select(r =>
            {
                object[] arr = new object[6];
                arr[0] = r.CustomerCode;
                arr[1] = r.Customer;
                arr[2] = r.Telephone;
                arr[3] = r.Address;
                arr[4] = r.Email;

                arr[5] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return arr;
            }).ToList();

            rowInserts.ForEach(r => { dtInsert.Rows.Add(r); });
            #endregion

            Console.WriteLine("");

            #region 組合刪除SQL

            var deleteCustCodes = dbCustomers.Except(updateCustomers, new CustomerCodeCompare())
                .Select(r => r.CustomerCode).ToList();

            var editCustCodes = exceptCustomers.Select(x => x.CustomerCode).ToList();
            var codes = deleteCustCodes.Concat(editCustCodes).ToList();

            Console.WriteLine("");

            var delCustomerCode = codes.Aggregate(new StringBuilder(),
                (cur, next) => cur.Append("'").Append(next).Append("'").Append(",")).ToString();
            delCustomerCode = delCustomerCode.Substring(0, delCustomerCode.Length -1);
            //刪除組合出要刪掉標的
            var deleteAllSQL = @"Delete FROM [Customer] WHERE CustomerCode IN (" + delCustomerCode + ");";            
            #endregion

            Console.Write(deleteAllSQL);

            #region 寫入資料庫
            //更新欄位SQL
            deleteAllSQL = deleteAllSQL + updateSQL;
            this._dbContext.ExcuteBulkCopy(deleteAllSQL,
                    new List<Tuple<string, System.Data.DataTable>>
                    {
                    new Tuple<string, System.Data.DataTable>("Customer",dtInsert)
                    });

            if (this._dbContext.Err != "")
            {
                MessageBox.Show(this._dbContext.Err.ToDbClientError());
            }
            else
            {
                MessageBox.Show("資料庫寫入成功");
            }

            #endregion
            Console.WriteLine("");

            #region rollback 重新載入datagridview
            this.toolStripButton3_Click(null,null);
            System.Data.DataTable quotationData = this._dbContext.Select(this._quotationSQL);
            this.bindingSource2.DataSource = quotationData;            
            this.bindingNavigator2.BindingSource = this.bindingSource2;
            this.dataGridView2.DataSource = this.bindingSource2;

            #endregion
            Console.WriteLine("");
            

        }
        /// <summary>
        /// 報價單編輯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            var foucsCell = this.dataGridView2.CurrentCell;

            Form2 form2 = new Form2();
            form2.QuotationNumber = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();
            form2.CustomerCode = this.dataGridView2.CurrentRow.Cells[1].Value.ToString();
            form2.WorkingDays = this.dataGridView2.CurrentRow.Cells[2].Value.ToString();
            form2.Contract = this.dataGridView2.CurrentRow.Cells[3].Value.ToString();
            form2.Deposit = this.dataGridView2.CurrentRow.Cells[4].Value.ToString();
            form2.FinalPayment = this.dataGridView2.CurrentRow.Cells[5].Value.ToString();

            form2.BindingSource = this.bindingSource2;
            form2.mode = "update";

            form2._SQLiteDbName = this._SQLiteDbName;
            form2._dbContext = this._dbContext;

            form2.Show();
            




            Console.WriteLine();
        }

        private Dictionary<string,string> GetWordKeyValues() 
        {
            //處理Word字串替換
            var selectSQL = string.Format(@"  SELECT C.Address,C.Customer,C.Telephone,C.Email,Q.Contract,
                                                     Q.WorkingDays,Q.Deposit,Q.FinalPayment
                                                      FROM [Quotation] As Q
                                                      INNER JOIN [Customer] As C
                                                      ON Q.[CustomerCode] = C.CustomerCode
                                                      WHERE Q.CustomerCode = '{0}' AND Q.QuotationNumber = '{1}'",
                          this.dataGridView2.CurrentRow.Cells[1].Value.ToString(),
                          this.dataGridView2.CurrentRow.Cells[0].Value.ToString());

            var row = this._dbContext.Select(selectSQL).AsEnumerable().
                First().ItemArray.Select(r => r.ToString()).ToList();

            int total = int.Parse(row[6]) + int.Parse(row[7]);

            row.Add(Convert.ToString(total));

            Console.WriteLine();

            //var replaces = new List<string>()
            //    {
            //        @"[$Address$]",
            //        @"[$Customer$]",
            //        @"[$Telephone$]",
            //        @"[$Email$]",
            //        @"[$Contract$]",
            //        @"[$WorkingDays$]",
            //        @"[$Deposit$]",
            //        @"[$FinalPayment$]",
            //        @"[$Total$]"
            //    };
            var replaces = new List<string>()
                        {
                            "Address",
                            "Customer",
                            "Telephone",
                            "Email",
                            "Contract",
                            "WorkingDays",
                            "Deposit",
                            "FinalPayment",
                            "Total"
                        };
            var keyValues = replaces.Zip(row,
                (inner, outer) => new KeyValuePair<string, string>(inner, outer)).ToDictionary(r => r.Key, r => r.Value);
            return keyValues;
        }

        /// <summary>
        /// Convert to word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton8_Click(object sender, EventArgs e)
        {

            //檔案樣本複製新並將產出刪除

            //從datagridview和db讀取資料

            //輸出word
            string templateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "報價單樣品.docx");
            string saveFilePath = "";
            //Dialog命名文件
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Word 文件 (*.docx)|*.docx";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
     
            var keyValues = this.GetWordKeyValues();
            var docxBytes = keyValues.GenerateDocx(File.ReadAllBytes(templateFile));
            File.WriteAllBytes(saveFileDialog1.FileName,docxBytes);

            MessageBox.Show("報價單word檔製作完成");
        }
        /// <summary>
        /// Convert to Pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //檔案樣本複製新並將產出刪除

            //從datagridview和db讀取資料

            //輸出word

            //copy word to pdf

            //delete word
            string templateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "報價單樣品.docx");
            
            //Dialog命名文件
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF document(*.pdf) | *.pdf";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //檔名特別處理成word
            
            var pdfPath = Path.GetDirectoryName(saveFileDialog1.FileName);
            var pdfFileNotExtension = "temp";
            string saveFile = Path.Combine(pdfPath,pdfFileNotExtension + ".docx");

            var keyValues = this.GetWordKeyValues();
            var docxBytes = keyValues.GenerateDocx(File.ReadAllBytes(templateFile));
            File.WriteAllBytes(saveFile, docxBytes);

            
            //建立 word application instance
            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
            //開啟 word 檔案
            var wordDocument = appWord.Documents.Open(saveFile);
            //匯出為 pdf
            wordDocument.ExportAsFixedFormat(saveFileDialog1.FileName, WdExportFormat.wdExportFormatPDF);
            //關閉 word 檔
            wordDocument.Close();
            //結束 word
            appWord.Quit();

            File.Delete(saveFile);

            MessageBox.Show("報價單pdf檔製作完成");

        }
        /// <summary>
        /// 刪除報價單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = CannedMessage.DoubleCheck(
                string.Format(@"是否要刪除{0}報價單?"
                              , this.dataGridView2.CurrentRow.Cells[0].Value.ToString())
                );

            if (dialogResult == DialogResult.No)
            {
                return;
            }

            string del = string.Format(@"DELETE FROM [Quotation]
                                         WHERE QuotationNumber = '{0}';"                                         
                                         , this.dataGridView2.CurrentRow.Cells[0].Value.ToString());

            this._dbContext.Excute(del);

            if (this._dbContext.Err != "")
            {
                MessageBox.Show("刪除失敗");
                return;
            }

            
            System.Data.DataTable quotationData = this._dbContext.Select(this._quotationSQL);

            this.bindingSource2.DataSource = quotationData;

            MessageBox.Show("刪除成功");

        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form3 form3 = new Form3();
            form3.content = this.dataGridView2.CurrentCell.FormattedValue.ToString();
            form3.Show();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.bindingSource1.AddNew();
            this.bindingSource1.MoveLast();
        }
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
           
            //Console.Write("");
            //var test = (System.Windows.Forms.ToolStripButton)sender;
            //var rows = (System.Data.DataRowView)this.bindingSource1.Current;
            //var code = rows.Cells[0].ToString();
            this._delCodes.Add(this._tmpCode);
            Console.Write("");
            System.Data.DataTable dtReport = this._dbContext.Select(this._quotationSQL);
            var delIncludeReports = this._delCodes.Where(r => r != "").Join(dtReport.AsEnumerable(),
                inner => inner,
                outer => outer.Field<string>("CustomerCode").ToString(),
                (inner, outer) => inner).
                Where(r => r == this._tmpCode).ToList();

            if (delIncludeReports.Any())
            {
                MessageBox.Show("客戶資料在報價單含有資料，無法刪除");    
                return;
            }
            else
            {
               
                this.bindingSource1.RemoveAt(this._tmpIdx);
            }

            Console.Write("");
        }

       
    

 
        /// <summary>
        /// 紀錄datagridview選取列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }
            this._tmpCode = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            this._tmpIdx = e.RowIndex;
            Console.Write("");
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            var editCode = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            this._editCodes.Add(new KeyValuePair<string, string>(this._tmpCode, editCode));
            Console.WriteLine("");
        }


    }

}



