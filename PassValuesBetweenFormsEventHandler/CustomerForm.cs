using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PassValuesBetweenFormsEventHandler
{
    //public partial class CustomerForm : Form
    //{
    //    public CustomerForm()
    //    {
    //        InitializeComponent();
    //    }
    //}

    // Customer Form - NO MAIN FORM REFERENCE!
    public partial class CustomerForm : Form
    {
        private TextBox txtSpecificData;
        private TextBox txtCommonData;
        private ListBox lstReceivedData;

        public CustomerForm()
        {
            InitializeComponents();
            SubscribeToEvents();
        }

        private void InitializeComponents()
        {
            this.Text = "Customer Management";
            this.Size = new System.Drawing.Size(500, 380);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(100, 100);

            Label lblTitle = new Label
            {
                Text = "CUSTOMER FORM - Independent Subscriber",
                Location = new System.Drawing.Point(20, 15),
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                AutoSize = true
            };

            Label lblSpecific = new Label
            {
                Text = "Specific Data (Dataset 1):",
                Location = new System.Drawing.Point(20, 50),
                AutoSize = true
            };

            txtSpecificData = new TextBox
            {
                Location = new System.Drawing.Point(20, 75),
                Size = new System.Drawing.Size(440, 60),
                Multiline = true,
                ReadOnly = true,
                BackColor = System.Drawing.Color.LightCyan
            };

            Label lblCommon = new Label
            {
                Text = "Common Data (Dataset 4):",
                Location = new System.Drawing.Point(20, 145),
                AutoSize = true
            };

            txtCommonData = new TextBox
            {
                Location = new System.Drawing.Point(20, 170),
                Size = new System.Drawing.Size(440, 60),
                Multiline = true,
                ReadOnly = true,
                BackColor = System.Drawing.Color.LightYellow
            };

            Label lblHistory = new Label
            {
                Text = "Data Reception History:",
                Location = new System.Drawing.Point(20, 245),
                AutoSize = true
            };

            lstReceivedData = new ListBox
            {
                Location = new System.Drawing.Point(20, 270),
                Size = new System.Drawing.Size(440, 80)
            };

            this.Controls.AddRange(new Control[] {
            lblTitle, lblSpecific, txtSpecificData, lblCommon, txtCommonData,
            lblHistory, lstReceivedData
        });
        }

        private void SubscribeToEvents()
        {
            // Subscribe to the centralized event manager
            DataEventManager.SpecificDataSent += OnSpecificDataReceived;
            DataEventManager.CommonDataBroadcasted += OnCommonDataReceived;

            // Unsubscribe when form closes to prevent memory leaks
            this.FormClosed += (s, e) =>
            {
                DataEventManager.SpecificDataSent -= OnSpecificDataReceived;
                DataEventManager.CommonDataBroadcasted -= OnCommonDataReceived;
            };
        }

        private void OnSpecificDataReceived(object sender, SpecificDataEventArgs e)
        {
            // Only process if this data is for CustomerForm
            if (e.TargetFormType == "CustomerForm")
            {
                DisplaySpecificData(e.Data);
            }
        }

        private void OnCommonDataReceived(object sender, CommonDataEventArgs e)
        {
            DisplayCommonData(e.Data);
        }

        private void DisplaySpecificData(DataSet specificData)
        {
            if (specificData != null && specificData.Tables.Count > 0)
            {
                DataTable dt = specificData.Tables[0];
                string info = dt.Rows[0]["Information"].ToString();
                string time = dt.Rows[0]["Timestamp"].ToString();

                txtSpecificData.Text = $"Dataset: {specificData.DataSetName}\r\n" +
                                      $"Data: {info}\r\n" +
                                      $"Received: {time}";

                lstReceivedData.Items.Insert(0, $"[SPECIFIC] {time} - {info}");
            }
        }

        private void DisplayCommonData(DataSet commonData)
        {
            if (commonData != null && commonData.Tables.Count > 0)
            {
                DataTable dt = commonData.Tables[0];
                string info = dt.Rows[0]["Information"].ToString();
                string time = dt.Rows[0]["Timestamp"].ToString();

                txtCommonData.Text = $"Dataset: {commonData.DataSetName}\r\n" +
                                    $"Data: {info}\r\n" +
                                    $"Received: {time}";

                lstReceivedData.Items.Insert(0, $"[COMMON] {time} - {info}");
            }
        }
    }
}
