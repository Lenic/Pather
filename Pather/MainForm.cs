using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pather
{
    public partial class MainForm : Form
    {
        #region Public Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Protected Properties

        protected EnvironmentVariableTarget CurrentTarget
        {
            get { return (EnvironmentVariableTarget)Enum.Parse(typeof(EnvironmentVariableTarget), cmbTarget.SelectedItem.ToString()); }
        }

        #endregion Protected Properties

        #region Protected Methods

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            cmbTarget.SelectedIndex = 0;
            LoadData();
        }

        #endregion Protected Methods

        #region Private Methods

        private void cmbTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var data = Environment.GetEnvironmentVariable("Path", CurrentTarget)
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            txtContent.Text = string.Join(Environment.NewLine, data);

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void SaveData()
        {
            var data = txtContent.Lines.Select(p=>p.Trim()).Where(p=>!string.IsNullOrWhiteSpace(p));

            txtContent.Text = string.Join(Environment.NewLine, data);

            var content = string.Join(";", data);
            Environment.SetEnvironmentVariable("Path", string.Join(";", data), CurrentTarget);

            MessageBox.Show(this, "更新 Path 变量成功！");
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        #endregion Private Methods
    }
}
