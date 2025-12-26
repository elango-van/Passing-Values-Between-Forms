using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PassValuesBetweenForms
{
    //public partial class ChildForm : Form
    //{
    //    public ChildForm()
    //    {
    //        InitializeComponent();
    //    }
    //}

    // Child Form - receives data from main form
    public partial class ChildForm : Form
    {
        private MainForm mainForm;
        private TextBox txtReceivedData;
        private Button btnGetCurrentData;
        private ListBox lstDataHistory;

        public ChildForm(string formName, MainForm parent)
        {
            this.mainForm = parent;
            InitializeComponents(formName);
        }

        private void InitializeComponents(string formName)
        {
            this.Text = formName;
            this.Size = new System.Drawing.Size(400, 300);

            Label lblReceived = new Label
            {
                Text = "Received Data:",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };

            txtReceivedData = new TextBox
            {
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(340, 20),
                ReadOnly = true,
                BackColor = System.Drawing.Color.LightYellow
            };

            btnGetCurrentData = new Button
            {
                Text = "Get Current Data from Main Form",
                Location = new System.Drawing.Point(20, 80),
                Size = new System.Drawing.Size(220, 30)
            };
            btnGetCurrentData.Click += BtnGetCurrentData_Click;

            Label lblHistory = new Label
            {
                Text = "Data History:",
                Location = new System.Drawing.Point(20, 120),
                AutoSize = true
            };

            lstDataHistory = new ListBox
            {
                Location = new System.Drawing.Point(20, 150),
                Size = new System.Drawing.Size(340, 100)
            };

            this.Controls.AddRange(new Control[] {
            lblReceived, txtReceivedData, btnGetCurrentData, lblHistory, lstDataHistory
        });
        }

        // Method called by main form to send data
        public void ReceiveData(string data)
        {
            txtReceivedData.Text = data;
            lstDataHistory.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} - {data}");
        }

        private void BtnGetCurrentData_Click(object sender, EventArgs e)
        {
            //// Child form actively requests data from main form
            //string currentData = mainForm.GetCurrentData();

            //if (!string.IsNullOrEmpty(currentData))
            //{
            //    ReceiveData(currentData);
            //}
            //else
            //{
            //    MessageBox.Show("No data available in main form!", "Info");
            //}
        }
    }

}
