namespace Homework_8
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
            btnsubmit = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            tbcustomer = new TextBox();
            tbproduct = new TextBox();
            tbprice = new TextBox();
            SuspendLayout();
            // 
            // btnsubmit
            // 
            btnsubmit.Location = new Point(233, 364);
            btnsubmit.Margin = new Padding(4, 4, 4, 4);
            btnsubmit.Name = "btnsubmit";
            btnsubmit.Size = new Size(205, 65);
            btnsubmit.TabIndex = 0;
            btnsubmit.Text = "提交";
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.Click += btnsubmit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(102, 174);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(62, 31);
            label1.TabIndex = 1;
            label1.Text = "客户";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(102, 90);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(62, 31);
            label2.TabIndex = 2;
            label2.Text = "产品";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(102, 278);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(62, 31);
            label3.TabIndex = 3;
            label3.Text = "单价";
            // 
            // tbcustomer
            // 
            tbcustomer.Location = new Point(300, 167);
            tbcustomer.Margin = new Padding(4, 4, 4, 4);
            tbcustomer.Name = "tbcustomer";
            tbcustomer.Size = new Size(224, 38);
            tbcustomer.TabIndex = 4;
            // 
            // tbproduct
            // 
            tbproduct.Location = new Point(300, 69);
            tbproduct.Margin = new Padding(4, 4, 4, 4);
            tbproduct.Name = "tbproduct";
            tbproduct.Size = new Size(224, 38);
            tbproduct.TabIndex = 5;
            // 
            // tbprice
            // 
            tbprice.Location = new Point(300, 274);
            tbprice.Margin = new Padding(4, 4, 4, 4);
            tbprice.Name = "tbprice";
            tbprice.Size = new Size(224, 38);
            tbprice.TabIndex = 6;
            // 
            // OrderForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(646, 474);
            Controls.Add(tbprice);
            Controls.Add(tbproduct);
            Controls.Add(tbcustomer);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnsubmit);
            Margin = new Padding(4, 4, 4, 4);
            Name = "OrderForm";
            Text = "OrderForm";
            ResumeLayout(false);
            PerformLayout();

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