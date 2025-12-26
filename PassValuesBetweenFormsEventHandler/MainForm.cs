using System.Data;

namespace PassValuesBetweenFormsEventHandler
{
    //public partial class MainForm : Form
    //{
    //    public MainForm()
    //    {
    //        InitializeComponent();
    //    }
    //}

    // Main Form - the central data hub
    public partial class MainForm : Form
    {
        private TextBox txtDataset1;
        private TextBox txtDataset2;
        private TextBox txtDataset3;
        private TextBox txtCommonDataset;
        private Button btnSendToCustomer;
        private Button btnSendToReport;
        private Button btnSendToSettings;
        private Button btnBroadcastCommon;
        private Button btnOpenCustomerForm;
        private Button btnOpenReportForm;
        private Button btnOpenSettingsForm;
        private Label lblSubscriberCount;

        public MainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Main Form - Centralized Event Manager";
            this.Size = new System.Drawing.Size(550, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Dataset 1 - for Customer Form
            Label lbl1 = new Label
            {
                Text = "Dataset 1 (Customer Data):",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };

            txtDataset1 = new TextBox
            {
                Location = new System.Drawing.Point(20, 45),
                Size = new System.Drawing.Size(350, 20),
                Text = "John Doe, john@email.com, Active"
            };

            btnSendToCustomer = new Button
            {
                Text = "Send to Customer Form",
                Location = new System.Drawing.Point(380, 43),
                Size = new System.Drawing.Size(140, 25)
            };
            btnSendToCustomer.Click += BtnSendToCustomer_Click;

            // Dataset 2 - for Report Form
            Label lbl2 = new Label
            {
                Text = "Dataset 2 (Report Data):",
                Location = new System.Drawing.Point(20, 80),
                AutoSize = true
            };

            txtDataset2 = new TextBox
            {
                Location = new System.Drawing.Point(20, 105),
                Size = new System.Drawing.Size(350, 20),
                Text = "Sales, Q4 2024, $150000"
            };

            btnSendToReport = new Button
            {
                Text = "Send to Report Form",
                Location = new System.Drawing.Point(380, 103),
                Size = new System.Drawing.Size(140, 25)
            };
            btnSendToReport.Click += BtnSendToReport_Click;

            // Dataset 3 - for Settings Form
            Label lbl3 = new Label
            {
                Text = "Dataset 3 (Settings Data):",
                Location = new System.Drawing.Point(20, 140),
                AutoSize = true
            };

            txtDataset3 = new TextBox
            {
                Location = new System.Drawing.Point(20, 165),
                Size = new System.Drawing.Size(350, 20),
                Text = "Theme=Dark, Timeout=30, Notifications=True"
            };

            btnSendToSettings = new Button
            {
                Text = "Send to Settings Form",
                Location = new System.Drawing.Point(380, 163),
                Size = new System.Drawing.Size(140, 25)
            };
            btnSendToSettings.Click += BtnSendToSettings_Click;

            // Common Dataset - for all forms
            Label lblCommon = new Label
            {
                Text = "Dataset 4 (Common Data for ALL Forms):",
                Location = new System.Drawing.Point(20, 200),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold)
            };

            txtCommonDataset = new TextBox
            {
                Location = new System.Drawing.Point(20, 225),
                Size = new System.Drawing.Size(350, 20),
                Text = "Company: ABC Corp, Date: 2024-12-26, User: Admin"
            };

            btnBroadcastCommon = new Button
            {
                Text = "Broadcast to ALL Forms",
                Location = new System.Drawing.Point(380, 223),
                Size = new System.Drawing.Size(140, 25),
                BackColor = System.Drawing.Color.LightGreen
            };
            btnBroadcastCommon.Click += BtnBroadcastCommon_Click;

            // Subscriber count display
            lblSubscriberCount = new Label
            {
                Text = "Active Subscribers: 0",
                Location = new System.Drawing.Point(20, 255),
                AutoSize = true,
                ForeColor = System.Drawing.Color.Blue
            };

            // Timer to update subscriber count
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += (s, e) => UpdateSubscriberCount();
            timer.Start();

            // Separator
            Label lblSeparator = new Label
            {
                Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━",
                Location = new System.Drawing.Point(20, 280),
                AutoSize = true
            };

            // Open Forms Section
            Label lblOpenForms = new Label
            {
                Text = "Open Forms:",
                Location = new System.Drawing.Point(20, 310),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold)
            };

            btnOpenCustomerForm = new Button
            {
                Text = "Open Customer Form",
                Location = new System.Drawing.Point(20, 340),
                Size = new System.Drawing.Size(150, 35)
            };
            btnOpenCustomerForm.Click += (s, e) => new CustomerForm().Show();

            btnOpenReportForm = new Button
            {
                Text = "Open Report Form",
                Location = new System.Drawing.Point(190, 340),
                Size = new System.Drawing.Size(150, 35)
            };
            btnOpenReportForm.Click += (s, e) => new ReportForm().Show();

            btnOpenSettingsForm = new Button
            {
                Text = "Open Settings Form",
                Location = new System.Drawing.Point(360, 340),
                Size = new System.Drawing.Size(150, 35)
            };
            btnOpenSettingsForm.Click += (s, e) => new SettingsForm().Show();

            Label lblInfo = new Label
            {
                Text = "ℹ Forms subscribe to DataEventManager automatically",
                Location = new System.Drawing.Point(20, 390),
                AutoSize = true,
                ForeColor = System.Drawing.Color.Gray,
                Font = new System.Drawing.Font("Arial", 8)
            };

            this.Controls.AddRange(new Control[] {
            lbl1, txtDataset1, btnSendToCustomer,
            lbl2, txtDataset2, btnSendToReport,
            lbl3, txtDataset3, btnSendToSettings,
            lblCommon, txtCommonDataset, btnBroadcastCommon,
            lblSubscriberCount, lblSeparator, lblOpenForms,
            btnOpenCustomerForm, btnOpenReportForm, btnOpenSettingsForm, lblInfo
        });
        }

        private void UpdateSubscriberCount()
        {
            int commonSubs = DataEventManager.GetCommonDataSubscriberCount();
            int specificSubs = DataEventManager.GetSpecificDataSubscriberCount();
            lblSubscriberCount.Text = $"Active Subscribers: Common={commonSubs}, Specific={specificSubs}";
        }

        private DataSet CreateDataSet(string name, string data)
        {
            DataSet ds = new DataSet(name);
            DataTable dt = new DataTable("Data");
            dt.Columns.Add("Information", typeof(string));
            dt.Columns.Add("Timestamp", typeof(string));

            DataRow row = dt.NewRow();
            row["Information"] = data;
            row["Timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dt.Rows.Add(row);

            ds.Tables.Add(dt);
            return ds;
        }

        private void BtnSendToCustomer_Click(object sender, EventArgs e)
        {
            DataSet ds = CreateDataSet("CustomerDataset", txtDataset1.Text);
            DataEventManager.RaiseSpecificDataEvent(ds, "CustomerForm");
            MessageBox.Show("Dataset 1 event raised for Customer Form!", "Success");
        }

        private void BtnSendToReport_Click(object sender, EventArgs e)
        {
            DataSet ds = CreateDataSet("ReportDataset", txtDataset2.Text);
            DataEventManager.RaiseSpecificDataEvent(ds, "ReportForm");
            MessageBox.Show("Dataset 2 event raised for Report Form!", "Success");
        }

        private void BtnSendToSettings_Click(object sender, EventArgs e)
        {
            DataSet ds = CreateDataSet("SettingsDataset", txtDataset3.Text);
            DataEventManager.RaiseSpecificDataEvent(ds, "SettingsForm");
            MessageBox.Show("Dataset 3 event raised for Settings Form!", "Success");
        }

        private void BtnBroadcastCommon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCommonDataset.Text))
            {
                MessageBox.Show("Please enter common data first!", "Warning");
                return;
            }

            DataSet commonDs = CreateDataSet("CommonDataset", txtCommonDataset.Text);
            DataEventManager.RaiseCommonDataEvent(commonDs);

            int subscriberCount = DataEventManager.GetCommonDataSubscriberCount();
            MessageBox.Show($"Common dataset broadcasted to {subscriberCount} subscriber(s)!", "Success");
        }
    }
}
