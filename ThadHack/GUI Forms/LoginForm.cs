using System;
using System.Reflection;
using System.Windows.Forms;
using ZzukBot.Settings;

namespace ZzukBot.GUI_Forms
{
    internal partial class LoginForm : Form
    {
        internal string Email;
        internal string Password;

        internal LoginForm()
        {
            InitializeComponent();
            Text += " - ZzukBot " + Assembly.GetExecutingAssembly().GetName().Version;
            Focus();
            var defaultMail = OptionManager.LoadZzukLogin().Split(';')[0];
            userText.Text = defaultMail;
            if (OptionManager.SaveZzukPassword)
            {
                cbSavePassword.Checked = OptionManager.SaveZzukPassword;
                var defaultPassword = OptionManager.LoadZzukLogin().Split(';')[1];
                passText.Text = defaultPassword;
            }
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }


        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (userText.Text.Length < 1 || passText.Text.Length < 1)
            {
                return;
            }
            if (!userText.Text.Contains("@"))
            {
                MessageBox.Show("Please use your e-mail. Not your forum username. (Is it that hard to read?)");
                return;
            }
            Email = userText.Text.ToLower();
            Password = passText.Text;
            OptionManager.SaveZzukLogin(Email, Password, cbSavePassword.Checked);
            DialogResult = DialogResult.OK;
        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {
            AcceptButton = loginBtn;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}