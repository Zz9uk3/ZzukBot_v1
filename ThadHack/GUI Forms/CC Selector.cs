using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ZzukBot.Engines.CustomClass;

namespace ZzukBot.GUI_Forms
{
    internal partial class CC_Selector : Form
    {
        internal CC_Selector(List<CustomClass> ccs)
        {
            InitializeComponent();
            foreach (var cc in ccs)
            {
                cclistbox.Items.Add(cc.CustomClassName);
            }
        }

        private void CC_Selector_Load(object sender, EventArgs e)
        {
        }

        private void selectbutton_Click(object sender, EventArgs e)
        {
            if (!cclistbox.SelectedIndex.Equals(null) && cclistbox.SelectedItems.Count == 1)
            {
                CCManager.selectedCC = cclistbox.SelectedItem.ToString();
                Dispose();
            }
        }
    }
}