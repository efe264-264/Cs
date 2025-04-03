namespace OrderManagementWinForms
{
    partial class OrderForm
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
            this.btnsubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbcustomer = new System.Windows.Forms.TextBox();
            this.tbproduct = new System.Windows.Forms.TextBox();
            this.tbprice = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnsubmit
            // 
            this.btnsubmit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnsubmit.Font = new System.Drawing.Font("宋体", 15F);
            this.btnsubmit.Location = new System.Drawing.Point(390, 532);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(271, 56);
            this.btnsubmit.TabIndex = 0;
            this.btnsubmit.Text = "生成";
            this.btnsubmit.UseVisualStyleBackColor = false;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PowderBlue;
            this.label1.Font = new System.Drawing.Font("宋体", 20F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(213, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "客户";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 20F);
            this.label2.Location = new System.Drawing.Point(213, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 54);
            this.label2.TabIndex = 2;
            this.label2.Text = "品名";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 20F);
            this.label3.Location = new System.Drawing.Point(213, 367);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 54);
            this.label3.TabIndex = 3;
            this.label3.Text = "价格";
            // 
            // tbcustomer
            // 
            this.tbcustomer.Font = new System.Drawing.Font("宋体", 20F);
            this.tbcustomer.Location = new System.Drawing.Point(390, 238);
            this.tbcustomer.Name = "tbcustomer";
            this.tbcustomer.Size = new System.Drawing.Size(537, 68);
            this.tbcustomer.TabIndex = 4;
            // 
            // tbproduct
            // 
            this.tbproduct.Font = new System.Drawing.Font("宋体", 20F);
            this.tbproduct.Location = new System.Drawing.Point(390, 117);
            this.tbproduct.Name = "tbproduct";
            this.tbproduct.Size = new System.Drawing.Size(537, 68);
            this.tbproduct.TabIndex = 5;
            // 
            // tbprice
            // 
            this.tbprice.Font = new System.Drawing.Font("宋体", 20F);
            this.tbprice.Location = new System.Drawing.Point(390, 353);
            this.tbprice.Name = "tbprice";
            this.tbprice.Size = new System.Drawing.Size(537, 68);
            this.tbprice.TabIndex = 6;
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 656);
            this.Controls.Add(this.tbprice);
            this.Controls.Add(this.tbproduct);
            this.Controls.Add(this.tbcustomer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnsubmit);
            this.Name = "OrderForm";
            this.Text = "OrderForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbcustomer;
        private System.Windows.Forms.TextBox tbproduct;
        private System.Windows.Forms.TextBox tbprice;
    }
}