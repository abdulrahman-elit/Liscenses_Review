namespace Liscenses.MyCustomControl
{
    partial class Replace_Damged
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbReplace = new System.Windows.Forms.RadioButton();
            this.rbDamged = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.btnCtreat = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbReNew = new System.Windows.Forms.RadioButton();
            this.rbDetaind = new System.Windows.Forms.RadioButton();
            this.localLicense1 = new EntityLayer.Classes.LocalLicense();
            this.rbGloabl = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbReplace
            // 
            this.rbReplace.AutoSize = true;
            this.rbReplace.Checked = true;
            this.rbReplace.Location = new System.Drawing.Point(641, 21);
            this.rbReplace.Name = "rbReplace";
            this.rbReplace.Size = new System.Drawing.Size(76, 21);
            this.rbReplace.TabIndex = 1;
            this.rbReplace.TabStop = true;
            this.rbReplace.Text = "Replace";
            this.rbReplace.UseVisualStyleBackColor = true;
            this.rbReplace.CheckedChanged += new System.EventHandler(this.rbReplace_CheckedChanged);
            // 
            // rbDamged
            // 
            this.rbDamged.AutoSize = true;
            this.rbDamged.Location = new System.Drawing.Point(641, 54);
            this.rbDamged.Name = "rbDamged";
            this.rbDamged.Size = new System.Drawing.Size(81, 21);
            this.rbDamged.TabIndex = 2;
            this.rbDamged.Text = "Damged";
            this.rbDamged.UseVisualStyleBackColor = true;
            this.rbDamged.CheckedChanged += new System.EventHandler(this.rbDamged_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cost";
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(701, 191);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(36, 17);
            this.lblCost.TabIndex = 4;
            this.lblCost.Text = "10 $";
            // 
            // btnCtreat
            // 
            this.btnCtreat.Location = new System.Drawing.Point(641, 227);
            this.btnCtreat.Name = "btnCtreat";
            this.btnCtreat.Size = new System.Drawing.Size(105, 33);
            this.btnCtreat.TabIndex = 5;
            this.btnCtreat.Text = "Ctreat";
            this.btnCtreat.UseVisualStyleBackColor = true;
            this.btnCtreat.Click += new System.EventHandler(this.btnCtreat_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(641, 282);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 33);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbReNew
            // 
            this.rbReNew.AutoSize = true;
            this.rbReNew.Location = new System.Drawing.Point(641, 87);
            this.rbReNew.Name = "rbReNew";
            this.rbReNew.Size = new System.Drawing.Size(71, 21);
            this.rbReNew.TabIndex = 7;
            this.rbReNew.TabStop = true;
            this.rbReNew.Text = "ReNew";
            this.rbReNew.UseVisualStyleBackColor = true;
            this.rbReNew.CheckedChanged += new System.EventHandler(this.rbReNew_CheckedChanged);
            // 
            // rbDetaind
            // 
            this.rbDetaind.AutoSize = true;
            this.rbDetaind.Location = new System.Drawing.Point(641, 120);
            this.rbDetaind.Name = "rbDetaind";
            this.rbDetaind.Size = new System.Drawing.Size(76, 21);
            this.rbDetaind.TabIndex = 8;
            this.rbDetaind.TabStop = true;
            this.rbDetaind.Text = "Detaind";
            this.rbDetaind.UseVisualStyleBackColor = true;
            this.rbDetaind.CheckedChanged += new System.EventHandler(this.rbDetaind_CheckedChanged);
            // 
            // localLicense1
            // 
            this.localLicense1.Location = new System.Drawing.Point(0, 0);
            this.localLicense1.Name = "localLicense1";
            this.localLicense1.Size = new System.Drawing.Size(609, 404);
            this.localLicense1.TabIndex = 0;
            // 
            // rbGloabl
            // 
            this.rbGloabl.AutoSize = true;
            this.rbGloabl.Location = new System.Drawing.Point(641, 153);
            this.rbGloabl.Name = "rbGloabl";
            this.rbGloabl.Size = new System.Drawing.Size(113, 21);
            this.rbGloabl.TabIndex = 9;
            this.rbGloabl.TabStop = true;
            this.rbGloabl.Text = "Gloabl License";
            this.rbGloabl.UseVisualStyleBackColor = true;
            this.rbGloabl.CheckedChanged += new System.EventHandler(this.rbGloabl_CheckedChanged);
            // 
            // Replace_Damged
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbGloabl);
            this.Controls.Add(this.rbDetaind);
            this.Controls.Add(this.rbReNew);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCtreat);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbDamged);
            this.Controls.Add(this.rbReplace);
            this.Controls.Add(this.localLicense1);
            this.Name = "Replace_Damged";
            this.Size = new System.Drawing.Size(872, 430);
            this.Load += new System.EventHandler(this.Replace_Damged_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EntityLayer.Classes.LocalLicense localLicense1;
        private System.Windows.Forms.RadioButton rbReplace;
        private System.Windows.Forms.RadioButton rbDamged;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Button btnCtreat;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbReNew;
        private System.Windows.Forms.RadioButton rbDetaind;
        private System.Windows.Forms.RadioButton rbGloabl;
    }
}
