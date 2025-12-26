using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PassValuesBetweenForms
{
    //public partial class ReportForm : Form
    //{
    //    public ReportForm()
    //    {
    //        InitializeComponent();
    //    }
    //}

    // Report Form - generates and displays reports
    public partial class ReportForm : Form, IDataReceiver
    {
        private MainForm mainForm;
        private ComboBox cboReportType;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerateReport;
        private TextBox txtReportOutput;
        private TextBox txtMainData;

        public ReportForm(MainForm parent)
        {
            this.mainForm = parent;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Report Generator";
            this.Size = new System.Drawing.Size(500, 450);

            Label lblType = new Label
            {
                Text = "Report Type:",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };

            cboReportType = new ComboBox
            {
                Location = new System.Drawing.Point(20, 45),
                Size = new System.Drawing.Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboReportType.Items.AddRange(new object[] { "Sales Report", "Customer Report", "Inventory Report" });
            cboReportType.SelectedIndex = 0;

            Label lblStart = new Label
            {
                Text = "Start Date:",
                Location = new System.Drawing.Point(20, 80),
                AutoSize = true
            };

            dtpStartDate = new DateTimePicker
            {
                Location = new System.Drawing.Point(20, 105),
                Size = new System.Drawing.Size(200, 20)
            };

            Label lblEnd = new Label
            {
                Text = "End Date:",
                Location = new System.Drawing.Point(250, 80),
                AutoSize = true
            };

            dtpEndDate = new DateTimePicker
            {
                Location = new System.Drawing.Point(250, 105),
                Size = new System.Drawing.Size(200, 20)
            };

            btnGenerateReport = new Button
            {
                Text = "Generate Report",
                Location = new System.Drawing.Point(20, 140),
                Size = new System.Drawing.Size(150, 30)
            };
            btnGenerateReport.Click += BtnGenerateReport_Click;

            Label lblMainData = new Label
            {
                Text = "Main Form Data:",
                Location = new System.Drawing.Point(20, 185),
                AutoSize = true
            };

            txtMainData = new TextBox
            {
                Location = new System.Drawing.Point(20, 210),
                Size = new System.Drawing.Size(440, 20),
                ReadOnly = true,
                BackColor = System.Drawing.Color.LightGreen
            };

            Label lblOutput = new Label
            {
                Text = "Report Output:",
                Location = new System.Drawing.Point(20, 245),
                AutoSize = true
            };

            txtReportOutput = new TextBox
            {
                Location = new System.Drawing.Point(20, 270),
                Size = new System.Drawing.Size(440, 120),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            this.Controls.AddRange(new Control[] {
            lblType, cboReportType, lblStart, dtpStartDate, lblEnd, dtpEndDate,
            btnGenerateReport, lblMainData, txtMainData, lblOutput, txtReportOutput
        });
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            string mainData = mainForm.GetMainFormData();
            txtReportOutput.Text = $"=== {cboReportType.Text} ===\r\n" +
                                  $"Period: {dtpStartDate.Value:yyyy-MM-dd} to {dtpEndDate.Value:yyyy-MM-dd}\r\n" +
                                  $"Main Form Context: {mainData}\r\n" +
                                  $"Generated at: {DateTime.Now}\r\n\r\n" +
                                  $"[Report data would appear here...]";
        }

        public void ReceiveDataFromMain(string data)
        {
            txtMainData.Text = $"Broadcast received: {data}";
        }
    }

}
