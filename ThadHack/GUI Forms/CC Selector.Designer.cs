namespace ZzukBot.GUI_Forms
{
    partial class CC_Selector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CC_Selector));
            this.selectbutton = new System.Windows.Forms.Button();
            this.cclistbox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // selectbutton

            this.selectbutton.Location = new System.Drawing.Point(12, 126);
            this.selectbutton.Name = "selectbutton";
            this.selectbutton.Size = new System.Drawing.Size(118, 23);
            this.selectbutton.TabIndex = 0;
            this.selectbutton.Text = "Select Routine";
            this.selectbutton.UseVisualStyleBackColor = true;
            this.selectbutton.Click += new System.EventHandler(this.selectbutton_Click);

            // cclistbox

            this.cclistbox.FormattingEnabled = true;
            this.cclistbox.Location = new System.Drawing.Point(12, 12);
            this.cclistbox.Name = "cclistbox";
            this.cclistbox.Size = new System.Drawing.Size(118, 108);
            this.cclistbox.TabIndex = 1;

            // CC_Selector

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(142, 160);
            this.Controls.Add(this.cclistbox);
            this.Controls.Add(this.selectbutton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CC_Selector";
            this.Load += new System.EventHandler(this.CC_Selector_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button selectbutton;
        private System.Windows.Forms.ListBox cclistbox;
    }
}