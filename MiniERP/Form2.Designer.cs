namespace MiniERP
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuotationNumber = new System.Windows.Forms.TextBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.txtWorkingDays = new System.Windows.Forms.TextBox();
            this.txtDeposit = new System.Windows.Forms.TextBox();
            this.txtFinalPayment = new System.Windows.Forms.TextBox();
            this.richTextBoxContract = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbCustomerCode = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "報價單編號";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "客戶編號";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "預計工作日";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "訂金";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(12, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "尾款";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(2, 235);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label6.Size = new System.Drawing.Size(108, 28);
            this.label6.TabIndex = 5;
            this.label6.Text = "報價單內容";
            // 
            // txtQuotationNumber
            // 
            this.txtQuotationNumber.Location = new System.Drawing.Point(127, 18);
            this.txtQuotationNumber.Name = "txtQuotationNumber";
            this.txtQuotationNumber.Size = new System.Drawing.Size(174, 29);
            this.txtQuotationNumber.TabIndex = 6;
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(329, 63);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(174, 29);
            this.txtCustomerCode.TabIndex = 7;
            this.txtCustomerCode.Visible = false;
            // 
            // txtWorkingDays
            // 
            this.txtWorkingDays.Location = new System.Drawing.Point(127, 108);
            this.txtWorkingDays.Name = "txtWorkingDays";
            this.txtWorkingDays.Size = new System.Drawing.Size(174, 29);
            this.txtWorkingDays.TabIndex = 8;
            // 
            // txtDeposit
            // 
            this.txtDeposit.Location = new System.Drawing.Point(127, 145);
            this.txtDeposit.Name = "txtDeposit";
            this.txtDeposit.Size = new System.Drawing.Size(174, 29);
            this.txtDeposit.TabIndex = 9;
            // 
            // txtFinalPayment
            // 
            this.txtFinalPayment.Location = new System.Drawing.Point(127, 190);
            this.txtFinalPayment.Name = "txtFinalPayment";
            this.txtFinalPayment.Size = new System.Drawing.Size(174, 29);
            this.txtFinalPayment.TabIndex = 10;
            // 
            // richTextBoxContract
            // 
            this.richTextBoxContract.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxContract.Location = new System.Drawing.Point(127, 235);
            this.richTextBoxContract.Name = "richTextBoxContract";
            this.richTextBoxContract.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.richTextBoxContract.Size = new System.Drawing.Size(500, 311);
            this.richTextBoxContract.TabIndex = 11;
            this.richTextBoxContract.Text = "";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(50, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(50, 10, 25, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 38);
            this.button1.TabIndex = 12;
            this.button1.Text = "儲存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(211, 10);
            this.button2.Margin = new System.Windows.Forms.Padding(25, 10, 25, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 38);
            this.button2.TabIndex = 13;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 545);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(627, 60);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // cbCustomerCode
            // 
            this.cbCustomerCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCustomerCode.FormattingEnabled = true;
            this.cbCustomerCode.Location = new System.Drawing.Point(127, 66);
            this.cbCustomerCode.Name = "cbCustomerCode";
            this.cbCustomerCode.Size = new System.Drawing.Size(174, 26);
            this.cbCustomerCode.TabIndex = 16;
            this.cbCustomerCode.SelectedIndexChanged += new System.EventHandler(this.cbCustomerCode_SelectedIndexChanged);
            this.cbCustomerCode.SelectedValueChanged += new System.EventHandler(this.cbCustomerCode_SelectedValueChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 605);
            this.Controls.Add(this.cbCustomerCode);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.richTextBoxContract);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQuotationNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCustomerCode);
            this.Controls.Add(this.txtWorkingDays);
            this.Controls.Add(this.txtFinalPayment);
            this.Controls.Add(this.txtDeposit);
            this.Controls.Add(this.label3);
            this.Name = "Form2";
            this.Text = "報價單";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtQuotationNumber;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.TextBox txtWorkingDays;
        private System.Windows.Forms.TextBox txtDeposit;
        private System.Windows.Forms.TextBox txtFinalPayment;
        private System.Windows.Forms.RichTextBox richTextBoxContract;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cbCustomerCode;
    }
}