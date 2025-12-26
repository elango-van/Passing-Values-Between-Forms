namespace PassValuesBetweenForms
{

    // Interface for forms that can receive data from main form
    public interface IDataReceiver
    {
        void ReceiveDataFromMain(string data);
    }

    // Main Form - the central data hub
    public partial class MainForm : Form
    {
        private TextBox txtData;
        private Button btnSendData;
        private Button btnOpenCustomerForm;
        private Button btnOpenReportForm;
        private Button btnOpenSettingsForm;
        private List<Form> openForms = new List<Form>();

        public MainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Main Form - Control Center";
            this.Size = new System.Drawing.Size(450, 250);

            Label lblInstruction = new Label
            {
                Text = "Enter data to broadcast to all open forms:",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };

            txtData = new TextBox
            {
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(390, 20)
            };

            btnSendData = new Button
            {
                Text = "Broadcast Data to All Forms",
                Location = new System.Drawing.Point(20, 80),
                Size = new System.Drawing.Size(200, 35)
            };
            btnSendData.Click += BtnSendData_Click;

            Label lblOpenForms = new Label
            {
                Text = "Open Forms:",
                Location = new System.Drawing.Point(20, 125),
                AutoSize = true
            };

            btnOpenCustomerForm = new Button
            {
                Text = "Customer Form",
                Location = new System.Drawing.Point(20, 155),
                Size = new System.Drawing.Size(120, 30)
            };
            btnOpenCustomerForm.Click += (s, e) => OpenForm(new CustomerForm(this));

            btnOpenReportForm = new Button
            {
                Text = "Report Form",
                Location = new System.Drawing.Point(150, 155),
                Size = new System.Drawing.Size(120, 30)
            };
            btnOpenReportForm.Click += (s, e) => OpenForm(new ReportForm(this));

            btnOpenSettingsForm = new Button
            {
                Text = "Settings Form",
                Location = new System.Drawing.Point(280, 155),
                Size = new System.Drawing.Size(120, 30)
            };
            btnOpenSettingsForm.Click += (s, e) => OpenForm(new SettingsForm(this));

            this.Controls.AddRange(new Control[] {
            lblInstruction, txtData, btnSendData, lblOpenForms,
            btnOpenCustomerForm, btnOpenReportForm, btnOpenSettingsForm
        });
        }

        private void OpenForm(Form form)
        {
            form.FormClosed += (s, e) => openForms.Remove(s as Form);
            openForms.Add(form);
            form.Show();
        }

        private void BtnSendData_Click(object sender, EventArgs e)
        {
            string dataToSend = txtData.Text;

            if (string.IsNullOrEmpty(dataToSend))
            {
                MessageBox.Show("Please enter some data first!", "Warning");
                return;
            }

            int count = 0;
            foreach (var form in openForms.ToList())
            {
                if (form is IDataReceiver receiver)
                {
                    receiver.ReceiveDataFromMain(dataToSend);
                    count++;
                }
            }

            MessageBox.Show($"Data broadcasted to {count} form(s)!", "Success");
        }

        public string GetMainFormData()
        {
            return txtData.Text;
        }
    }

}
