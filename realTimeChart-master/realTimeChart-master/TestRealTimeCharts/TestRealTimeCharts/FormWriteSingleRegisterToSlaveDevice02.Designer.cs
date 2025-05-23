﻿namespace TestRealTimeCharts
{
    partial class FormWriteSingleRegisterToSlaveDevice02
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReadTimer = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnWrite);
            this.groupBox1.Controls.Add(this.txtValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 126);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "\tCommand Code：06, Preset Single Register";
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(270, 46);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(120, 50);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "WRITE";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(118, 74);
            this.txtValue.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtValue.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(120, 22);
            this.txtValue.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Value of D:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(118, 46);
            this.txtAddress.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(120, 22);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.Value = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address of D:";
            // 
            // btnReadTimer
            // 
            this.btnReadTimer.Location = new System.Drawing.Point(12, 302);
            this.btnReadTimer.Name = "btnReadTimer";
            this.btnReadTimer.Size = new System.Drawing.Size(154, 38);
            this.btnReadTimer.TabIndex = 4;
            this.btnReadTimer.Text = "READ D0 To D9";
            this.btnReadTimer.UseVisualStyleBackColor = true;
            this.btnReadTimer.Click += new System.EventHandler(this.btnReadTimer_Click);
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(12, 144);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(468, 152);
            this.txtResult.TabIndex = 3;
            // 
            // FormWriteSingleRegisterToSlaveDevice02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 447);
            this.Controls.Add(this.btnReadTimer);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormWriteSingleRegisterToSlaveDevice02";
            this.Text = "Write single register D0, D1,... to slave device 02";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormWriteSingleRegisterToSlaveDevice02_FormClosed);
            this.Load += new System.EventHandler(this.FormWriteSingleRegisterToSlaveDevice02_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.NumericUpDown txtValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReadTimer;
        private System.Windows.Forms.TextBox txtResult;
    }
}