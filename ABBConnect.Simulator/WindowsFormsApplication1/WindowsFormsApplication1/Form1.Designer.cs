namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.grpbChangeBoundery = new System.Windows.Forms.GroupBox();
            this.txtbLowerBoundery = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.lbllBoundery = new System.Windows.Forms.Label();
            this.lbluBoundery = new System.Windows.Forms.Label();
            this.txtbUpperBoundery = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnPublish = new System.Windows.Forms.Button();
            this.grpbSensors = new System.Windows.Forms.GroupBox();
            this.lstbSensors = new System.Windows.Forms.ListBox();
            this.grpbAddValues = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.grpbChangeBoundery.SuspendLayout();
            this.grpbSensors.SuspendLayout();
            this.grpbAddValues.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbChangeBoundery
            // 
            this.grpbChangeBoundery.Controls.Add(this.txtbLowerBoundery);
            this.grpbChangeBoundery.Controls.Add(this.btnApply);
            this.grpbChangeBoundery.Controls.Add(this.lbllBoundery);
            this.grpbChangeBoundery.Controls.Add(this.lbluBoundery);
            this.grpbChangeBoundery.Controls.Add(this.txtbUpperBoundery);
            this.grpbChangeBoundery.Location = new System.Drawing.Point(329, 33);
            this.grpbChangeBoundery.Name = "grpbChangeBoundery";
            this.grpbChangeBoundery.Size = new System.Drawing.Size(111, 178);
            this.grpbChangeBoundery.TabIndex = 1;
            this.grpbChangeBoundery.TabStop = false;
            this.grpbChangeBoundery.Text = "Change Boundery";
            // 
            // txtbLowerBoundery
            // 
            this.txtbLowerBoundery.Location = new System.Drawing.Point(9, 47);
            this.txtbLowerBoundery.Name = "txtbLowerBoundery";
            this.txtbLowerBoundery.Size = new System.Drawing.Size(54, 20);
            this.txtbLowerBoundery.TabIndex = 9;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(6, 113);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(99, 35);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lbllBoundery
            // 
            this.lbllBoundery.AutoSize = true;
            this.lbllBoundery.Location = new System.Drawing.Point(4, 32);
            this.lbllBoundery.Name = "lbllBoundery";
            this.lbllBoundery.Size = new System.Drawing.Size(87, 13);
            this.lbllBoundery.TabIndex = 8;
            this.lbllBoundery.Text = "Lower Boundery:";
            // 
            // lbluBoundery
            // 
            this.lbluBoundery.AutoSize = true;
            this.lbluBoundery.Location = new System.Drawing.Point(6, 71);
            this.lbluBoundery.Name = "lbluBoundery";
            this.lbluBoundery.Size = new System.Drawing.Size(87, 13);
            this.lbluBoundery.TabIndex = 7;
            this.lbluBoundery.Text = "Upper Boundery:";
            // 
            // txtbUpperBoundery
            // 
            this.txtbUpperBoundery.Location = new System.Drawing.Point(9, 87);
            this.txtbUpperBoundery.Name = "txtbUpperBoundery";
            this.txtbUpperBoundery.Size = new System.Drawing.Size(54, 20);
            this.txtbUpperBoundery.TabIndex = 4;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(6, 32);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(99, 35);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(12, 281);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(428, 42);
            this.btnPublish.TabIndex = 11;
            this.btnPublish.Text = "Publish Values";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // grpbSensors
            // 
            this.grpbSensors.Controls.Add(this.lstbSensors);
            this.grpbSensors.Location = new System.Drawing.Point(12, 33);
            this.grpbSensors.Name = "grpbSensors";
            this.grpbSensors.Size = new System.Drawing.Size(171, 242);
            this.grpbSensors.TabIndex = 10;
            this.grpbSensors.TabStop = false;
            this.grpbSensors.Text = "Loaded Sensors";
            // 
            // lstbSensors
            // 
            this.lstbSensors.FormattingEnabled = true;
            this.lstbSensors.Location = new System.Drawing.Point(7, 20);
            this.lstbSensors.Name = "lstbSensors";
            this.lstbSensors.Size = new System.Drawing.Size(158, 212);
            this.lstbSensors.TabIndex = 0;
            this.lstbSensors.SelectedIndexChanged += new System.EventHandler(this.lstbSensors_SelectedIndexChanged);
            // 
            // grpbAddValues
            // 
            this.grpbAddValues.Controls.Add(this.btnLoad);
            this.grpbAddValues.Controls.Add(this.btnGenerate);
            this.grpbAddValues.Location = new System.Drawing.Point(200, 33);
            this.grpbAddValues.Name = "grpbAddValues";
            this.grpbAddValues.Size = new System.Drawing.Size(111, 148);
            this.grpbAddValues.TabIndex = 10;
            this.grpbAddValues.TabStop = false;
            this.grpbAddValues.Text = "Add Values";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(6, 87);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(99, 35);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 339);
            this.Controls.Add(this.grpbAddValues);
            this.Controls.Add(this.btnPublish);
            this.Controls.Add(this.grpbSensors);
            this.Controls.Add(this.grpbChangeBoundery);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "ABBConnect Simulator";
            this.grpbChangeBoundery.ResumeLayout(false);
            this.grpbChangeBoundery.PerformLayout();
            this.grpbSensors.ResumeLayout(false);
            this.grpbAddValues.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbChangeBoundery;
        private System.Windows.Forms.Label lbllBoundery;
        private System.Windows.Forms.Label lbluBoundery;
        private System.Windows.Forms.TextBox txtbUpperBoundery;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.GroupBox grpbSensors;
        private System.Windows.Forms.TextBox txtbLowerBoundery;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox grpbAddValues;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ListBox lstbSensors;
    }
}

