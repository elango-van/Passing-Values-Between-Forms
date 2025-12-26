using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PassValuesBetweenForms
{
    //public partial class SettingsForm : Form
    //{
    //    public SettingsForm()
    //    {
    //        InitializeComponent();
    //    }
    //}

    // Settings Form - manages application settings
    public partial class SettingsForm : Form, IDataReceiver
    {
        private MainForm mainForm;
        private CheckBox chkEnableNotifications;
        private CheckBox chkAutoSave;
        private ComboBox cboTheme;
        private NumericUpDown nudTimeout;
        private Button btnApplySettings;
        private TextBox txtBroadcastData;
        private ListBox lstBroadcastHistory;

        public SettingsForm(MainForm parent)
        {
            this.mainForm = parent;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Application Settings";
            this.Size = new System.Drawing.Size(450, 450);

            Label lblGeneral = new Label
            {
                Text = "General Settings:",
                Location = new System.Drawing.Point(20, 20),
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                AutoSize = true
            };

            chkEnableNotifications = new CheckBox
            {
                Text = "Enable Notifications",
                Location = new System.Drawing.Point(20, 50),
                AutoSize = true,
                Checked = true
            };

            chkAutoSave = new CheckBox
            {
                Text = "Auto-save every 5 minutes",
                Location = new System.Drawing.Point(20, 80),
                AutoSize = true
            };

            Label lblTheme = new Label
            {
                Text = "Theme:",
                Location = new System.Drawing.Point(20, 115),
                AutoSize = true
            };

            cboTheme = new ComboBox
            {
                Location = new System.Drawing.Point(20, 140),
                Size = new System.Drawing.Size(150, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboTheme.Items.AddRange(new object[] { "Light", "Dark", "Auto" });
            cboTheme.SelectedIndex = 0;

            Label lblTimeout = new Label
            {
                Text = "Session Timeout (minutes):",
                Location = new System.Drawing.Point(20, 175),
                AutoSize = true
            };

            nudTimeout = new NumericUpDown
            {
                Location = new System.Drawing.Point(20, 200),
                Size = new System.Drawing.Size(100, 20),
                Minimum = 5,
                Maximum = 120,
                Value = 30
            };

            btnApplySettings = new Button
            {
                Text = "Apply Settings",
                Location = new System.Drawing.Point(20, 235),
                Size = new System.Drawing.Size(120, 30)
            };
            btnApplySettings.Click += BtnApplySettings_Click;

            Label lblBroadcast = new Label
            {
                Text = "Latest Broadcast from Main:",
                Location = new System.Drawing.Point(20, 280),
                AutoSize = true
            };

            txtBroadcastData = new TextBox
            {
                Location = new System.Drawing.Point(20, 305),
                Size = new System.Drawing.Size(390, 20),
                ReadOnly = true,
                BackColor = System.Drawing.Color.LightYellow
            };

            Label lblHistory = new Label
            {
                Text = "Broadcast History:",
                Location = new System.Drawing.Point(20, 340),
                AutoSize = true
            };

            lstBroadcastHistory = new ListBox
            {
                Location = new System.Drawing.Point(20, 365),
                Size = new System.Drawing.Size(390, 60)
            };

            this.Controls.AddRange(new Control[] {
            lblGeneral, chkEnableNotifications, chkAutoSave, lblTheme, cboTheme,
            lblTimeout, nudTimeout, btnApplySettings, lblBroadcast, txtBroadcastData,
            lblHistory, lstBroadcastHistory
        });
        }

        private void BtnApplySettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Settings Applied!\n" +
                           $"Notifications: {chkEnableNotifications.Checked}\n" +
                           $"Auto-save: {chkAutoSave.Checked}\n" +
                           $"Theme: {cboTheme.Text}\n" +
                           $"Timeout: {nudTimeout.Value} min",
                           "Success");
        }

        public void ReceiveDataFromMain(string data)
        {
            txtBroadcastData.Text = data;
            lstBroadcastHistory.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} - {data}");
        }
    }

}
