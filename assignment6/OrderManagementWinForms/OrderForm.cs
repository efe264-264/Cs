using Homework_5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManagementWinForms
{
    public partial class OrderForm : Form
    {
        public string CustomerName { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }

        public OrderForm(string customer = "", string product = "", decimal price = 0)
        {
            InitializeComponent();

            tbcustomer.Text = customer;
            tbproduct.Text = product;
            tbprice.Text = price.ToString();
        }

    
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbcustomer.Text) ||
                string.IsNullOrWhiteSpace(tbproduct.Text) ||
                !decimal.TryParse(tbprice.Text, out decimal price))
            {
                MessageBox.Show("填写格式有误。", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CustomerName = tbcustomer.Text.Trim();
            ProductName = tbproduct.Text.Trim();
            Price = price;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
