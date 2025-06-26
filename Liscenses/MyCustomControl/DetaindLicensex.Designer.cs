namespace Liscenses.MyCustomControl
{
    partial class DetaindLicensex
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
            this.localLicense1 = new EntityLayer.Classes.LocalLicense();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDetainDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFineFees = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCreatedByUserID = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblReleasedByUserID = new System.Windows.Forms.Label();
            this.btnRelase = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // localLicense1
            // 
            this.localLicense1.Location = new System.Drawing.Point(0, 0);
            this.localLicense1.Name = "localLicense1";
            this.localLicense1.Size = new System.Drawing.Size(609, 404);
            this.localLicense1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(626, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "DetainID";
            // 
            // lblDetainID
            // 
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.Location = new System.Drawing.Point(710, 27);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(0, 17);
            this.lblDetainID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(626, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "DetainDate";
            // 
            // lblDetainDate
            // 
            this.lblDetainDate.AutoSize = true;
            this.lblDetainDate.Location = new System.Drawing.Point(710, 96);
            this.lblDetainDate.Name = "lblDetainDate";
            this.lblDetainDate.Size = new System.Drawing.Size(0, 17);
            this.lblDetainDate.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(626, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "FineFees";
            // 
            // lblFineFees
            // 
            this.lblFineFees.AutoSize = true;
            this.lblFineFees.Location = new System.Drawing.Point(710, 165);
            this.lblFineFees.Name = "lblFineFees";
            this.lblFineFees.Size = new System.Drawing.Size(0, 17);
            this.lblFineFees.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(626, 234);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Created By";
            // 
            // lblCreatedByUserID
            // 
            this.lblCreatedByUserID.AutoSize = true;
            this.lblCreatedByUserID.Location = new System.Drawing.Point(710, 234);
            this.lblCreatedByUserID.Name = "lblCreatedByUserID";
            this.lblCreatedByUserID.Size = new System.Drawing.Size(0, 17);
            this.lblCreatedByUserID.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(626, 303);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "Released By";
            // 
            // lblReleasedByUserID
            // 
            this.lblReleasedByUserID.AutoSize = true;
            this.lblReleasedByUserID.Location = new System.Drawing.Point(710, 303);
            this.lblReleasedByUserID.Name = "lblReleasedByUserID";
            this.lblReleasedByUserID.Size = new System.Drawing.Size(0, 17);
            this.lblReleasedByUserID.TabIndex = 10;
            // 
            // btnRelase
            // 
            this.btnRelase.Location = new System.Drawing.Point(737, 357);
            this.btnRelase.Name = "btnRelase";
            this.btnRelase.Size = new System.Drawing.Size(92, 33);
            this.btnRelase.TabIndex = 11;
            this.btnRelase.Text = "Relase";
            this.btnRelase.UseVisualStyleBackColor = true;
            this.btnRelase.Click += new System.EventHandler(this.btnRelase_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(618, 357);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 33);
            this.button2.TabIndex = 12;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DetaindLicensex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnRelase);
            this.Controls.Add(this.lblReleasedByUserID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblCreatedByUserID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblFineFees);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblDetainDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDetainID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.localLicense1);
            this.Name = "DetaindLicensex";
            this.Size = new System.Drawing.Size(850, 438);
            this.Load += new System.EventHandler(this.DetaindLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EntityLayer.Classes.LocalLicense localLicense1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDetainDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFineFees;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCreatedByUserID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblReleasedByUserID;
        private System.Windows.Forms.Button btnRelase;
        private System.Windows.Forms.Button button2;
    }
}
