using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PassValuesBetweenForms
{
    //public partial class CustomerForm : Form
    //{
    //    public CustomerForm()
    //    {
    //        InitializeComponent();
    //    }
    //}

    // Customer Form - manages customer operations
    public partial class CustomerForm : Form, IDataReceiver
    {
        private MainForm mainForm;
        private TextBox txtCustomerName;
        private TextBox txtCustomerEmail;
        private TextBox txtReceivedData;
        private Button btnSaveCustomer;
        private Button btnLoadFromMain;
        private ListBox lstCustomers;

        public CustomerForm(MainForm parent)
        {
            this.mainForm = parent;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Customer Management";
            this.Size = new System.Drawing.Size(450, 400);

            Label lblName = new Label
            {
                Text = "Customer Name:",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };

            txtCustomerName = new TextBox
            {
                Location = new System.Drawing.Point(20, 45),
                Size = new System.Drawing.Size(390, 20)
            };

            Label lblEmail = new Label
            {
                Text = "Email:",
                Location = new System.Drawing.Point(20, 75),
                AutoSize = true
            };

            txtCustomerEmail = new TextBox
            {
                Location = new System.Drawing.Point(20, 100),
                Size = new System.Drawing.Size(390, 20)
            };

            btnSaveCustomer = new Button
            {
                Text = "Save Customer",
                Location = new System.Drawing.Point(20, 130),
                Size = new System.Drawing.Size(120, 30)
            };
            btnSaveCustomer.Click += BtnSaveCustomer_Click;

            btnLoadFromMain = new Button
            {
                Text = "Load from Main",
                Location = new System.Drawing.Point(150, 130),
                Size = new System.Drawing.Size(120, 30)
            };
            btnLoadFromMain.Click += BtnLoadFromMain_Click;

            Label lblReceived = new Label
            {
                Text = "Data from Main Form:",
                Location = new System.Drawing.Point(20, 175),
                AutoSize = true
            };

            txtReceivedData = new TextBox
            {
                Location = new System.Drawing.Point(20, 200),
                Size = new System.Drawing.Size(390, 20),
                ReadOnly = true,
                BackColor = System.Drawing.Color.LightCyan
            };

            Label lblList = new Label
            {
                Text = "Saved Customers:",
                Location = new System.Drawing.Point(20, 235),
                AutoSize = true
            };

            lstCustomers = new ListBox
            {
                Location = new System.Drawing.Point(20, 260),
                Size = new System.Drawing.Size(390, 80)
            };

            this.Controls.AddRange(new Control[] {
            lblName, txtCustomerName, lblEmail, txtCustomerEmail,
            btnSaveCustomer, btnLoadFromMain, lblReceived, txtReceivedData,
            lblList, lstCustomers
        });
        }

        private void BtnSaveCustomer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                lstCustomers.Items.Add($"{txtCustomerName.Text} - {txtCustomerEmail.Text}");
                MessageBox.Show("Customer saved!", "Success");
                txtCustomerName.Clear();
                txtCustomerEmail.Clear();
            }
        }

        private void BtnLoadFromMain_Click(object sender, EventArgs e)
        {
            string data = mainForm.GetMainFormData();
            if (!string.IsNullOrEmpty(data))
            {
                txtCustomerName.Text = data;
            }
        }

        public void ReceiveDataFromMain(string data)
        {
            txtReceivedData.Text = $"Received: {data} at {DateTime.Now:HH:mm:ss}";
        }
    }
}
