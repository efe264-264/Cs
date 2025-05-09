using System.Windows.Forms;
using assignment8;
using System;


namespace OrderManagementSystem
{
    public partial class TransactionEntryForm : Form
    {
        public string ClientName { get; private set; }
        public string ArticleName { get; private set; }
        public decimal Subtotal { get; private set; }

        public TransactionEntryForm(Order transaction = null) : this(
            client: transaction?.ClientName ?? "",
            article: (transaction?.Items.Count > 0) ? transaction.Items[0].ArticleName : "",
            subtotal: (transaction?.Items.Count > 0) ? transaction.Items[0].Subtotal : 0
        )
        {
        }

        public TransactionEntryForm(string client = "", string article = "", decimal subtotal = 0)
        {
            InitializeComponent();
            InitializeFormData(client, article, subtotal);
        }

        private void InitializeFormData(string client, string article, decimal subtotal)
        {
            tbClient.Text = client;
            tbArticle.Text = article;
            tbSubtotal.Text = subtotal.ToString("F2");
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            decimal parsedSubtotal = 0;

            if (string.IsNullOrWhiteSpace(tbClient.Text) ||
                string.IsNullOrWhiteSpace(tbArticle.Text) ||
                !decimal.TryParse(tbSubtotal.Text, out parsedSubtotal))
            {
                MessageBox.Show("请注意格式及完整性",
                    "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClientName = tbClient.Text.Trim();
            ArticleName = tbArticle.Text.Trim();
            Subtotal = parsedSubtotal;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
