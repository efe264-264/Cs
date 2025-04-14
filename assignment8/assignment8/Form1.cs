// Form1.cs
using Homework_8;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OrderManagementSystem
{
    public partial class MainInterface : Form
    {
        private readonly TransactionService _transactionService = new TransactionService();
        private DataGridViewTextBoxColumn? colTransactionId, colClient, colArticle, colTotal;

        public MainInterface()
        {
            InitializeComponent();
            InitializeDataGridView();
            RefreshTransactionList();
        }

        private void InitializeDataGridView()
        {
            dataGridViewTransactions.AutoGenerateColumns = false;
            dataGridViewTransactions.Columns.Clear();

            colTransactionId = new DataGridViewTextBoxColumn
            {
                Name = "colTransactionId",
                DataPropertyName = "OrderId",
                HeaderText = "交易编号"
            };
            colClient = new DataGridViewTextBoxColumn
            {
                Name = "colClient",
                DataPropertyName = "ClientName",
                HeaderText = "客户名称"
            };
            colArticle = new DataGridViewTextBoxColumn
            {
                Name = "colArticle",
                HeaderText = "商品明细",
                Width = 180
            };
            colTotal = new DataGridViewTextBoxColumn
            {
                Name = "colTotal",
                HeaderText = "交易总额",
                ValueType = typeof(decimal)
            };

            dataGridViewTransactions.Columns.AddRange(
                colTransactionId, colClient, colArticle, colTotal);
        }

        private void UpdateTransactionGrid(IEnumerable<Order> transactions)
        {
            dataGridViewTransactions.Rows.Clear();

            foreach (var transaction in transactions)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridViewTransactions);

                row.Cells[colTransactionId!.Index].Value = transaction.OrderId;
                row.Cells[colClient!.Index].Value = transaction.ClientName;
                row.Cells[colArticle!.Index].Value = GetArticlesString(transaction.Items);
                row.Cells[colTotal!.Index].Value = transaction.Total;

                row.Tag = transaction.OrderId;
                dataGridViewTransactions.Rows.Add(row);
            }
        }

        private string GetArticlesString(IEnumerable<OrderItem> items) =>
            string.Join(", ", items.Select(i => $"{i.ArticleName}({i.Subtotal:C})"));

        private void RefreshTransactionList() =>
            UpdateTransactionGrid(_transactionService.GetAllTransactions());

        private void btnCreateTransaction_Click(object sender, EventArgs e)
        {
            using var form = new TransactionEntryForm("", "", 0);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newTransaction = new Order
                    {
                        ClientName = form.ClientName,
                        Items = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                ArticleName = form.ArticleName,
                                Subtotal = form.Subtotal
                            }
                        }
                    };

                    _transactionService.CreateTransaction(newTransaction);
                    RefreshTransactionList();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }
            }
        }

        private void btnModifyTransaction_Click(object sender, EventArgs e)
        {
            if (dataGridViewTransactions.SelectedRows.Count == 0) return;

            var transactionId = (int)dataGridViewTransactions.SelectedRows[0].Tag!;
            var transaction = _transactionService.GetAllTransactions().First(t => t.OrderId == transactionId);

            using var form = new TransactionEntryForm(transaction);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _transactionService.ModifyTransaction(transactionId, updatedTransaction =>
                    {
                        updatedTransaction.ClientName = form.ClientName;
                        updatedTransaction.Items.Clear();
                        updatedTransaction.Items.Add(new OrderItem
                        {
                            ArticleName = form.ArticleName,
                            Subtotal = form.Subtotal
                        });
                    });

                    RefreshTransactionList();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }
            }
        }

        private void btnRemoveTransaction_Click(object sender, EventArgs e)
        {
            if (dataGridViewTransactions.SelectedRows.Count == 0) return;

            var transactionId = (int)dataGridViewTransactions.SelectedRows[0].Tag!;
            if (MessageBox.Show("确认删除该交易记录？", "删除确认",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _transactionService.RemoveTransaction(transactionId);
                    RefreshTransactionList();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var query = txtSearch.Text.Trim();
            var results = string.IsNullOrEmpty(query)
                ? _transactionService.GetAllTransactions()
                : _transactionService.FilterTransactions(query);

            UpdateTransactionGrid(results);
        }

        private void ShowErrorMessage(Exception ex) =>
            MessageBox.Show(ex.Message, "操作异常",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _transactionService.Dispose();
            base.OnFormClosing(e);
        }
    }
}