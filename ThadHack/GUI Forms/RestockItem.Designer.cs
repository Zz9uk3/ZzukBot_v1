namespace ZzukBot.GUI_Forms
{
    partial class FormRestockItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRestockItem));
            this.lItem = new System.Windows.Forms.Label();
            this.lStockUp = new System.Windows.Forms.Label();
            this.tbItemName = new System.Windows.Forms.TextBox();
            this.nudRestockUpTo = new System.Windows.Forms.NumericUpDown();
            this.bOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudRestockUpTo)).BeginInit();
            this.SuspendLayout();

            // lItem

            this.lItem.AutoSize = true;
            this.lItem.Location = new System.Drawing.Point(12, 9);
            this.lItem.Name = "lItem";
            this.lItem.Size = new System.Drawing.Size(30, 13);
            this.lItem.TabIndex = 0;
            this.lItem.Text = "Item:";

            // lStockUp

            this.lStockUp.AutoSize = true;
            this.lStockUp.Location = new System.Drawing.Point(12, 35);
            this.lStockUp.Name = "lStockUp";
            this.lStockUp.Size = new System.Drawing.Size(65, 13);
            this.lStockUp.TabIndex = 2;
            this.lStockUp.Text = "Stock up to:";

            // tbItemName

            this.tbItemName.Location = new System.Drawing.Point(91, 6);
            this.tbItemName.Name = "tbItemName";
            this.tbItemName.Size = new System.Drawing.Size(153, 20);
            this.tbItemName.TabIndex = 3;

            // nudRestockUpTo

            this.nudRestockUpTo.Location = new System.Drawing.Point(91, 33);
            this.nudRestockUpTo.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRestockUpTo.Name = "nudRestockUpTo";
            this.nudRestockUpTo.Size = new System.Drawing.Size(153, 20);
            this.nudRestockUpTo.TabIndex = 4;
            this.nudRestockUpTo.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});

            // bOk

            this.bOk.Location = new System.Drawing.Point(91, 59);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(85, 28);
            this.bOk.TabIndex = 5;
            this.bOk.Text = "Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);

            // FormRestockItem

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 95);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.nudRestockUpTo);
            this.Controls.Add(this.tbItemName);
            this.Controls.Add(this.lStockUp);
            this.Controls.Add(this.lItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormRestockItem";
            this.ShowInTaskbar = false;
            this.Text = "RestockItem";
            ((System.ComponentModel.ISupportInitialize)(this.nudRestockUpTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lItem;
        private System.Windows.Forms.Label lStockUp;
        private System.Windows.Forms.Button bOk;
        internal System.Windows.Forms.TextBox tbItemName;
        internal System.Windows.Forms.NumericUpDown nudRestockUpTo;
    }
}